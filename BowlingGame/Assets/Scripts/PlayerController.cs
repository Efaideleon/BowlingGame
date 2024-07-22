using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Chargeable bowlingBall;
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
        if (bowlingBall != null && gameManager.CanThrow())
        {
            bowlingBall.StartCharging();
            isCharging = true;
            gameManager.StartCharing();
        } 
        else 
        {
            Debug.LogError("Error: bowlingBall has not been assigned to PlayerController");
        }
    }

    private void OnPlayerChargeCanceled(InputAction.CallbackContext context)
    {
        if (bowlingBall != null && isCharging)
        {
            bowlingBall.ReleaseCharge();
            gameManager.ReleaseThrow(1); // How do we calculate the score?
            isCharging = false;
        } 
        else 
        {
            Debug.LogError("Error: bowlingBall has not been assigned to PlayerController");
        }

    }
}
