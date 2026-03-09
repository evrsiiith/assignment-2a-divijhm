// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class ValveInteract_GasValve : MonoBehaviour
    {
        void Update()
        {
            if ((GasValveStateStorage.Get(GameObject.Find("GasValve")) == GasValveStateEnum.Off && UserAlgorithms.IsObjectClicked(GameObject.Find("GasValve"))))
            {
                UserAlgorithms.HandleValveClick();
            }
        }
    }
}
