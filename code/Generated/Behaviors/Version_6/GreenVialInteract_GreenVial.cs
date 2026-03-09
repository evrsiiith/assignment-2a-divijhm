// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class GreenVialInteract_GreenVial : MonoBehaviour
    {
        void Update()
        {
            if ((GreenVialStateStorage.Get(GameObject.Find("GreenVial")) == GreenVialStateEnum.Ready && UserAlgorithms.IsObjectClicked(GameObject.Find("GreenVial"))))
            {
                UserAlgorithms.HandleGreenVialClick();
            }
        }
    }
}
