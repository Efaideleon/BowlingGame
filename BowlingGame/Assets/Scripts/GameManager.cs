using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI throwStatusText;
    [SerializeField] TextMeshProUGUI scoreText;
    private int currentRound = 1;
    private bool canThrow = true;
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
        }
    }

    public void ReleaseThrow()
    {
        canThrow = true;
        currentRound++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        roundText.text = "Round: " + currentRound;
        scoreText.text = "Score: " + fallenPinsThisThrow;
        UpdateThrowStatus("Ready!");
    }

    private void UpdateThrowStatus(string status)
    {
        throwStatusText.text = status;
    }

    public void PinFallen()
    {
        fallenPinsThisThrow++;
        UpdateGUI();
    }
}
