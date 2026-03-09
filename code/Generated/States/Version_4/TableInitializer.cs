// GENERATED FILE — DO NOT EDIT
using UnityEngine;

namespace Version_4
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
