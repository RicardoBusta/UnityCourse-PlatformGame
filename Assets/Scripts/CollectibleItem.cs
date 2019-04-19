using NaughtyAttributes;

namespace Game {
    using System;
    using UnityEngine;

    public class CollectibleItem : MonoBehaviour {
        private static readonly int Collect = Animator.StringToHash("Collect");

        [BoxGroup("ScriptReferences")]
        [Required]
        public Animator Animator;

        [BoxGroup("ScriptReferences")]
        [Required]
        public AudioSource AudioSource;

        [BoxGroup("Assets")]
        [Required]
        public AudioClip CollectSound;

        private GameController controllerRef;

        private void Start() {
            controllerRef = GameController.Instance;
        }

        public enum CollectibleTypes {
            Cherry,
            Crystal
        }

        public CollectibleTypes ItemType;

        public void GetItem() {
            switch (ItemType) {
                case CollectibleTypes.Cherry:
                    break;
                case CollectibleTypes.Crystal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            PlaySound(CollectSound);
            controllerRef.GetItem(ItemType);
            Animator.SetTrigger(Collect);
        }

        public void Destroy() {
            gameObject.SetActive(false);
        }

        private void PlaySound(AudioClip audio) {
            AudioSource.clip = audio;
            AudioSource.volume = 0.5f;
            AudioSource.Play();
        }
    }
}