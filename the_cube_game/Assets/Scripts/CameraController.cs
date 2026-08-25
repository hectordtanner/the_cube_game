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

    public float rotationX = 25;
    public float rotationY = -40;

    private Vector3 localEulerAngles = new Vector3(25, -40, 0);
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private PauseMenuController menuData;

    void Update()
    {
        if (Mouse.current.rightButton.isPressed && !menuData.isMenuOpen)
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
