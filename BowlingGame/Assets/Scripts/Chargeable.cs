using UnityEngine;

public abstract class Chargeable : MonoBehaviour, IChargeable
{
    public abstract void StopCharging();
    public abstract void StartCharging();
    public abstract float GetChargePercentage();
}
