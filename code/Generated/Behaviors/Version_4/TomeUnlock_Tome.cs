// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class TomeUnlock_Tome : MonoBehaviour
    {
        void Update()
        {
            if ((TomeStateStorage.Get(GameObject.Find("Tome")) == TomeStateEnum.Locked && BloodstoneKeyStateStorage.Get(GameObject.Find("BloodstoneKey")) == BloodstoneKeyStateEnum.Held && UserAlgorithms.IsObjectClicked(GameObject.Find("Tome"))))
            {
                UserAlgorithms.OpenTome();
            }
        }
    }
}
