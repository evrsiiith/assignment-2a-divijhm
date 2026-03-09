// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
{
    public class SceneManagerInitializer : MonoBehaviour
    {
        public SceneManagerStateEnum initialState = SceneManagerStateEnum.Setup;

        void Awake()
        {
            SceneManagerStateStorage.Register(gameObject, initialState);
        }
    }
}
