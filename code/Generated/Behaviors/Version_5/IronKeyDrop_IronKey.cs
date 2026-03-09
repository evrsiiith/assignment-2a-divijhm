// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class IronKeyDrop_IronKey : MonoBehaviour
    {
        void Update()
        {
            if ((IronKeyStateStorage.Get(GameObject.Find("IronKey")) == IronKeyStateEnum.Held && UserAlgorithms.IsObjectClicked(GameObject.Find("IronKey"))))
            {
                UserAlgorithms.PutDownIronKey();
            }
        }
    }
}
