namespace Game
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class CollectibleItem : MonoBehaviour
    {
        private GameController controllerRef;
        
        private void Start()
        {
            controllerRef = GameController.Instance;
        }

        public enum CollectibleTypes
        {
            Cherry,
            Crystal
        }

        public CollectibleTypes ItemType;

        public void GetItem()
        {
            switch (ItemType)
            {
                case CollectibleTypes.Cherry:
                    break;
                case CollectibleTypes.Crystal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            controllerRef.GetItem(ItemType);
            gameObject.SetActive(false);
        }
    }
}