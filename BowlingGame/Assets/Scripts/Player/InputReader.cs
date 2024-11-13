using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// This class reads from the player inputs to invoke an event action.
/// </summary>
[CreateAssetMenu(fileName = "InputReader", menuName = "BowlingBall/InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction ChargeStarted = delegate { };
    public event UnityAction ChargeFinished = delegate { };
    public event UnityAction ActionPerformed = delegate { };
    public event UnityAction OnMenuOpen = delegate { };
    public event UnityAction OnMenuClosed = delegate { };
    public event UnityAction OnToggleStarted = delegate { };

    private PlayerInputActions _inputActions;

    public Vector2 Direction => _inputActions.Player.Move.ReadValue<Vector2>();

    private int instanceCount = 0;
    void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }
        _inputActions.Enable();
        _inputActions.Player.Enable();
        Debug.Log($"InputReader enabled. Instance count: {instanceCount++}");
    }

    void OnDisable()
    {
        Debug.Log($"InputReader destroyed. Instance count: {instanceCount--}");
        _inputActions.Player.RemoveCallbacks(this);
        _inputActions.Player.Disable();
        _inputActions.Disable();
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                ChargeStarted.Invoke();
                break;
            case InputActionPhase.Canceled:
                ChargeFinished.Invoke();
                break;
        }
        ActionPerformed.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
        ActionPerformed.Invoke();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnMenuOpen.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnMenuClosed.Invoke();
                break;
        }
    }

    public void OnToggle(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnToggleStarted.Invoke();
                break;
        }
    }
}