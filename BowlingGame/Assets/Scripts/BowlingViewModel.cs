using System;
using UnityEngine;

public class BowlingViewModel : MonoBehaviour
{
    private BowlingGame _game;

    public event Action OnGameStateChange;
    public event Action OnGameOver;

    public int CurrentFrame => _game.CurrentFrame;
    public int CurrentRoll => _game.CurrentRoll;
    public int Score => _game.Score;
    public bool IsGameOver => _game.IsGameOver;

    private void Awake()
    {
        _game = new BowlingGame();
    }

    public void Roll(int pinsKnocked)
    {
        _game.Roll(pinsKnocked);
        OnGameStateChange?.Invoke();

        if (_game.IsGameOver)
        {
            OnGameOver.Invoke();
        }
    }

    public bool ShouldResetPins()
    {
        return _game.CurrentRoll == 1 || (_game.CurrentFrame == 10 && _game.CurrentRoll == 3);
    }

    public bool ShouldRemoveFallenPins()
    {
        return !ShouldResetPins() && !_game.IsGameOver;
    }
}
