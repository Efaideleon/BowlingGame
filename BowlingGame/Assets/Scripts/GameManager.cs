using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI throwStatusText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int currentRound = 1;
    private int playerScore = 0;
    private bool canThrow = true;

    void Start()
    {
        UpdateGUI();
    }

    public bool CanThrow()
    {
        return canThrow;
    }

    public void StartCharing()
    {
        if (canThrow)
        {
            canThrow = false;
            UpdateThrowStatus("");
        }
    }

    public void ReleaseThrow(int score)
    {
        canThrow = true;
        playerScore += score;
        currentRound++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        roundText.text = "Round: " + currentRound;
        scoreText.text = "Score: " + playerScore;
        UpdateThrowStatus("Ready!");
    }

    private void UpdateThrowStatus(string status)
    {
        throwStatusText.text = status;
    }

}
