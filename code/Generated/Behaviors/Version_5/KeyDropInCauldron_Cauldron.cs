// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_5
{
    public class KeyDropInCauldron_Cauldron : MonoBehaviour
    {
        void Update()
        {
            if ((CauldronStateStorage.Get(GameObject.Find("Cauldron")) == CauldronStateEnum.Primed && IronKeyStateStorage.Get(GameObject.Find("IronKey")) == IronKeyStateEnum.Held && UserAlgorithms.IsObjectClicked(GameObject.Find("Cauldron"))))
            {
                UserAlgorithms.TransmuteKey();
            }
        }
    }
}
