namespace Game
{
    using System;
    using UnityEngine;

    public class CollectibleItem : MonoBehaviour
    {
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
        }
    }
}