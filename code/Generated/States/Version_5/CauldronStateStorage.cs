// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_5
{
    public static class CauldronStateStorage
    {
        private static Dictionary<GameObject, CauldronStateEnum> stateTable = new();

        public static event Action<GameObject, CauldronStateEnum> OnStateChanged;

        public static void Register(GameObject obj, CauldronStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static CauldronStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsEmpty(GameObject obj) => stateTable[obj] == CauldronStateEnum.Empty;
        public static bool IsGreened(GameObject obj) => stateTable[obj] == CauldronStateEnum.Greened;
        public static bool IsHeated(GameObject obj) => stateTable[obj] == CauldronStateEnum.Heated;
        public static bool IsPrimed(GameObject obj) => stateTable[obj] == CauldronStateEnum.Primed;
        public static bool IsFailed(GameObject obj) => stateTable[obj] == CauldronStateEnum.Failed;

        public static void SetEmpty(GameObject obj) => SetState(obj, CauldronStateEnum.Empty);
        public static void SetGreened(GameObject obj) => SetState(obj, CauldronStateEnum.Greened);
        public static void SetHeated(GameObject obj) => SetState(obj, CauldronStateEnum.Heated);
        public static void SetPrimed(GameObject obj) => SetState(obj, CauldronStateEnum.Primed);
        public static void SetFailed(GameObject obj) => SetState(obj, CauldronStateEnum.Failed);

        private static void SetState(GameObject obj, CauldronStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
