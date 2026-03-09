// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class BloodstoneKeyDrop_BloodstoneKey : MonoBehaviour
    {
        void Update()
        {
            if ((BloodstoneKeyStateStorage.Get(GameObject.Find("BloodstoneKey")) == BloodstoneKeyStateEnum.Held && UserAlgorithms.IsObjectClicked(GameObject.Find("BloodstoneKey"))))
            {
                UserAlgorithms.PutDownBloodstoneKey();
            }
        }
    }
}
