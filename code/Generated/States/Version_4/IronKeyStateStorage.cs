// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class IronKeyStateStorage
    {
        private static Dictionary<GameObject, IronKeyStateEnum> stateTable = new();

        public static event Action<GameObject, IronKeyStateEnum> OnStateChanged;

        public static void Register(GameObject obj, IronKeyStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static IronKeyStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsIdle(GameObject obj) => stateTable[obj] == IronKeyStateEnum.Idle;
        public static bool IsHeld(GameObject obj) => stateTable[obj] == IronKeyStateEnum.Held;
        public static bool IsTransmuted(GameObject obj) => stateTable[obj] == IronKeyStateEnum.Transmuted;

        public static void SetIdle(GameObject obj) => SetState(obj, IronKeyStateEnum.Idle);
        public static void SetHeld(GameObject obj) => SetState(obj, IronKeyStateEnum.Held);
        public static void SetTransmuted(GameObject obj) => SetState(obj, IronKeyStateEnum.Transmuted);

        private static void SetState(GameObject obj, IronKeyStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
