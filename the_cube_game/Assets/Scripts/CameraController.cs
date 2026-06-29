using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{   
    [SerializeField]
    private float mouseSensitivity = 1.0f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float cameraDistance = 5;

    [SerializeField]
    private float smoothTime = 0.1f;

    private float rotationX;
    private float rotationY;

    private Vector3 localEulerAngles = new Vector3(0, 0, 0);
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
            
            rotationY += mouseDelta.x;
            rotationX += -1 * mouseDelta.y;
        }
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        localEulerAngles = Vector3.SmoothDamp(localEulerAngles, new Vector3(rotationX, rotationY, 0), ref velocity, smoothTime);

        transform.localEulerAngles = localEulerAngles; 
        transform.position = target.transform.position - transform.forward * cameraDistance;
    }
}
