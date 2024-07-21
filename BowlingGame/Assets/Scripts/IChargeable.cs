using UnityEngine;

public interface IChargeable
{
    void StartCharging();
    void ReleaseCharge();
    float GetChargePercentage();
}
