// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class IronKeyPickup_IronKey : MonoBehaviour
    {
        void Update()
        {
            if ((IronKeyStateStorage.Get(GameObject.Find("IronKey")) == IronKeyStateEnum.Idle && UserAlgorithms.IsObjectClicked(GameObject.Find("IronKey"))))
            {
                UserAlgorithms.PickUpIronKey();
            }
        }
    }
}
