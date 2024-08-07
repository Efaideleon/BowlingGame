using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerActions _actions;
    private PlayerController _controller;

    public PlayerActions Actions => _actions;

    void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _movement = GetComponent<PlayerMovement>();
        _actions = GetComponent<PlayerActions>();
    }

    private void OnEnable()
    {
        _controller.OnChargeStarted += HandleChargeStarted;
        _controller.OnChargeCancelled += HandleChargeCancelled;
        _controller.OnMoveStarted += HandleMoveStarted;
        _controller.OnMoveCancelled += HandleMoveCancelled;
    }

    private void OnDisable()
    {
        _controller.OnChargeStarted -= HandleChargeStarted;
        _controller.OnChargeCancelled -= HandleChargeCancelled;
        _controller.OnMoveStarted -= HandleMoveStarted;
        _controller.OnMoveCancelled -= HandleMoveCancelled;
    }

    private void HandleChargeStarted(InputAction.CallbackContext context)
    {
        Actions.Charge();
    }

    private void HandleChargeCancelled(InputAction.CallbackContext context)
    {
        Actions.Swing();
    }

    private void HandleMoveStarted(InputAction.CallbackContext context)
    {
        Vector2 _movementInput = context.ReadValue<Vector2>();
        _movement.Move(_movementInput);
    }

    private void HandleMoveCancelled(InputAction.CallbackContext context)
    {
        _movement.Move(Vector2.zero);
    }
}
