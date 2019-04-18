namespace Game
{
    using DG.Tweening;

    public class EagleController : EnemyController
    {
        public float MovementAmount;
        public float MovementTime;
        public float WaitTime;

        private void Start()
        {
            MoveUp();
        }

        private void MoveUp()
        {
            animator.SetBool("Moving", false);
            tween = DOVirtual.DelayedCall(WaitTime, () =>
            {
                animator.SetBool("Moving", true);
                var y = transform.position.y + MovementAmount;
                tween = rigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveDown;
            });
        }

        private void MoveDown()
        {
            animator.SetBool("Moving", false);
            tween = DOVirtual.DelayedCall(WaitTime, () =>
            {
                animator.SetBool("Moving", true);
                var y = transform.position.y - MovementAmount;
                tween = rigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveUp;
            });
        }

        protected override void OnDie()
        {
        }
    }
}