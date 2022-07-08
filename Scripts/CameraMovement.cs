using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform centerPoint;
    public Transform cameraView;
    public float cameraDistance, cameraHeight, cameraSpeed, cameraVerticalSpeed;

    private void Update() {
        centerPoint.position = transform.position + new Vector3(0f, 1.5f, 0f);
        centerPoint.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime, 0f);
        cameraHeight += Input.GetAxis("Mouse Y") * -1 * cameraVerticalSpeed * Time.deltaTime;
        if(cameraHeight < -2) cameraHeight = -2;
        if(cameraHeight > 2) cameraHeight = 2;
    }

    private void LateUpdate() {
        cameraView.position = centerPoint.position + (centerPoint.forward * cameraDistance * -1) + Vector3.up * cameraHeight;
        cameraView.LookAt(centerPoint);
    }

}
