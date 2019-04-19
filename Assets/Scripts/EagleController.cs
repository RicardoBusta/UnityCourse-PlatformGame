using NaughtyAttributes;
using UnityEngine;

namespace Game {
    using DG.Tweening;

    public class EagleController : EnemyController {
        private static readonly int Moving = Animator.StringToHash("Moving");

        [BoxGroup("Assets")]
        [Required]
        public AudioClip FlapSound;

        [BoxGroup("Parameters")]
        public float MovementAmount;

        [BoxGroup("Parameters")]
        public float MovementTime;

        [BoxGroup("Parameters")]
        public float WaitTime;

        private void Start() {
            MoveUp();
        }

        private void MoveUp() {
            Animator.SetBool(Moving, false);
            tween = DOVirtual.DelayedCall(WaitTime, () => {
                Animator.SetBool(Moving, true);
                var y = transform.position.y + MovementAmount;
                tween = RigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveDown;
            });
        }

        private void MoveDown() {
            Animator.SetBool(Moving, false);
            tween = DOVirtual.DelayedCall(WaitTime, () => {
                Animator.SetBool(Moving, true);
                var y = transform.position.y - MovementAmount;
                tween = RigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveUp;
            });
        }

        protected override void OnDie() { }

        private void PlayFlapSound() {
            AudioSource.clip = FlapSound;
            AudioSource.volume = 0.2f;
            AudioSource.Play();
        }
    }
}