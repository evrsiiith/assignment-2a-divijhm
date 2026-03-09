// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class CauldronHeat_Cauldron : MonoBehaviour
    {
        void Update()
        {
            if ((CauldronStateStorage.Get(GameObject.Find("Cauldron")) == CauldronStateEnum.Greened && GasValveStateStorage.Get(GameObject.Find("GasValve")) == GasValveStateEnum.On))
            {
                UserAlgorithms.HeatCauldron();
            }
        }
    }
}
