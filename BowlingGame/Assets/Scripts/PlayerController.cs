using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager;
    private PlayerInputActions _playerInputActions;
    private bool _isCharging = false;
    private Vector2 _movementInput;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Charge.started += OnPlayerChargeStarted;
        _playerInputActions.Player.Charge.canceled += OnPlayerChargeCanceled;
        _playerInputActions.Player.Move.started += OnPlayerMoveStarted;
        _playerInputActions.Player.Move.canceled += OnPlayerMoveStarted;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Charge.started -= OnPlayerChargeStarted;
        _playerInputActions.Player.Charge.canceled -= OnPlayerChargeCanceled;
        _playerInputActions.Player.Move.started -= OnPlayerMoveStarted;
        _playerInputActions.Player.Move.canceled -= OnPlayerMoveCanceled;
        _playerInputActions.Player.Disable();
    }

    private void OnPlayerChargeStarted(InputAction.CallbackContext context)
    {
        if (_player != null && _gameManager.CanThrow)
        {
            _player.Charge();
            _isCharging = true;
            _gameManager.StartCharging();
        } 
        else 
        {
            Debug.LogError("Error: Player has not been assigned to PlayerController");
        }
    }

    private void OnPlayerChargeCanceled(InputAction.CallbackContext context)
    {
        if (_player != null && _isCharging)
        {
            _player.Swing();
            _isCharging = false;
        } 
        else 
        {
            Debug.LogError("Error: Player has not been assigned to PlayerController");
        }
    }

    private void OnPlayerMoveStarted(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        _player.Move(_movementInput);
    }

    private void OnPlayerMoveCanceled(InputAction.CallbackContext context)
    {
        _player.Move(Vector2.zero);
    }
}
