using UnityEngine;

public interface IChargeable
{
    void StartCharging();
    void StopCharging();
    float GetChargePercentage();
}
