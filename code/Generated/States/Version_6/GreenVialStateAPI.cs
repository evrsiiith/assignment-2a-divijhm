// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public static class GreenVialStateAPI
    {
        public static bool Ready(GameObject obj) => GreenVialStateStorage.IsReady(obj);
        public static bool Poured(GameObject obj) => GreenVialStateStorage.IsPoured(obj);

        public static void SetReady(GameObject obj) => GreenVialStateStorage.SetReady(obj);
        public static void SetPoured(GameObject obj) => GreenVialStateStorage.SetPoured(obj);
    }
}
