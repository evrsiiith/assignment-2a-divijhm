// GENERATED FILE — DO NOT EDIT
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Version_4
{
    public static class SceneManagerStateStorage
    {
        private static Dictionary<GameObject, SceneManagerStateEnum> stateTable = new();

        public static event Action<GameObject, SceneManagerStateEnum> OnStateChanged;

        public static void Register(GameObject obj, SceneManagerStateEnum initialState)
        {
            if (!stateTable.ContainsKey(obj))
                stateTable.Add(obj, initialState);
        }

        public static SceneManagerStateEnum Get(GameObject obj) => stateTable[obj];

        public static bool IsSetup(GameObject obj) => stateTable[obj] == SceneManagerStateEnum.Setup;
        public static bool IsRunning(GameObject obj) => stateTable[obj] == SceneManagerStateEnum.Running;

        public static void SetSetup(GameObject obj) => SetState(obj, SceneManagerStateEnum.Setup);
        public static void SetRunning(GameObject obj) => SetState(obj, SceneManagerStateEnum.Running);

        private static void SetState(GameObject obj, SceneManagerStateEnum newState)
        {
            if (stateTable[obj] != newState)
            {
                stateTable[obj] = newState;
                OnStateChanged?.Invoke(obj, newState);
            }
        }
    }
}
