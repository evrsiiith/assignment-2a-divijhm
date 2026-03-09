// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class IronKeyInitializer : MonoBehaviour
    {
        public IronKeyStateEnum initialState = IronKeyStateEnum.Idle;

        void Awake()
        {
            IronKeyStateStorage.Register(gameObject, initialState);
        }
    }
}
