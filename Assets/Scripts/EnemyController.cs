namespace Game
{
    using DG.Tweening;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Collider2D))]
    public abstract class EnemyController : MonoBehaviour
    {
        private static readonly int Die1 = Animator.StringToHash("Die");
        
        protected Tween tween;
        protected Rigidbody2D rigidBody;
        protected Animator animator;
        protected Collider2D collider;
        
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            collider = GetComponent<Collider2D>();
        }

        protected abstract void OnDie();
        
        public void Die()
        {   
            gameObject.SetActive(false);
        }

        public bool Hit(ContactPoint2D[] contactPoints)
        {
            var topHit = true;
            foreach (var contact in contactPoints)
            {
                if (Vector2.Dot(contact.normal, Vector2.up) < 0.7f)
                {
                    topHit = false;
                    break;
                }
            }

            if (topHit)
            {
                collider.enabled = false;
                tween?.Kill();
                OnDie();
                animator.SetTrigger(Die1);
            }

            return topHit;
        }
    }
}