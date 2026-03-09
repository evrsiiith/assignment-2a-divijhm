// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_5
{
    public static class GreenVialStateStorage
    {
        private static Dictionary<GameObject, GreenVialStateEnum> stateTable = new();

        public static event Action<GameObject, GreenVialStateEnum> OnStateChanged;

        public static void Register(GameObject obj, GreenVialStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static GreenVialStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsReady(GameObject obj) => stateTable[obj] == GreenVialStateEnum.Ready;
        public static bool IsPoured(GameObject obj) => stateTable[obj] == GreenVialStateEnum.Poured;

        public static void SetReady(GameObject obj) => SetState(obj, GreenVialStateEnum.Ready);
        public static void SetPoured(GameObject obj) => SetState(obj, GreenVialStateEnum.Poured);

        private static void SetState(GameObject obj, GreenVialStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
