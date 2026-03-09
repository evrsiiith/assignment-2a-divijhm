// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_6
{
    public static class GasValveStateStorage
    {
        private static Dictionary<GameObject, GasValveStateEnum> stateTable = new();

        public static event Action<GameObject, GasValveStateEnum> OnStateChanged;

        public static void Register(GameObject obj, GasValveStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static GasValveStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsOff(GameObject obj) => stateTable[obj] == GasValveStateEnum.Off;
        public static bool IsOn(GameObject obj) => stateTable[obj] == GasValveStateEnum.On;

        public static void SetOff(GameObject obj) => SetState(obj, GasValveStateEnum.Off);
        public static void SetOn(GameObject obj) => SetState(obj, GasValveStateEnum.On);

        private static void SetState(GameObject obj, GasValveStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
