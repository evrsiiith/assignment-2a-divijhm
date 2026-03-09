// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class CauldronInitializer : MonoBehaviour
    {
        public CauldronStateEnum initialState = CauldronStateEnum.Empty;

        void Awake()
        {
            CauldronStateStorage.Register(gameObject, initialState);
        }
    }
}
