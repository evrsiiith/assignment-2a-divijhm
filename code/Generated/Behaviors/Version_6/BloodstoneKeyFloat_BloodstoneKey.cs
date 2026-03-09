// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class BloodstoneKeyFloat_BloodstoneKey : MonoBehaviour
    {
        void Update()
        {
            if (BloodstoneKeyStateStorage.Get(GameObject.Find("BloodstoneKey")) == BloodstoneKeyStateEnum.Active)
            {
                UserAlgorithms.FloatBloodstoneKey();
            }
        }
    }
}
