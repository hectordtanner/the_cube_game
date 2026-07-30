using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CheckController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 5;

    [SerializeField]
    private float positionZ;

    [SerializeField]
    private float startY;

    private float positionY;
    private bool leverHeld = false;

    public float checkValue;

    void Awake()
    {
        positionY = startY;
        checkValue = (positionY + 1) / 2;
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
                leverHeld = true;
            }
        }

        if (!(Mouse.current.leftButton.isPressed))
        {
            leverHeld = false;
        }

        if (leverHeld)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
            
                positionY += mouseDelta.y;
                positionY = Mathf.Clamp(positionY, -1, 1);

                transform.position = new Vector3(2, positionY, positionZ);
        }

        checkValue = (positionY + 1) / 2;
    }
}
