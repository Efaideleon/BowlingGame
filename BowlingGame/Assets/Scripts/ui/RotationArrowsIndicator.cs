using UnityEngine;

public class RotationArrowsIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftArrow;
    [SerializeField]
    private GameObject _rightArrow;
    [SerializeField]
    private InputReader _inputReader;

    void OnEnable()
    {
        _inputReader.OnToggleStarted += ToggleArrowRotation;
    }

    void ToggleArrowRotation()
    {
        Debug.Log("Toggling arrows");
        _leftArrow.SetActive(!_leftArrow.activeSelf);
        _rightArrow.SetActive(!_rightArrow.activeSelf);
    }
}