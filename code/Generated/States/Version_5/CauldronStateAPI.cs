// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public static class CauldronStateAPI
    {
        public static bool Empty(GameObject obj) => CauldronStateStorage.IsEmpty(obj);
        public static bool Greened(GameObject obj) => CauldronStateStorage.IsGreened(obj);
        public static bool Heated(GameObject obj) => CauldronStateStorage.IsHeated(obj);
        public static bool Primed(GameObject obj) => CauldronStateStorage.IsPrimed(obj);
        public static bool Failed(GameObject obj) => CauldronStateStorage.IsFailed(obj);

        public static void SetEmpty(GameObject obj) => CauldronStateStorage.SetEmpty(obj);
        public static void SetGreened(GameObject obj) => CauldronStateStorage.SetGreened(obj);
        public static void SetHeated(GameObject obj) => CauldronStateStorage.SetHeated(obj);
        public static void SetPrimed(GameObject obj) => CauldronStateStorage.SetPrimed(obj);
        public static void SetFailed(GameObject obj) => CauldronStateStorage.SetFailed(obj);
    }
}
