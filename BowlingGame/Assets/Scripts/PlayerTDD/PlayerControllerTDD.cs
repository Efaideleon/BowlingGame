using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTDD : MonoBehaviour
{
    private PlayerInputActions _inputActions;

    public delegate void PlayerMoveAction(Vector2 motion);
    public event PlayerMoveAction OnMoveStarted;
    public event PlayerMoveAction OnMoveCancelled;
    public event Action OnChargeStarted;
    public event Action OnChargeCancelled;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Move.started += OnMoveStartedHandler;
        _inputActions.Player.Move.canceled += OnMoveCancelledHandler;
        _inputActions.Player.Charge.started += OnChargeStartedHandler;
        _inputActions.Player.Charge.canceled += OnChargeCancelledHandler;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.started -= OnMoveStartedHandler; 
        _inputActions.Player.Move.canceled -= OnMoveCancelledHandler;
        _inputActions.Player.Charge.started -= OnChargeStartedHandler;
        _inputActions.Player.Charge.canceled -= OnChargeCancelledHandler;
        _inputActions.Player.Disable();
    }

    private void OnMoveStartedHandler(InputAction.CallbackContext context)
    {
        OnMoveStarted?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCancelledHandler(InputAction.CallbackContext context)
    {
        OnMoveCancelled?.Invoke(Vector2.zero);
    }

    private void OnChargeStartedHandler(InputAction.CallbackContext context)
    {
        OnChargeStarted?.Invoke();
    }

    private void OnChargeCancelledHandler(InputAction.CallbackContext context)
    {
        OnChargeCancelled?.Invoke();
    }
}