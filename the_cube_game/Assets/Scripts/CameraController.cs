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

    [SerializeField]
    private float _smoothTime = 0.1f;

    private float _rotationX;
    private float _rotationY;

    private Vector3 _localEulerAngles = new Vector3(0, 0, 0);
    private Vector3 _velocity = Vector3.zero;

    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * _mouseSensitivity;
            
            _rotationY += mouseDelta.x;
            _rotationX += -1 * mouseDelta.y;
        }
        _rotationX = Mathf.Clamp(_rotationX, -90, 90);

        _localEulerAngles = Vector3.SmoothDamp(_localEulerAngles, new Vector3(_rotationX, _rotationY, 0), ref _velocity, _smoothTime);

        transform.localEulerAngles = _localEulerAngles; 
        transform.position = _target.transform.position - transform.forward * _cameraDistance;
    }
}
