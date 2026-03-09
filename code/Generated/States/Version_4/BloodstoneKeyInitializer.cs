// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class BloodstoneKeyInitializer : MonoBehaviour
    {
        public BloodstoneKeyStateEnum initialState = BloodstoneKeyStateEnum.Inactive;

        void Awake()
        {
            BloodstoneKeyStateStorage.Register(gameObject, initialState);
        }
    }
}
