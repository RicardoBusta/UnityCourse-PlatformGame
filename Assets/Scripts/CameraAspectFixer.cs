using UnityEngine;

public class CameraAspectFixer : MonoBehaviour {
    public Camera Camera;

    public float TargetAspect = 16f / 9f;

    private float originalSize;

    private void Start() {
        originalSize = Camera.orthographicSize;
        CalculateAspectRatio();
    }

    private void CalculateAspectRatio() {
        var screenRatio = Screen.width / (float) Screen.height;
        if (Screen.width / (float) Screen.height < TargetAspect) {
            Camera.orthographicSize = originalSize * TargetAspect / screenRatio;
        } else {
            Camera.orthographicSize = originalSize;
        }
    }
}