using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _powerBarContainer;
    [SerializeField] private BowlingBall _bowlingBall;
    [SerializeField] private Material _material; // Ensure this is assigned in the Inspector

    void Start()
    {
        _powerBarContainer.SetActive(false);
        

    }

    void Update()
    {
        
        if (_bowlingBall.ChargePercentage > 0)
        {
            SetPowerBarVisibility(true);
            _fillImage.fillAmount = _bowlingBall.ChargePercentage;
            _material.SetFloat("_GradientHue", _bowlingBall.ChargePercentage);
        }
        else 
        {
            SetPowerBarVisibility(false);
            _fillImage.fillAmount = 0;
        }
    }

    private void SetPowerBarVisibility(bool isVisible)
    {
        if (_powerBarContainer != null)
        {
            _powerBarContainer.SetActive(isVisible);
        } 
        else
        {
            Debug.LogWarning("Power bar container is not assigned");
        } 
    }
}
