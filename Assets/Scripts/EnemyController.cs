using NaughtyAttributes;

namespace Game {
    using DG.Tweening;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class EnemyController : MonoBehaviour {
        private static readonly int Die1 = Animator.StringToHash("Die");

        protected Tween tween;

        [BoxGroup("ScriptReferences")]
        [Required]
        public Rigidbody2D RigidBody;

        [BoxGroup("ScriptReferences")]
        [Required]
        public Animator Animator;

        [BoxGroup("ScriptReferences")]
        [Required]
        public Collider2D Collider;

        [BoxGroup("ScriptReferences")]
        [Required]
        public AudioSource AudioSource;
        
        [BoxGroup("Assets")]
        [Required]
        public AudioClip DeathSound;

        protected abstract void OnDie();

        public void Die() {
            gameObject.SetActive(false);
        }

        public bool Hit(ContactPoint2D[] contactPoints) {
            var topHit = true;
            foreach (var contact in contactPoints) {
                if (Vector2.Dot(contact.normal, Vector2.up) < 0.7f) {
                    topHit = false;
                    break;
                }
            }

            if (topHit) {
                Collider.enabled = false;
                tween?.Kill();
                OnDie();
                PlayDeathSound();
                Animator.SetTrigger(Die1);
            }

            return topHit;
        }

        private void PlayDeathSound() {
            AudioSource.clip = DeathSound;
            AudioSource.volume = 0.7f;
            AudioSource.Play();
        }
    }
}