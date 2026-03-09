// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class GreenVialInitializer : MonoBehaviour
    {
        public GreenVialStateEnum initialState = GreenVialStateEnum.Ready;

        void Awake()
        {
            GreenVialStateStorage.Register(gameObject, initialState);
        }
    }
}
