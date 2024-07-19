using UnityEngine;

public abstract class Chargeable : MonoBehaviour, IChargeable
{
    public abstract void ReleaseCharge();
    public abstract void StartCharging();
    public abstract float GetChargePercentage();
}
