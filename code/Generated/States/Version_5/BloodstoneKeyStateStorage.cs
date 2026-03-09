// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_5
{
    public static class BloodstoneKeyStateStorage
    {
        private static Dictionary<GameObject, BloodstoneKeyStateEnum> stateTable = new();

        public static event Action<GameObject, BloodstoneKeyStateEnum> OnStateChanged;

        public static void Register(GameObject obj, BloodstoneKeyStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static BloodstoneKeyStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsInactive(GameObject obj) => stateTable[obj] == BloodstoneKeyStateEnum.Inactive;
        public static bool IsActive(GameObject obj) => stateTable[obj] == BloodstoneKeyStateEnum.Active;
        public static bool IsHeld(GameObject obj) => stateTable[obj] == BloodstoneKeyStateEnum.Held;

        public static void SetInactive(GameObject obj) => SetState(obj, BloodstoneKeyStateEnum.Inactive);
        public static void SetActive(GameObject obj) => SetState(obj, BloodstoneKeyStateEnum.Active);
        public static void SetHeld(GameObject obj) => SetState(obj, BloodstoneKeyStateEnum.Held);

        private static void SetState(GameObject obj, BloodstoneKeyStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
