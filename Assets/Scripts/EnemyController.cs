namespace Game
{
    using DG.Tweening;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        protected Tween tween;
        protected Rigidbody2D rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        
        public void Die()
        {
            tween?.Kill();
            gameObject.SetActive(false);
        }
    }
}