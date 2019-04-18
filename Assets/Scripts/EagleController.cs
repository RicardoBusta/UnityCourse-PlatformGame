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
            tween = DOVirtual.DelayedCall(WaitTime, () =>
            {
                var y = transform.position.y + MovementAmount;
                tween = rigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveDown;
            });
        }

        private void MoveDown()
        {
            tween = DOVirtual.DelayedCall(WaitTime, () =>
            {
                var y = transform.position.y - MovementAmount;
                tween = rigidBody.DOMoveY(y, MovementTime);
                tween.onComplete += MoveUp;
            });
        }
    }
}