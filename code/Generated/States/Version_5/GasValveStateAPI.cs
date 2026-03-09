// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public static class GasValveStateAPI
    {
        public static bool Off(GameObject obj) => GasValveStateStorage.IsOff(obj);
        public static bool On(GameObject obj) => GasValveStateStorage.IsOn(obj);

        public static void SetOff(GameObject obj) => GasValveStateStorage.SetOff(obj);
        public static void SetOn(GameObject obj) => GasValveStateStorage.SetOn(obj);
    }
}
