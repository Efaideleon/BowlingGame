using System;
using UnityEngine;

public class BowlingViewModel : MonoBehaviour
{
    private BowlingGame game;

    public event Action OnGameStateChange;
    public event Action OnGameOver;

    public int CurrentFrame => game.CurrentFrame;
    public int CurrentRoll => game.CurrentRoll;
    public int Score => game.Score;
    public bool IsGameOver => game.IsGameOver;

    private void Awake()
    {
        game = new BowlingGame();
    }

    public void Roll(int pinsKnocked)
    {
        game.Roll(pinsKnocked);
        OnGameStateChange?.Invoke();

        if (game.IsGameOver)
        {
            OnGameOver.Invoke();
        }
    }

    public bool ShouldResetPins()
    {
        return game.CurrentRoll == 1 || (game.CurrentFrame == 10 && game.CurrentRoll == 3);    
    }

    public bool ShouldRemoveFallenPins()
    {
        return !ShouldResetPins() && !game.IsGameOver;
    }
}
