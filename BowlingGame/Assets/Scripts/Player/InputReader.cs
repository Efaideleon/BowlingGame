using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// This class reads from the player inputs to invoke an event action.
/// </summary>
[CreateAssetMenu(fileName = "InputReader", menuName = "BowlingBall/InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions {
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction ChargeStarted = delegate { };
    public event UnityAction ChargeFinished = delegate { };

    private PlayerInputActions _inputActions;

    public Vector2 Direction => _inputActions.Player.Move.ReadValue<Vector2>();

    void OnEnable() {
        if (_inputActions == null) {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }
        _inputActions.Enable();
        _inputActions.Player.Enable();
    }

    void OnDisable() {
        _inputActions.Player.RemoveCallbacks(this);
        _inputActions.Player.Disable();
        _inputActions.Disable();
    }

    public void OnCharge(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Started:
                ChargeStarted.Invoke();
                break;
            case InputActionPhase.Canceled:
                ChargeFinished.Invoke();
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        Move.Invoke(context.ReadValue<Vector2>());
    }
}