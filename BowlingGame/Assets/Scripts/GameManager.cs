using UnityEngine;
using TMPro;
using System.Collections;
using player;
using bowling_ball;

public class GameManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _currentFrameText;
    [SerializeField] private TextMeshProUGUI _currentRollText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _readyPanel;
    [SerializeField] private PinManager _pinManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private BowlingViewModel _viewModel;
    [SerializeField] private BowlingBall _bowlingBall;

    private void Start() {
        _viewModel.OnGameStateChange += UpdateGUI;
        _viewModel.OnGameOver += HandleGameOver;
        _pinManager.OnPinsSettled += HandlePinSettled;
        _bowlingBall.OnBallThrown += HandleBallThrown;
        UpdateGUI();
    }

    private void OnDestroy() {
        _viewModel.OnGameStateChange -= UpdateGUI;
        _viewModel.OnGameOver -= HandleGameOver;
        _pinManager.OnPinsSettled -= HandlePinSettled;
        _bowlingBall.OnBallThrown -= HandleBallThrown;
    }

    public bool CanThrow => !_viewModel.IsGameOver;

    public void StartCharging() => _readyPanel.SetActive(false);

    private void UpdateGUI() {
        _currentFrameText.text = "Frame: " + _viewModel.CurrentFrame;
        _scoreText.text = "Score: " + _viewModel.Score;
        _currentRollText.text = "Roll: " + _viewModel.CurrentRoll;
    }

    public void HandleBallThrown() {
        StartCoroutine(WaitAndCountPins());
    }

    private IEnumerator WaitAndCountPins() {
        yield return new WaitForSeconds(8f);
        _pinManager.CheckForPinsToSettle();
    }

    private void HandlePinSettled() {
        Debug.Log("Pins have settled");
        UpdateGameState(_pinManager.CountFallenPins());
    }

    private void UpdateGameState(int pinsKnocked) {
        _viewModel.Roll(pinsKnocked);

        if (_viewModel.ShouldResetPins()) {
            EndFrame();
        }
        else if (_viewModel.ShouldRemoveFallenPins()) {
            _pinManager.RemoveFallenPins();
        }

        _player.Hold();
    }

    private void EndFrame() {
        _readyPanel.SetActive(true);
        _pinManager.ResetPins();
        _player.Hold();
    }

    private void HandleGameOver() {
        Debug.Log("Game Over! Final Score: " + _viewModel.Score);
    }
}
