// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_5
{
    public static class RedVialStateStorage
    {
        private static Dictionary<GameObject, RedVialStateEnum> stateTable = new();

        public static event Action<GameObject, RedVialStateEnum> OnStateChanged;

        public static void Register(GameObject obj, RedVialStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static RedVialStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsReady(GameObject obj) => stateTable[obj] == RedVialStateEnum.Ready;
        public static bool IsPoured(GameObject obj) => stateTable[obj] == RedVialStateEnum.Poured;

        public static void SetReady(GameObject obj) => SetState(obj, RedVialStateEnum.Ready);
        public static void SetPoured(GameObject obj) => SetState(obj, RedVialStateEnum.Poured);

        private static void SetState(GameObject obj, RedVialStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
