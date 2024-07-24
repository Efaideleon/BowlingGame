using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    private PlayerInputActions playerInputActions;
    private bool isCharging = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Charge.started += OnPlayerChargeStarted;
        playerInputActions.Player.Charge.canceled += OnPlayerChargeCanceled;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Charge.started -= OnPlayerChargeStarted;
        playerInputActions.Player.Charge.canceled -= OnPlayerChargeCanceled;
        playerInputActions.Player.Disable();
    }

    private void OnPlayerChargeStarted(InputAction.CallbackContext context)
    {
        if (player != null && gameManager.CanThrow())
        {
            player.StartCharging();
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
            player.ReleaseCharge();
            isCharging = false;
        } 
        else 
        {
            Debug.LogError("Error: Player has not been assigned to PlayerController");
        }
    }
}
