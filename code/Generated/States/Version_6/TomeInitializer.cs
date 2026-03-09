// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class TomeInitializer : MonoBehaviour
    {
        public TomeStateEnum initialState = TomeStateEnum.Locked;

        void Awake()
        {
            TomeStateStorage.Register(gameObject, initialState);
        }
    }
}
