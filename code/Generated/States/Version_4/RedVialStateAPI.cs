// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public static class RedVialStateAPI
    {
        public static bool Ready(GameObject obj) => RedVialStateStorage.IsReady(obj);
        public static bool Poured(GameObject obj) => RedVialStateStorage.IsPoured(obj);

        public static void SetReady(GameObject obj) => RedVialStateStorage.SetReady(obj);
        public static void SetPoured(GameObject obj) => RedVialStateStorage.SetPoured(obj);
    }
}
