// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class SceneInit_SceneManager : MonoBehaviour
    {
        void Update()
        {
            if (SceneManagerStateStorage.Get(GameObject.Find("SceneManager")) == SceneManagerStateEnum.Setup)
            {
                UserAlgorithms.InitScene();
            }
        }
    }
}
