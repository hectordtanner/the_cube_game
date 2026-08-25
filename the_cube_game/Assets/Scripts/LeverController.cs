using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;

public class CheckController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 5;

    [SerializeField]
    public float positionZ;

    [SerializeField]
    private float startY;

    private float positionY;
    private bool leverHeld = false;

    public float checkValue;

    [SerializeField]
    private PauseMenuController menuData;

    [SerializeField]
    private List<CheckController> otherLevers = new List<CheckController>();

    void Awake()
    {
        positionY = startY;
        checkValue = (positionY + 1) / 2;
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed && !menuData.isMenuOpen)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (otherLevers.All(otherLever => !otherLever.leverHeld))
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
