using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _controlsPanel;
    [SerializeField] private InputReader _inputReader;

    void OnEnable() {
        _inputReader.OnMenuOpen += ToggleControlsPanel;
        _inputReader.ActionPerformed += DisableControlsPanel;
    }

    void OnDisable() {
        _inputReader.OnMenuOpen -= ToggleControlsPanel;
        _inputReader.ActionPerformed -= DisableControlsPanel;
    }

    private void ToggleControlsPanel() => _controlsPanel.SetActive(!_controlsPanel.activeSelf);
    private void DisableControlsPanel() => _controlsPanel.SetActive(false);
}