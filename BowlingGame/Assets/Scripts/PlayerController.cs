using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Chargeable bowlingBall;
    private PlayerInputActions playerInputActions;

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
        if (bowlingBall != null)
        {
            bowlingBall.StartCharging();
        } 
        else 
        {
            Debug.LogError("Error: bowlingBall has not been assigned to PlayerController");
        }
    }

    private void OnPlayerChargeCanceled(InputAction.CallbackContext context)
    {
        if (bowlingBall != null)
        {
            bowlingBall.ReleaseCharge();
        } 
        else 
        {
            Debug.LogError("Error: bowlingBall has not been assigned to PlayerController");
        }

    }
}
