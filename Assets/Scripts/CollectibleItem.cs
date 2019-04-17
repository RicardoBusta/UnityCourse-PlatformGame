namespace Game
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class CollectibleItem : MonoBehaviour
    {
        private static readonly int Collect = Animator.StringToHash("Collect");

        private GameController controllerRef;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

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
            animator.SetTrigger(Collect);
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}