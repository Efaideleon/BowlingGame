using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Chargeable bowlingBall;

    void Update()
    {
        if (bowlingBall != null)
        {
            HandleInput();
        } 
        else 
        {
            Debug.LogError("Error: bowlingBall has not been assigned to PlayerController");
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bowlingBall.StartCharging();
        }
        // Player lets go of the space bar or power is fully charged.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            bowlingBall.ReleaseCharge();
        }
    }
}
