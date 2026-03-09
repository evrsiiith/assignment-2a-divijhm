// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
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
