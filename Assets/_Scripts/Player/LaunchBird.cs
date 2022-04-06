using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchBird : MonoBehaviour
{
    [Header("Controller Info")]
    [SerializeField] private PlayerController controller;

    [Header("Camera Info")]
    [SerializeField] private Camera cam;

    [Header("Bird Launch Info")]
    [SerializeField] private float launchForce;
    private bool _canLaunch;
    private bool _isReadyToLaunch;
    private Vector2 dragStartValue;
    private Vector2 dragEndValue;

    [Header("Raycast Info")]
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        controller.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            DesktopControls();
        }
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            MobileControls();
        }

    }

    private void FixedUpdate()
    {
        if (_canLaunch)
        {
            Launch();
            _isReadyToLaunch = false;
            _canLaunch = false;
        }
    }

    private void DesktopControls()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics2D.Raycast(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, 100, playerLayer))
            {
                dragStartValue = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _isReadyToLaunch = true;
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && _isReadyToLaunch)
        {
            dragEndValue = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _canLaunch = true;
        }
    }

    private void MobileControls()
    {

        if (Touchscreen.current.press.wasPressedThisFrame)
        {
            if (Physics2D.Raycast(cam.ScreenToWorldPoint(Touchscreen.current.position.ReadValue()), Vector2.zero, 100, playerLayer))
            {
                dragStartValue = cam.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
                _isReadyToLaunch = true;
            }
        }
        if (Touchscreen.current.press.wasReleasedThisFrame && _isReadyToLaunch)
        {
            dragEndValue = cam.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
            _canLaunch = true;
        }
    }

    private void Launch()
    {
        Vector2 dragForce = dragStartValue - dragEndValue;
        controller.rb.bodyType = RigidbodyType2D.Dynamic;
        controller.rb.velocity = Vector2.zero;
        controller.rb.AddForce(dragForce * launchForce, ForceMode2D.Impulse);
        dragForce = Vector2.zero;
    }
}
