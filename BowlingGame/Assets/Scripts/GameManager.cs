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
    private BowlingGame game;

    void Start()
    {
        game = new BowlingGame();
        UpdateGUI();
    }

    public bool CanThrow()
    {
        return !game.IsGameOver;
    }

    public void StartCharging()
    {
        throwStatusPanel.SetActive(false);
    }

    private void UpdateGUI()
    {
        currentFrameText.text = "Frame: " + game.CurrentFrame;
        scoreText.text = "Score: " + game.Score;
        currentRollText.text = "Roll: " + game.CurrentRoll;
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
        game.Roll(pinsKnocked);  
        UpdateGUI();
        Debug.Log("current roll: " + game.CurrentRoll);
        if (game.CurrentRoll <= 10  && (game.CurrentRoll == 1 || game.IsGameOver))
        {
            EndFrame();
        }
        else if (game.CurrentFrame == 10 && game.CurrentRoll == 3)
        {
            EndFrame();
        }
        else {
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
}
