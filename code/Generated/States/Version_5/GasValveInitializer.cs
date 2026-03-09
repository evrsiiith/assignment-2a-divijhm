// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class GasValveInitializer : MonoBehaviour
    {
        public GasValveStateEnum initialState = GasValveStateEnum.Off;

        void Awake()
        {
            GasValveStateStorage.Register(gameObject, initialState);
        }
    }
}
