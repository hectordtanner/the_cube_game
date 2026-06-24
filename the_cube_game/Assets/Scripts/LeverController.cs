using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CheckController : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 5;

    [SerializeField]
    private float _positionZ;

    private float _positionY = 1;
    private bool _leverHeld = false;

    public float _checkValue;

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                _leverHeld = true;
            }
        }

        if (!(Mouse.current.leftButton.isPressed))
        {
            _leverHeld = false;
        }

        if (_leverHeld)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * _mouseSensitivity;
            
                _positionY += mouseDelta.y;
                _positionY = Mathf.Clamp(_positionY, -1, 1);

                transform.position = new Vector3(2, _positionY, _positionZ);
        }

        _checkValue = (transform.position.y + 1) / 2;
    }
}
