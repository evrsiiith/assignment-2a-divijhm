using UnityEngine;
using VReqDV;

namespace Version_4
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
            SetObjectColor("Floor",          new Color(0.55f, 0.45f, 0.35f)); // dark wood
            SetObjectColor("Table",          new Color(0.45f, 0.28f, 0.12f)); // brown
            SetObjectColor("Cauldron",       new Color(0.25f, 0.25f, 0.25f)); // dark iron
            SetObjectColor("GasValve",       new Color(0.50f, 0.50f, 0.50f)); // grey metal
            SetObjectColor("GreenVial",      Color.green);
            SetObjectColor("RedVial",        Color.red);
            SetObjectColor("IronKey",        new Color(0.65f, 0.65f, 0.65f)); // silver-grey
            SetObjectColor("BloodstoneKey",  new Color(0.72f, 0.07f, 0.07f)); // deep red
            SetObjectColor("Tome",           new Color(0.20f, 0.08f, 0.04f)); // dark leather

            // Transition SceneManager so this behavior never fires again
            StateAccessor.SetState("SceneManager", "Running",
                GameObject.Find("SceneManager"), "Version_4");

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

            bool cauldronEmpty = StateAccessor.IsState("Cauldron", "Empty", cauldron, "Version_4");
            bool valveOff      = StateAccessor.IsState("GasValve", "Off",   gasValve, "Version_4");

            if (cauldronEmpty && valveOff)
            {
                StateAccessor.SetState("GreenVial", "Poured",  greenVial, "Version_4");
                StateAccessor.SetState("Cauldron",  "Greened", cauldron,  "Version_4");

                SetObjectColor("Cauldron", new Color(0.20f, 0.60f, 0.20f)); // dark green liquid
                SetObjectColor("GreenVial", new Color(0.10f, 0.40f, 0.10f)); // emptied vial (darker)

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
        public static void HandleValveClick()
        {
            GameObject cauldron = GameObject.Find("Cauldron");
            GameObject gasValve = GameObject.Find("GasValve");

            bool cauldronGreened = StateAccessor.IsState("Cauldron", "Greened", cauldron, "Version_4");

            if (cauldronGreened)
            {
                StateAccessor.SetState("GasValve", "On", gasValve, "Version_4");

                SetObjectColor("GasValve", new Color(1.0f, 0.45f, 0.0f)); // glowing orange

                Debug.Log("[Alchemy] Gas Valve ignited! The fire is burning. Cauldron will heat up.");
            }
            else
            {
                Debug.Log("[Alchemy] The valve turns but nothing happens — add the Green Vial first.");
            }
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

            StateAccessor.SetState("Cauldron", "Heated", cauldron, "Version_4");

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

            bool cauldronHeated = StateAccessor.IsState("Cauldron", "Heated", cauldron, "Version_4");

            if (cauldronHeated)
            {
                StateAccessor.SetState("RedVial",  "Poured", redVial,  "Version_4");
                StateAccessor.SetState("Cauldron", "Primed", cauldron, "Version_4");

                SetObjectColor("Cauldron", new Color(0.55f, 0.0f, 0.70f)); // deep purple — primed
                SetObjectColor("RedVial",  new Color(0.40f, 0.0f, 0.0f));  // emptied vial

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
            StateAccessor.SetState("IronKey", "Held", GameObject.Find("IronKey"), "Version_4");

            Debug.Log("[Alchemy] Iron Key picked up. Click the PRIMED Cauldron to transmute it, or click the Tome to test the lock.");
        }

        /// <summary>Puts the Iron Key back down.</summary>
        public static void PutDownIronKey()
        {
            heldObjectName = null;
            StateAccessor.SetState("IronKey", "Idle", GameObject.Find("IronKey"), "Version_4");

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
            StateAccessor.SetState("IronKey", "Transmuted", ironKey, "Version_4");
            if (ironKey != null)
                ironKey.transform.position = new Vector3(0f, -20f, 0f);

            // Reveal the Bloodstone Key at the spot where the Iron Key was
            if (bloodstoneKey != null)
            {
                bloodstoneKey.transform.position = new Vector3(1.5f, 1.05f, 0f);
                SetObjectColor("BloodstoneKey", new Color(0.72f, 0.07f, 0.07f)); // vivid blood-red
            }
            StateAccessor.SetState("BloodstoneKey", "Active", bloodstoneKey, "Version_4");

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

        /// <summary>Picks up the Bloodstone Key.</summary>
        public static void PickUpBloodstoneKey()
        {
            if (heldObjectName != null)
            {
                Debug.Log("[Alchemy] Already holding: " + heldObjectName + ". Put it down first.");
                return;
            }
            heldObjectName = "BloodstoneKey";
            StateAccessor.SetState("BloodstoneKey", "Held", GameObject.Find("BloodstoneKey"), "Version_4");

            Debug.Log("[Alchemy] Bloodstone Key picked up. Click the Tome to unseal it!");
        }

        /// <summary>Puts the Bloodstone Key back down.</summary>
        public static void PutDownBloodstoneKey()
        {
            heldObjectName = null;
            StateAccessor.SetState("BloodstoneKey", "Active", GameObject.Find("BloodstoneKey"), "Version_4");

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

            StateAccessor.SetState("Tome", "Open", tome, "Version_4");

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
            StateAccessor.SetState("Cauldron",  "Failed",  cauldron,  "Version_4");
            StateAccessor.SetState("GreenVial", "Ready",   greenVial, "Version_4");
            StateAccessor.SetState("RedVial",   "Ready",   redVial,   "Version_4");
            StateAccessor.SetState("GasValve",  "Off",     gasValve,  "Version_4");

            // Re-apply initial colours
            SetObjectColor("Cauldron",  new Color(0.25f, 0.25f, 0.25f));
            SetObjectColor("GreenVial", Color.green);
            SetObjectColor("RedVial",   Color.red);
            SetObjectColor("GasValve",  new Color(0.50f, 0.50f, 0.50f));

            // If the iron key was held, release it
            if (heldObjectName == "IronKey")
            {
                heldObjectName = null;
                // Bring back the Iron Key if it was somehow moved (should not happen in explosion path)
                if (ironKey != null && ironKey.transform.position.y < -5f)
                    ironKey.transform.position = new Vector3(1.5f, 0.875f, 0f);
                StateAccessor.SetState("IronKey", "Idle", ironKey, "Version_4");
            }

            // Transition cauldron from Failed back to Empty so play can restart
            StateAccessor.SetState("Cauldron", "Empty", cauldron, "Version_4");

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
    }
}
