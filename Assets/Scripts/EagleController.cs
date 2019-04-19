using NaughtyAttributes;
using UnityEngine;

namespace Game {
    using DG.Tweening;

    public class EagleController : MonoBehaviour {
        private static readonly int Moving = Animator.StringToHash("Moving");

        [BoxGroup("References")]
        [Required]
        public EnemyController EnemyController;

        [BoxGroup("Assets")]
        [Required]
        public AudioClip FlapSound;

        [BoxGroup("Parameters")]
        public float MovementAmount;

        [BoxGroup("Parameters")]
        public float MovementTime;

        [BoxGroup("Parameters")]
        public float WaitTime;

        private Tween tween;

        private void Start() {
            MoveUp();

            EnemyController.DieEvent += () => tween?.Kill();
        }

        private void MoveUp() {
            EnemyController.Animator.SetBool(Moving, false);
            tween = DOVirtual.DelayedCall(WaitTime, () => {
                EnemyController.Animator.SetBool(Moving, true);
                var y = transform.position.y + MovementAmount;
                tween = EnemyController.RigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveDown;
            });
        }

        private void MoveDown() {
            EnemyController.Animator.SetBool(Moving, false);
            tween = DOVirtual.DelayedCall(WaitTime, () => {
                EnemyController.Animator.SetBool(Moving, true);
                var y = transform.position.y - MovementAmount;
                tween = EnemyController.RigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveUp;
            });
        }

        private void PlayFlapSound() {
            EnemyController.PlaySound(FlapSound, 0.2f);
        }
    }
}