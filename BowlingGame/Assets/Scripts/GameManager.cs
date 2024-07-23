using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI throwStatusText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject throwStatusPanel;
    [SerializeField] PinsLoader pinsLoader;
    
    private int currentFrame = 1;
    private bool canThrow = true;
    private int playerScore = 0;
    private int fallenPinsThisThrow = 0;

    public static GameManager Instance { get; private set;}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            throwStatusPanel.SetActive(false);
        }
    }

    public void ReleaseThrow()
    {
        canThrow = true;
    }

    private void UpdateGUI()
    {
        roundText.text = "Round: " + currentFrame;
        scoreText.text = "Score: " + playerScore;
        UpdateThrowStatus("Ready!");
    }

    private void UpdateThrowStatus(string status)
    {
        throwStatusText.text = status;
    }

    public void PinFallen()
    {
        fallenPinsThisThrow++;
    }

    public void UpdateGameState()
    {
        if (IsStrike() || fallenPinsThisThrow > 0)
        {
            EndFrame();
            UpdateGUI();
        }
    }

    private bool IsStrike()
    {
        return fallenPinsThisThrow == 10;
    }

    private void EndFrame()
    {
        playerScore += fallenPinsThisThrow;
        fallenPinsThisThrow = 0;
        currentFrame++;
        throwStatusPanel.SetActive(true);
        pinsLoader.ResetPins();
    }
}
