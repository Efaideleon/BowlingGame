using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour, IChargeable
{
    [SerializeField] Image fillImage;
    [SerializeField] float maxChargeTime = 2f;
    private float currentCharge;
    private bool isCharging = false;
    private readonly float chargingSpeed = 5f;

    void Update()
    {
        if (isCharging)
        {
            currentCharge = Mathf.Lerp(currentCharge, maxChargeTime, Time.deltaTime * chargingSpeed);
            fillImage.fillAmount = currentCharge / maxChargeTime;
        }        
    }

    public void StartCharging()
    {
        isCharging = true;
        currentCharge = 0f;
    }

    public void ReleaseCharge()
    {
        isCharging = false;
        currentCharge = 0f;
        fillImage.fillAmount = 0f;
    }
}
