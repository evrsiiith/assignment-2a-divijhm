// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_5
{
    public static class TableStateStorage
    {
        private static Dictionary<GameObject, TableStateEnum> stateTable = new();

        public static event Action<GameObject, TableStateEnum> OnStateChanged;

        public static void Register(GameObject obj, TableStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static TableStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsReady(GameObject obj) => stateTable[obj] == TableStateEnum.Ready;

        public static void SetReady(GameObject obj) => SetState(obj, TableStateEnum.Ready);

        private static void SetState(GameObject obj, TableStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
