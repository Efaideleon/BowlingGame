using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentFrameText;
    [SerializeField] private TextMeshProUGUI currentRollText;
    [SerializeField] private TextMeshProUGUI throwStatusText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject throwStatusPanel;
    [SerializeField] private PinManager pinManager;
    [SerializeField] private BowlingBall bowlingBall;
    [SerializeField] private BowlingViewModel viewModel;

    private void Start()
    {
        viewModel.OnGameStateChange += UpdateGUI;
        viewModel.OnGameOver += HandleGameOver;
        UpdateGUI();
    }

    public bool CanThrow() => !viewModel.IsGameOver;

    public void StartCharging()
    {
        throwStatusPanel.SetActive(false);
    }

    private void UpdateGUI()
    {
        currentFrameText.text = "Frame: " + viewModel.CurrentFrame;
        scoreText.text = "Score: " + viewModel.Score;
        currentRollText.text = "Roll: " + viewModel.CurrentRoll;
        UpdateThrowStatus("Ready!");
    }

    private void UpdateThrowStatus(string status)
    {
        throwStatusText.text = status;
    }

    public void CheckPinsAfterThrow()
    {
        StartCoroutine(WaitAndCountPins());
    }

    private IEnumerator WaitAndCountPins()
    {
        yield return new WaitForSeconds(8f);

        int pinsKnocked = pinManager.CountFallenPins();
        UpdateGameState(pinsKnocked);
    }

    private void UpdateGameState(int pinsKnocked)
    {
        viewModel.Roll(pinsKnocked);

        if (viewModel.ShouldResetPins())
        {
            EndFrame();
        }
        else if (viewModel.ShoudRemoveFallenPins())
        {
            pinManager.RemoveFallenPins();
        }

        bowlingBall.ResetBall();
    }

    private void EndFrame()
    {
        throwStatusPanel.SetActive(true);
        pinManager.ResetPins();
        bowlingBall.ResetBall();
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over! Final Score: " + viewModel.Score);
    }

    private void OnDestroy()
    {
        viewModel.OnGameStateChange -= UpdateGUI;
        viewModel.OnGameOver -= HandleGameOver;
    }
}
