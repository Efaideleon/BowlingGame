using UnityEngine;
using UnityEngine.UI;
using player;

public class PowerBar : MonoBehaviour {
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _powerBarContainer;
    [SerializeField] private Material _material; // Ensure this is assigned in the Inspector
    [SerializeField] PlayerController _playerController;

    private float ChargePercentage => _playerController.ChargedThrowAction.ChargePercentage;

    void Start() {
        _powerBarContainer.SetActive(false);
    }

    void Update() {
        if (ChargePercentage > 0) {
            SetPowerBarVisibility(true);
            _fillImage.fillAmount = ChargePercentage;
            _material.SetFloat("_GradientHue", ChargePercentage);
        }
        else {
            SetPowerBarVisibility(false);
            _fillImage.fillAmount = 0;
        }
    }

    private void SetPowerBarVisibility(bool isVisible) {
        if (_powerBarContainer != null) {
            _powerBarContainer.SetActive(isVisible);
        }
        else {
            Debug.LogWarning("Power bar container is not assigned");
        }
    }
}