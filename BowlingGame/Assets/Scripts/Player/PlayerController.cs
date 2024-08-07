using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public delegate void PlayerAction(InputAction.CallbackContext context);
    public event PlayerAction OnChargeStarted;
    public event PlayerAction OnChargeCancelled;
    public event PlayerAction OnMoveStarted;
    public event PlayerAction OnMoveCancelled;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Charge.started += ctx => OnChargeStarted?.Invoke(ctx);
        _playerInputActions.Player.Charge.canceled += ctx => OnChargeCancelled?.Invoke(ctx);
        _playerInputActions.Player.Move.started += ctx => OnMoveStarted?.Invoke(ctx);
        _playerInputActions.Player.Move.canceled += ctx => OnMoveCancelled?.Invoke(ctx);
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Charge.started -= ctx => OnChargeStarted?.Invoke(ctx);
        _playerInputActions.Player.Charge.canceled -= ctx => OnChargeCancelled?.Invoke(ctx);
        _playerInputActions.Player.Move.started -= ctx => OnMoveStarted?.Invoke(ctx);
        _playerInputActions.Player.Move.canceled -= ctx => OnMoveCancelled?.Invoke(ctx);
        _playerInputActions.Player.Disable();
    }
}
