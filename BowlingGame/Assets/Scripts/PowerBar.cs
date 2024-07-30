using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject powerBarContainer;
    [SerializeField] private BowlingBall bowlingBall;

    void Start()
    {
        powerBarContainer.SetActive(false);
    }

    void Update()
    {
        if (bowlingBall.ChargePercentage > 0)
        {
            SetPowerBarVisibility(true);
            fillImage.fillAmount = bowlingBall.ChargePercentage;
        }
        else 
        {
            SetPowerBarVisibility(false);
            fillImage.fillAmount = 0;
        }
    }

    private void SetPowerBarVisibility(bool isVisible)
    {
        if (powerBarContainer != null)
        {
            powerBarContainer.SetActive(isVisible);
        } 
        else
        {
            Debug.LogWarning("Power bar container is not assigned");
        } 
    }
}
