namespace Game
{
    using UnityEngine;

    public class BackgroundController : MonoBehaviour
    {
        public Camera FollowUpCamera;

        public Vector2 PositionMultiplier;
        public Vector3 OffSet;

        public float LoopX;

        // Update is called once per frame
        private void Update()
        {
            var cameraPos = FollowUpCamera.transform.position;
            var x = cameraPos.x * PositionMultiplier.x + OffSet.x;
            x += Mathf.Floor((cameraPos.x - x + OffSet.x) / LoopX) * LoopX;
            var y = cameraPos.y * PositionMultiplier.y + OffSet.y;
            var z = OffSet.z;
            transform.position = new Vector3(x, y, z);
        }
    }
}