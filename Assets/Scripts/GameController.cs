using TMPro;

namespace Game {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameController : MonoBehaviour {
        public TextMeshProUGUI FruitText;
        public TextMeshProUGUI FruitTextShadow;

        public TextMeshProUGUI GemText;
        public TextMeshProUGUI GemTextShadow;

        public GameObject PauseMenu;
        
        private static GameController instance;

        private static Dictionary<CollectibleItem.CollectibleTypes, int> totalItemCount;
        private static Dictionary<CollectibleItem.CollectibleTypes, int> itemGetCount;

        private static Dictionary<CollectibleItem.CollectibleTypes, TextMeshProUGUI> Text;
        private static Dictionary<CollectibleItem.CollectibleTypes, TextMeshProUGUI> Shadow;

        private bool paused;

        public static GameController Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<GameController>();
                }

                return instance;
            }
        }

        public void GetItem(CollectibleItem.CollectibleTypes itemType) {
            itemGetCount[itemType]++;
            UpdateItemInformation(itemType);

            CheckVictoryCondition();
        }

        private static void UpdateItemInformation(CollectibleItem.CollectibleTypes itemType) {
            var text = $"{itemGetCount[itemType]}/{totalItemCount[itemType]}";
            Text[itemType].text = text;
            Shadow[itemType].text = text;
        }

        private void CheckVictoryCondition() {
            if (itemGetCount[CollectibleItem.CollectibleTypes.Cherry] ==
                totalItemCount[CollectibleItem.CollectibleTypes.Cherry]) {
                Debug.Log("WIN!");
            }
        }

        public void Start() {
            Text = new Dictionary<CollectibleItem.CollectibleTypes, TextMeshProUGUI>() {
                {
                    CollectibleItem.CollectibleTypes.Cherry, FruitText
                }, {
                    CollectibleItem.CollectibleTypes.Crystal, GemText
                }
            };

            Shadow = new Dictionary<CollectibleItem.CollectibleTypes, TextMeshProUGUI>() {
                {
                    CollectibleItem.CollectibleTypes.Cherry, FruitTextShadow
                }, {
                    CollectibleItem.CollectibleTypes.Crystal, GemTextShadow
                }
            };

            totalItemCount = new Dictionary<CollectibleItem.CollectibleTypes, int>();
            itemGetCount = new Dictionary<CollectibleItem.CollectibleTypes, int>();
            var itemTypes = Enum.GetValues(typeof(CollectibleItem.CollectibleTypes));
            foreach (CollectibleItem.CollectibleTypes type in itemTypes) {
                totalItemCount[type] = 0;
                itemGetCount[type] = 0;
            }

            var items = FindObjectsOfType<CollectibleItem>();
            foreach (var item in items) {
                totalItemCount[item.ItemType]++;
            }

            foreach (CollectibleItem.CollectibleTypes type in itemTypes) {
                UpdateItemInformation(type);
            }
        }

        public void Update() {
            var esc = Input.GetButtonDown("Cancel");
            var enter = Input.GetButtonDown("Submit");
            if (esc || (enter && !paused)) {
                paused = !paused;

                PauseMenu.SetActive(!paused);
                Time.timeScale = paused ? 1 : 0;
            }
        }
    }
}