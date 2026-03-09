// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public static class SceneManagerStateAPI
    {
        public static bool Setup(GameObject obj) => SceneManagerStateStorage.IsSetup(obj);
        public static bool Running(GameObject obj) => SceneManagerStateStorage.IsRunning(obj);

        public static void SetSetup(GameObject obj) => SceneManagerStateStorage.SetSetup(obj);
        public static void SetRunning(GameObject obj) => SceneManagerStateStorage.SetRunning(obj);
    }
}
