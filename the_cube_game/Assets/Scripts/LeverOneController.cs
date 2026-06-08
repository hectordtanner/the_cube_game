
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class LeverOneController : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 5;

    private float _positionY = 1;
    private bool _leverHeld = false;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private MeshRenderer _button;

    public float redColorValue;

    [SerializeField]
    private ButtonController _buttonData;

    void Start()
    {
        _material = _button.material;
    }

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

                transform.position = new Vector3(2, _positionY, 0);
        }

        redColorValue = (transform.position.y / 2) + 0.5f;

        if (!_buttonData._overrideColor)
        {
            _material.SetColor("_BaseColor", new Color(redColorValue, 1.0f, 1.0f, 1.0f));
        }
    }
}
