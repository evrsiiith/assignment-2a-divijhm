// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public static class IronKeyStateAPI
    {
        public static bool Idle(GameObject obj) => IronKeyStateStorage.IsIdle(obj);
        public static bool Held(GameObject obj) => IronKeyStateStorage.IsHeld(obj);
        public static bool Transmuted(GameObject obj) => IronKeyStateStorage.IsTransmuted(obj);

        public static void SetIdle(GameObject obj) => IronKeyStateStorage.SetIdle(obj);
        public static void SetHeld(GameObject obj) => IronKeyStateStorage.SetHeld(obj);
        public static void SetTransmuted(GameObject obj) => IronKeyStateStorage.SetTransmuted(obj);
    }
}
