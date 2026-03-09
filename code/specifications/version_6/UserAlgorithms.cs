using UnityEngine;
using VReqDV;

namespace Version_6
{
    public static class UserAlgorithms
    {
        // Tracks the name of the object the player is currently holding ("IronKey" or "BloodstoneKey", or null)
        private static string heldObjectName = null;

        // -----------------------------------------------------------------------
        // UTILITY
        // -----------------------------------------------------------------------

        /// <summary>
        /// Returns true if the given GameObject was left-clicked this frame via raycast.
        /// </summary>
        public static bool IsObjectClicked(GameObject obj)
        {
            if (obj == null) return false;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    return hit.collider.gameObject == obj;
                }
            }
            return false;
        }

        // -----------------------------------------------------------------------
        // SCENE INITIALISATION (fires once when SceneManager state == setup)
        // -----------------------------------------------------------------------

        /// <summary>
        /// Colour-codes all interactive objects and transitions SceneManager to "running".
        /// Called once automatically by the SceneInit behavior.
        /// </summary>
        public static void InitScene()
        {
            SetObjectColor("Floor",    new Color(0.55f, 0.45f, 0.35f)); // dark wood
            SetObjectColor("Cauldron", new Color(0.25f, 0.25f, 0.25f)); // dark iron
            SetObjectColor("GasValve", new Color(0.50f, 0.50f, 0.50f)); // grey metal
            SetObjectColor("Tome",     new Color(0.20f, 0.08f, 0.04f)); // dark leather
            // Vials, keys and table use prefab materials — no colour override needed

            // Transition SceneManager so this behavior never fires again
            StateAccessor.SetState("SceneManager", "Running",
                GameObject.Find("SceneManager"), "Version_6");

            Debug.Log("[Alchemy] Scene initialised. Click GreenVial to begin the brewing sequence.");
        }

        // -----------------------------------------------------------------------
        // GREEN VIAL
        // -----------------------------------------------------------------------

        /// <summary>
        /// Attempt to pour the Green Vial.
        ///   Valid  : Cauldron is empty AND Gas Valve is off  → pour.
        ///   Invalid: any other state                         → explosion.
        /// </summary>
        public static void HandleGreenVialClick()
        {
            GameObject cauldron  = GameObject.Find("Cauldron");
            GameObject greenVial = GameObject.Find("GreenVial");
            GameObject gasValve  = GameObject.Find("GasValve");

            bool cauldronEmpty = StateAccessor.IsState("Cauldron", "Empty", cauldron, "Version_6");
            bool valveOff      = StateAccessor.IsState("GasValve", "Off",   gasValve, "Version_6");

            if (cauldronEmpty && valveOff)
            {
                StateAccessor.SetState("GreenVial", "Poured",  greenVial, "Version_6");
                StateAccessor.SetState("Cauldron",  "Greened", cauldron,  "Version_6");

                SetObjectColor("Cauldron", new Color(0.20f, 0.60f, 0.20f)); // dark green liquid
                SwapVialToEmpty(greenVial);

                Debug.Log("[Alchemy] Green Vial poured. Turn the Gas Valve to ignite the fire.");
            }
            else
            {
                Debug.Log("[Alchemy] WRONGSEQUENCE: Green Vial poured at the wrong time!");
                TriggerExplosion();
            }
        }

        // -----------------------------------------------------------------------
        // GAS VALVE
        // -----------------------------------------------------------------------

        /// <summary>
        /// Attempt to turn on the Gas Valve.
        ///   Valid  : Cauldron has green vial (greened state) → ignite.
        ///   Other  : just log — does not trigger explosion.
        /// </summary>
        // --- Valve animation state ---
        private static float _valveOrbitCurrent = 0f;  // degrees currently orbited from origin
        private static float _valveOrbitTarget  = 0f;  // degrees to orbit to
        private static bool  _valveAnimating    = false;

        public static void HandleValveClick()
        {
            GameObject cauldron = GameObject.Find("Cauldron");
            GameObject gasValve = GameObject.Find("GasValve");

            bool cauldronGreened = StateAccessor.IsState("Cauldron", "Greened", cauldron, "Version_6");

            if (cauldronGreened)
            {
                StateAccessor.SetState("GasValve", "On", gasValve, "Version_6");
                _valveOrbitTarget = 90f;
                _valveAnimating   = true;
                Debug.Log("[Alchemy] Gas Valve turning on...");
            }
            else
            {
                Debug.Log("[Alchemy] The valve turns but nothing happens — add the Green Vial first.");
            }
        }

        /// <summary>Turns the Gas Valve back off: animates it back to its original position.</summary>
        public static void HandleValveOff()
        {
            GameObject gasValve = GameObject.Find("GasValve");
            StateAccessor.SetState("GasValve", "Off", gasValve, "Version_6");
            _valveOrbitTarget = 0f;
            _valveAnimating   = true;
            Debug.Log("[Alchemy] Gas Valve turning off...");
        }

        /// <summary>Returns true while the valve is mid-animation (drives the ValveAnimating behavior).</summary>
        public static bool IsValveAnimating()
        {
            return _valveAnimating;
        }

        /// <summary>Steps the valve orbit toward its target each frame at 90°/sec.</summary>
        public static void AnimateValve()
        {
            GameObject gasValve = GameObject.Find("GasValve");
            if (gasValve == null) return;

            float remaining = _valveOrbitTarget - _valveOrbitCurrent;
            if (Mathf.Abs(remaining) < 0.01f)
            {
                // Snap to exact target
                OrbitValveAroundCauldron(gasValve, remaining);
                _valveOrbitCurrent = _valveOrbitTarget;
                _valveAnimating    = false;
                return;
            }
            float step  = 90f * Time.deltaTime;
            float delta = Mathf.Sign(remaining) * Mathf.Min(step, Mathf.Abs(remaining));
            OrbitValveAroundCauldron(gasValve, delta);
            _valveOrbitCurrent += delta;
        }

        // Incremental orbit of the valve around the cauldron's world-Y axis.
        private static void OrbitValveAroundCauldron(GameObject valve, float angleDeg)
        {
            if (valve == null || Mathf.Abs(angleDeg) < 0.0001f) return;
            GameObject cauldron = GameObject.Find("Cauldron");
            Vector3 pivot = cauldron != null
                ? new Vector3(cauldron.transform.position.x, valve.transform.position.y, cauldron.transform.position.z)
                : new Vector3(0f, valve.transform.position.y, 0f);
            valve.transform.RotateAround(pivot, Vector3.up, angleDeg);
        }

        // -----------------------------------------------------------------------
        // CAULDRON AUTO-HEAT (called by CauldronHeat behavior — no user click needed)
        // -----------------------------------------------------------------------

        /// <summary>
        /// Auto-transitions the Cauldron from greened → heated once the fire is on.
        /// </summary>
        public static void HeatCauldron()
        {
            GameObject cauldron = GameObject.Find("Cauldron");

            StateAccessor.SetState("Cauldron", "Heated", cauldron, "Version_6");

            SetObjectColor("Cauldron", new Color(1.0f, 0.50f, 0.0f)); // boiling orange

            Debug.Log("[Alchemy] Cauldron is now heated and boiling. Pour the Red Vial to complete the brew!");
        }

        // -----------------------------------------------------------------------
        // RED VIAL
        // -----------------------------------------------------------------------

        /// <summary>
        /// Attempt to pour the Red Vial.
        ///   Valid  : Cauldron is heated  → pour and prime.
        ///   Invalid: any other state     → explosion.
        /// </summary>
        public static void HandleRedVialClick()
        {
            GameObject cauldron = GameObject.Find("Cauldron");
            GameObject redVial  = GameObject.Find("RedVial");

            bool cauldronHeated = StateAccessor.IsState("Cauldron", "Heated", cauldron, "Version_6");

            if (cauldronHeated)
            {
                StateAccessor.SetState("RedVial",  "Poured", redVial,  "Version_6");
                StateAccessor.SetState("Cauldron", "Primed", cauldron, "Version_6");

                SetObjectColor("Cauldron", new Color(0.55f, 0.0f, 0.70f)); // deep purple — primed
                SwapVialToEmpty(redVial);

                Debug.Log("[Alchemy] Red Vial poured! Cauldron is PRIMED. Drop the Iron Key into it to transmute.");
            }
            else
            {
                Debug.Log("[Alchemy] WRONG SEQUENCE: Red Vial poured at the wrong time!");
                TriggerExplosion();
            }
        }

        // -----------------------------------------------------------------------
        // IRON KEY
        // -----------------------------------------------------------------------

        /// <summary>Picks up the Iron Key.</summary>
        public static void PickUpIronKey()
        {
            if (heldObjectName != null)
            {
                Debug.Log("[Alchemy] Already holding: " + heldObjectName + ". Put it down first.");
                return;
            }
            heldObjectName = "IronKey";
            StateAccessor.SetState("IronKey", "Held", GameObject.Find("IronKey"), "Version_6");

            Debug.Log("[Alchemy] Iron Key picked up. Click the PRIMED Cauldron to transmute it, or click the Tome to test the lock.");
        }

        /// <summary>Puts the Iron Key back down.</summary>
        public static void PutDownIronKey()
        {
            heldObjectName = null;
            StateAccessor.SetState("IronKey", "Idle", GameObject.Find("IronKey"), "Version_6");

            Debug.Log("[Alchemy] Iron Key put down.");
        }

        // -----------------------------------------------------------------------
        // TRANSMUTATION  (Iron Key dropped into Primed Cauldron)
        // -----------------------------------------------------------------------

        /// <summary>
        /// Transmutes the Iron Key into the Bloodstone Key.
        /// Called when the player clicks the Primed Cauldron while holding the Iron Key.
        /// </summary>
        public static void TransmuteKey()
        {
            GameObject ironKey       = GameObject.Find("IronKey");
            GameObject bloodstoneKey = GameObject.Find("BloodstoneKey");
            GameObject cauldron      = GameObject.Find("Cauldron");

            // "Consume" the Iron Key — move it far below the scene so it is invisible
            StateAccessor.SetState("IronKey", "Transmuted", ironKey, "Version_6");
            if (ironKey != null)
                ironKey.transform.position = new Vector3(0f, -20f, 0f);

            // Reveal the Bloodstone Key at the spot where the Iron Key was
            if (bloodstoneKey != null)
            {
                bloodstoneKey.transform.position = new Vector3(1.5f, 4.0f, -2.82f);
                SetObjectColor("BloodstoneKey", new Color(0.72f, 0.07f, 0.07f)); // vivid blood-red
            }
            StateAccessor.SetState("BloodstoneKey", "Active", bloodstoneKey, "Version_6");

            // Cauldron spent — change colour to indicate it is no longer active
            SetObjectColor("Cauldron", new Color(0.25f, 0.25f, 0.25f));

            heldObjectName = null;

            Debug.Log("[Alchemy] TRANSMUTATION! The Iron Key dissolved and the Bloodstone Key has appeared. Pick it up and unlock the Tome!");
        }

        // -----------------------------------------------------------------------
        // IRON KEY USED ON TOME  (denied)
        // -----------------------------------------------------------------------

        /// <summary>Called when player clicks the Tome while holding the Iron Key.</summary>
        public static void DenyTomeAccess()
        {
            Debug.Log("[Alchemy] ACCESS DENIED: The Iron Key rattles against the lock but does not fit. You need the BLOODSTONE KEY.");
        }

        // -----------------------------------------------------------------------
        // BLOODSTONE KEY
        // -----------------------------------------------------------------------

        /// <summary>
        /// Called every frame while BloodstoneKey is active.
        /// Rotates the key around Y and bobs it gently up and down.
        /// </summary>
        private static float _bloodstoneKeyAngle = 0f;

        public static void FloatBloodstoneKey()
        {
            GameObject key = GameObject.Find("BloodstoneKey");
            if (key == null) return;
            _bloodstoneKeyAngle = (_bloodstoneKeyAngle + 90f * Time.deltaTime) % 360f;
            key.transform.eulerAngles = new Vector3(0f, _bloodstoneKeyAngle, 0f);
        }

        /// <summary>Picks up the Bloodstone Key.</summary>
        public static void PickUpBloodstoneKey()
        {
            if (heldObjectName != null)
            {
                Debug.Log("[Alchemy] Already holding: " + heldObjectName + ". Put it down first.");
                return;
            }
            heldObjectName = "BloodstoneKey";
            StateAccessor.SetState("BloodstoneKey", "Held", GameObject.Find("BloodstoneKey"), "Version_6");

            Debug.Log("[Alchemy] Bloodstone Key picked up. Click the Tome to unseal it!");
        }

        /// <summary>Puts the Bloodstone Key back down.</summary>
        public static void PutDownBloodstoneKey()
        {
            heldObjectName = null;
            StateAccessor.SetState("BloodstoneKey", "Active", GameObject.Find("BloodstoneKey"), "Version_6");

            Debug.Log("[Alchemy] Bloodstone Key put down.");
        }

        // -----------------------------------------------------------------------
        // TOME UNLOCK
        // -----------------------------------------------------------------------

        /// <summary>
        /// Unseals the Tome using the Bloodstone Key. Scene objective complete.
        /// </summary>
        public static void OpenTome()
        {
            GameObject tome         = GameObject.Find("Tome");
            GameObject bloodstoneKey = GameObject.Find("BloodstoneKey");

            StateAccessor.SetState("Tome", "Open", tome, "Version_6");

            // Gold cover — tome is now open
            SetObjectColor("Tome", new Color(1.0f, 0.84f, 0.0f));

            // Rotate the tome to simulate opening (tilt outward)
            if (tome != null)
                tome.transform.rotation = Quaternion.Euler(0f, -30f, -75f);

            // Move the Bloodstone Key underground — it was consumed by the seal
            if (bloodstoneKey != null)
                bloodstoneKey.transform.position = new Vector3(1.5f, -20f, 0f);

            heldObjectName = null;

            Debug.Log("[Alchemy] *** The Tome of Dark Arts has been UNSEALED! Objective complete! ***");
        }

        // -----------------------------------------------------------------------
        // EXPLOSION / RESET
        // -----------------------------------------------------------------------

        /// <summary>
        /// Triggers the explosion visual and resets all brewing state back to the initial values.
        /// </summary>
        public static void TriggerExplosion()
        {
            GameObject cauldron  = GameObject.Find("Cauldron");
            GameObject greenVial = GameObject.Find("GreenVial");
            GameObject redVial   = GameObject.Find("RedVial");
            GameObject gasValve  = GameObject.Find("GasValve");
            GameObject ironKey   = GameObject.Find("IronKey");

            // Brief red flash on the cauldron to indicate explosion
            SetObjectColor("Cauldron", Color.red);

            Debug.Log("[Alchemy] BOOM! The cauldron explodes in a cloud of foul smoke. Resetting the brew...");

            // Reset all brewing states
            StateAccessor.SetState("Cauldron",  "Failed",  cauldron,  "Version_6");
            StateAccessor.SetState("GreenVial", "Ready",   greenVial, "Version_6");
            StateAccessor.SetState("RedVial",   "Ready",   redVial,   "Version_6");
            StateAccessor.SetState("GasValve",  "Off",     gasValve,  "Version_6");

            // Re-apply initial colours and restore vial materials
            SetObjectColor("Cauldron",  new Color(0.25f, 0.25f, 0.25f));
            RestoreVialMaterial(greenVial, "VialGreen");
            RestoreVialMaterial(redVial,   "VialRed");
            // Snap valve instantly back to off position on explosion reset
            if (gasValve != null)
            {
                float snapDelta = -_valveOrbitCurrent;
                OrbitValveAroundCauldron(gasValve, snapDelta);
            }
            _valveOrbitCurrent = 0f;
            _valveOrbitTarget  = 0f;
            _valveAnimating    = false;

            // If the iron key was held, release it
            if (heldObjectName == "IronKey")
            {
                heldObjectName = null;
                // Bring back the Iron Key if it was somehow moved (should not happen in explosion path)
                if (ironKey != null && ironKey.transform.position.y < -5f)
                    ironKey.transform.position = new Vector3(1.5f, 0.875f, 0f);
                StateAccessor.SetState("IronKey", "Idle", ironKey, "Version_6");
            }

            // Transition cauldron from Failed back to Empty so play can restart
            StateAccessor.SetState("Cauldron", "Empty", cauldron, "Version_6");

            Debug.Log("[Alchemy] Cauldron reset. Start the sequence again: Green Vial → Valve → Red Vial.");
        }

        // -----------------------------------------------------------------------
        // PRIVATE HELPER
        // -----------------------------------------------------------------------

        private static void SetObjectColor(string objName, Color color)
        {
            GameObject obj = GameObject.Find(objName);
            if (obj == null) return;
            Renderer r = obj.GetComponent<Renderer>();
            if (r != null) r.material.color = color;
        }

        /// <summary>
        /// Swaps all renderers on a vial GameObject to the VialEmpty material,
        /// giving a clear visual signal that the vial has been poured out.
        /// The material must live at Assets/Resources/VialEmpty.mat.
        /// </summary>
        private static void SwapVialToEmpty(GameObject vial)
        {
            if (vial == null) return;
            Material emptyMat = Resources.Load<Material>("VialEmpty");
            if (emptyMat == null)
            {
                Debug.LogWarning("[Alchemy] VialEmpty material not found in Resources folder.");
                return;
            }
            foreach (Renderer r in vial.GetComponentsInChildren<Renderer>(true))
            {
                Material[] mats = new Material[r.materials.Length];
                for (int i = 0; i < mats.Length; i++) mats[i] = emptyMat;
                r.materials = mats;
            }
        }

        /// <summary>
        /// Restores a vial's material from Resources by name (e.g. "VialGreen" or "VialRed").
        /// Called during reset/explosion to return vials to their original look.
        /// </summary>
        private static void RestoreVialMaterial(GameObject vial, string materialName)
        {
            if (vial == null) return;
            Material mat = Resources.Load<Material>(materialName);
            if (mat == null)
            {
                Debug.LogWarning("[Alchemy] Material not found in Resources: " + materialName);
                return;
            }
            foreach (Renderer r in vial.GetComponentsInChildren<Renderer>(true))
            {
                Material[] mats = new Material[r.materials.Length];
                for (int i = 0; i < mats.Length; i++) mats[i] = mat;
                r.materials = mats;
            }
        }
    }
}
