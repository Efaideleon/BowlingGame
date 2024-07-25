using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentFrameText;
    [SerializeField] private TextMeshProUGUI currentRollText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject ReadyPanel;
    [SerializeField] private PinManager pinManager;
    [SerializeField] private Player player;
    [SerializeField] private BowlingViewModel viewModel;

    private void Start()
    {
        viewModel.OnGameStateChange += UpdateGUI;
        viewModel.OnGameOver += HandleGameOver;
        UpdateGUI();
    }

    private void OnDestroy()
    {
        viewModel.OnGameStateChange -= UpdateGUI;
        viewModel.OnGameOver -= HandleGameOver;
    }

    public bool CanThrow => !viewModel.IsGameOver;

    public void StartCharging() => ReadyPanel.SetActive(false);

    private void UpdateGUI()
    {
        currentFrameText.text = "Frame: " + viewModel.CurrentFrame;
        scoreText.text = "Score: " + viewModel.Score;
        currentRollText.text = "Roll: " + viewModel.CurrentRoll;
    }

    public void CheckPinsAfterThrow() => StartCoroutine(WaitAndCountPins());

    private IEnumerator WaitAndCountPins()
    {
        yield return new WaitForSeconds(8f);
        UpdateGameState(pinManager.CountFallenPins());
    }

    private void UpdateGameState(int pinsKnocked)
    {
        viewModel.Roll(pinsKnocked);

        if (viewModel.ShouldResetPins())
        {
            EndFrame();
        }
        else if (viewModel.ShouldRemoveFallenPins())
        {
            pinManager.RemoveFallenPins();
        }
        
        player.HoldBall();
    }

    private void EndFrame()
    {
        ReadyPanel.SetActive(true);
        pinManager.ResetPins();
        player.HoldBall();
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over! Final Score: " + viewModel.Score);
    }
}
