// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class ValveAnimating_GasValve : MonoBehaviour
    {
        void Update()
        {
            if (UserAlgorithms.IsValveAnimating())
            {
                UserAlgorithms.AnimateValve();
            }
        }
    }
}
