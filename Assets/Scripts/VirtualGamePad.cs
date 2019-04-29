using UnityEngine;

namespace Game
{
    using System;

    public class VirtualGamePad : MonoBehaviour
    {
        private Vector2 previousAxis = Vector2.zero;

        private float up;
        private float down;
        private float right;
        private float left;

        public event Action<Vector2> UpdateControl;

        public void PressUp(bool press)
        {
            up = press ? 1 : 0;
        }

        public void PressDown(bool press)
        {
            down = press ? -1 : 0;
        }

        public void PressLeft(bool press)
        {
            left = press ? -1 : 0;
        }

        public void PressRight(bool press)
        {
            right = press ? 1 : 0;
        }

        public void Update()
        {
            var Axis = new Vector2(
                Mathf.Clamp(Input.GetAxisRaw("Horizontal") + (left + right), -1, 1),
                Mathf.Clamp(Input.GetAxisRaw("Vertical") + (up + down), -1, 1)
            );

            if (Axis != previousAxis)
            {
                UpdateControl?.Invoke(Axis);
            }

            previousAxis = Axis;
        }
    }
}