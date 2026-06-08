using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{   
    [SerializeField]
    private float _mouseSensitivity = 1.0f;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _cameraDistance = 5;

    private float _rotationX;
    private float _rotationY;

    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * _mouseSensitivity;
            
            _rotationY += mouseDelta.x;
            _rotationX += -1 * mouseDelta.y;
        }
        _rotationX = Mathf.Clamp(_rotationX, -90, 90);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        transform.position = _target.transform.position - transform.forward * _cameraDistance;
    }
}
