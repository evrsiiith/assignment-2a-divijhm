// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class RedVialInitializer : MonoBehaviour
    {
        public RedVialStateEnum initialState = RedVialStateEnum.Ready;

        void Awake()
        {
            RedVialStateStorage.Register(gameObject, initialState);
        }
    }
}
