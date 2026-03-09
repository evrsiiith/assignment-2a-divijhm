// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public static class TomeStateAPI
    {
        public static bool Locked(GameObject obj) => TomeStateStorage.IsLocked(obj);
        public static bool Open(GameObject obj) => TomeStateStorage.IsOpen(obj);

        public static void SetLocked(GameObject obj) => TomeStateStorage.SetLocked(obj);
        public static void SetOpen(GameObject obj) => TomeStateStorage.SetOpen(obj);
    }
}
