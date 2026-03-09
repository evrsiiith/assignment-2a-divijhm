// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class FloorStateStorage
    {
        private static Dictionary<GameObject, FloorStateEnum> stateTable = new();

        public static event Action<GameObject, FloorStateEnum> OnStateChanged;

        public static void Register(GameObject obj, FloorStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static FloorStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsReady(GameObject obj) => stateTable[obj] == FloorStateEnum.Ready;

        public static void SetReady(GameObject obj) => SetState(obj, FloorStateEnum.Ready);

        private static void SetState(GameObject obj, FloorStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
