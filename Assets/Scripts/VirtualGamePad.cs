using UnityEngine;

namespace Game {
    public class VirtualGamePad : MonoBehaviour {
        public Vector2 Axis;

        private int up;
        private int down;
        private int right;
        private int left;

        public void PressUp(bool press) {
            up = press ? 1 : 0;
        }

        public void PressDown(bool press) {
            down = press ? -1 : 0;
        }

        public void PressLeft(bool press) {
            left = press ? 1 : 0;
        }

        public void PressRight(bool press) {
            right = press ? -1 : 0;
        }

        public void Update() {
            Axis.x = Input.GetAxisRaw("Horizontal") + (left - right);
            Axis.y = Input.GetAxisRaw("Vertical") + (up - down);
        }
    }
}