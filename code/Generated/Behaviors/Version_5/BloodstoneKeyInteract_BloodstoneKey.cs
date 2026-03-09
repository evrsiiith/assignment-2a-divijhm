// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class BloodstoneKeyInteract_BloodstoneKey : MonoBehaviour
    {
        void Update()
        {
            if ((BloodstoneKeyStateStorage.Get(GameObject.Find("BloodstoneKey")) == BloodstoneKeyStateEnum.Active && UserAlgorithms.IsObjectClicked(GameObject.Find("BloodstoneKey"))))
            {
                UserAlgorithms.PickUpBloodstoneKey();
            }
        }
    }
}
