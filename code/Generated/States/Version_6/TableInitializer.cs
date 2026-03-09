// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_6
{
    public class TableInitializer : MonoBehaviour
    {
        public TableStateEnum initialState = TableStateEnum.Ready;

        void Awake()
        {
            TableStateStorage.Register(gameObject, initialState);
        }
    }
}
