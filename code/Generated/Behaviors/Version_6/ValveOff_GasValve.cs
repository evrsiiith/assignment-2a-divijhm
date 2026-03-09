// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class ValveOff_GasValve : MonoBehaviour
    {
        void Update()
        {
            if ((GasValveStateStorage.Get(GameObject.Find("GasValve")) == GasValveStateEnum.On && UserAlgorithms.IsObjectClicked(GameObject.Find("GasValve"))))
            {
                UserAlgorithms.HandleValveOff();
            }
        }
    }
}
