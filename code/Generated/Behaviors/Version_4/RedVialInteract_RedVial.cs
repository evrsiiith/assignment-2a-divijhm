// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class RedVialInteract_RedVial : MonoBehaviour
    {
        void Update()
        {
            if ((RedVialStateStorage.Get(GameObject.Find("RedVial")) == RedVialStateEnum.Ready && UserAlgorithms.IsObjectClicked(GameObject.Find("RedVial"))))
            {
                UserAlgorithms.HandleRedVialClick();
            }
        }
    }
}
