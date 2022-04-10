using UnityEngine;

public class InputControls : MonoBehaviour
{
    [Header("InputAction Info")]
    private InputActions inputActions;

    [Header("Drag Input Info")]
    public bool isLeftMousePressed;
    public float leftMouseInput;
    public Vector2 mousePosition;

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.LeftMouse.started += ctx => leftMouseInput = ctx.ReadValue<float>();
        inputActions.Player.LeftMouse.performed += ctx => leftMouseInput = ctx.ReadValue<float>();
        inputActions.Player.LeftMouse.canceled += ctx => leftMouseInput = ctx.ReadValue<float>();
    }

    private void Update()
    {
        isLeftMousePressed = inputActions.Player.LeftMouse.triggered;

        if (leftMouseInput > 0)
        {
            mousePosition = inputActions.Player.Position.ReadValue<Vector2>();
        }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
