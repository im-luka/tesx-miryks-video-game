using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvasFollowCamera : MonoBehaviour
{
    public Transform cameraLook;

    private void LateUpdate() {
        transform.LookAt(transform.position + cameraLook.forward);
    }
}
