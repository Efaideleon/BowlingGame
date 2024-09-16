using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI _currentFrameText;
    [SerializeField] TextMeshProUGUI _currentRollText;
    [SerializeField] TextMeshProUGUI _scoreText;

    [Header("UI Panels")]
    [SerializeField] GameObject _readyPanel;
    [SerializeField] GameObject _scoreTable;

    [Header("References")]
    [SerializeField] GameController _bowlingViewModel;
    [SerializeField] InputReader _input;

    void Awake() {
        _input.ActionPerformed += DisablePanels;
    }

    public void UpdateUI(int frame, int roll, int score) {
        _currentRollText.text = "Roll: " + roll;
        _scoreText.text = "Score: " + score;
        _currentFrameText.text = "Frame: " + frame;
    }

    public void ActivatePanels() {
        _readyPanel.SetActive(true);
        _scoreTable.SetActive(true);
    }

    private void DisablePanels() {
        _readyPanel.SetActive(false);
        _scoreTable.SetActive(false);
    }

    void OnDestroy() {
        _input.ActionPerformed -= DisablePanels;
    }
}