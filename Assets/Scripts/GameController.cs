namespace Game
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameController : MonoBehaviour
    {
        private static GameController instance;

        private static Dictionary<CollectibleItem.CollectibleTypes, int> totalItemCount;
        private static Dictionary<CollectibleItem.CollectibleTypes, int> itemGetCount;

        private bool paused;
        
        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameController>();
                }

                return instance;
            }
        }

        public void GetItem(CollectibleItem.CollectibleTypes itemType)
        {
            itemGetCount[itemType]++;
            CheckVictoryCondition();
        }

        private void CheckVictoryCondition()
        {
            if (itemGetCount[CollectibleItem.CollectibleTypes.Cherry] ==
                totalItemCount[CollectibleItem.CollectibleTypes.Cherry])
            {
                Debug.Log("WIN!");
            }
        }

        public void Start()
        {
            totalItemCount = new Dictionary<CollectibleItem.CollectibleTypes, int>();
            itemGetCount = new Dictionary<CollectibleItem.CollectibleTypes, int>();
            foreach (CollectibleItem.CollectibleTypes type in Enum.GetValues(typeof(CollectibleItem.CollectibleTypes)))
            {
                totalItemCount[type] = 0;
                itemGetCount[type] = 0;
            }

            var items = FindObjectsOfType<CollectibleItem>();
            foreach (var item in items)
            {
                totalItemCount[item.ItemType]++;
            }
        }

        public void Update()
        {
            var pause = Input.GetButtonDown("Cancel");
            if (pause)
            {
                paused = !paused;
                
                if (paused)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }
                
            }
        }
    }
}