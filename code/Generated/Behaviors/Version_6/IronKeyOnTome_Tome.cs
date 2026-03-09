// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class IronKeyOnTome_Tome : MonoBehaviour
    {
        void Update()
        {
            if ((TomeStateStorage.Get(GameObject.Find("Tome")) == TomeStateEnum.Locked && IronKeyStateStorage.Get(GameObject.Find("IronKey")) == IronKeyStateEnum.Held && UserAlgorithms.IsObjectClicked(GameObject.Find("Tome"))))
            {
                UserAlgorithms.DenyTomeAccess();
            }
        }
    }
}
