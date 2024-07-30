using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    private PlayerInputActions playerInputActions;
    private bool isCharging = false;
    private Vector2 movementInput;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Charge.started += OnPlayerChargeStarted;
        playerInputActions.Player.Charge.canceled += OnPlayerChargeCanceled;
        playerInputActions.Player.Move.started += OnPlayerMoveStarted;
        playerInputActions.Player.Move.canceled += OnPlayerMoveStarted;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Charge.started -= OnPlayerChargeStarted;
        playerInputActions.Player.Charge.canceled -= OnPlayerChargeCanceled;
        playerInputActions.Player.Move.started -= OnPlayerMoveStarted;
        playerInputActions.Player.Move.canceled -= OnPlayerMoveCanceled;
        playerInputActions.Player.Disable();
    }

    private void OnPlayerChargeStarted(InputAction.CallbackContext context)
    {
        if (player != null && gameManager.CanThrow)
        {
            player.Charge();
            isCharging = true;
            gameManager.StartCharging();
        } 
        else 
        {
            Debug.LogError("Error: Player has not been assigned to PlayerController");
        }
    }

    private void OnPlayerChargeCanceled(InputAction.CallbackContext context)
    {
        if (player != null && isCharging)
        {
            player.Swing();
            isCharging = false;
        } 
        else 
        {
            Debug.LogError("Error: Player has not been assigned to PlayerController");
        }
    }

    private void OnPlayerMoveStarted(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        player.Move(movementInput);
    }

    private void OnPlayerMoveCanceled(InputAction.CallbackContext context)
    {
        player.Move(Vector2.zero);
    }
}
