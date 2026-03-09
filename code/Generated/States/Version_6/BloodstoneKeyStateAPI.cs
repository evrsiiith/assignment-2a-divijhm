// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public static class BloodstoneKeyStateAPI
    {
        public static bool Inactive(GameObject obj) => BloodstoneKeyStateStorage.IsInactive(obj);
        public static bool Active(GameObject obj) => BloodstoneKeyStateStorage.IsActive(obj);
        public static bool Held(GameObject obj) => BloodstoneKeyStateStorage.IsHeld(obj);

        public static void SetInactive(GameObject obj) => BloodstoneKeyStateStorage.SetInactive(obj);
        public static void SetActive(GameObject obj) => BloodstoneKeyStateStorage.SetActive(obj);
        public static void SetHeld(GameObject obj) => BloodstoneKeyStateStorage.SetHeld(obj);
    }
}
