// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class TomeStateStorage
    {
        private static Dictionary<GameObject, TomeStateEnum> stateTable = new();

        public static event Action<GameObject, TomeStateEnum> OnStateChanged;

        public static void Register(GameObject obj, TomeStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static TomeStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsLocked(GameObject obj) => stateTable[obj] == TomeStateEnum.Locked;
        public static bool IsOpen(GameObject obj) => stateTable[obj] == TomeStateEnum.Open;

        public static void SetLocked(GameObject obj) => SetState(obj, TomeStateEnum.Locked);
        public static void SetOpen(GameObject obj) => SetState(obj, TomeStateEnum.Open);

        private static void SetState(GameObject obj, TomeStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
