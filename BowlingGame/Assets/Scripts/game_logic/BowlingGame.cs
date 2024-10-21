using game_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BowlingGame", menuName = "BowlingBall/BowlingGame")]
public class BowlingGame : ScriptableObject, IBowlingGame {
    [SerializeField] BowlingGameConfig _gameConfig;
    private FrameBonusCalculator _frameBonusCalculator;

    public BowlingGameConfig Config {
        get { return _gameConfig; }
        set { _gameConfig = value; }
    }

    public List<BowlingFrame> AllFrames { get; private set; }
    public int TotalScore => AllFrames.Sum(frame => frame.Score);

    public BowlingFrame CurrentFrame => AllFrames[CurrentFrameIndex];
    public int CurrentFrameIndex { get; private set; } = 0;
    public bool HasGameEnded => CurrentFrameIndex >= _gameConfig.MaxFrames;

    public event Action OnRollCompleted = delegate { };
    public event Action OnGameOver = delegate { };

    public void OnValidate() {
        InitializeFrames();
        _frameBonusCalculator = new FrameBonusCalculator(AllFrames);
    }

    private void InitializeFrames() {
        AllFrames = Enumerable.Range(1, _gameConfig.MaxFrames)
                        .Select(frameNumber => new BowlingFrame(frameNumber, _gameConfig))
                        .Concat(new[] { new BowlingFrame(_gameConfig.MaxFrames, _gameConfig) })
                        .ToList();
    }

    /// <summary>
    /// Records the number of pins knocked after the roll, advances to the next roll.
    /// Triggers the OnRollCompleted event, and advances to the next frame if the frame is finsished.
    /// <summary>
    /// <param name="pinsKnocked">Pins knocked down by the bowling ball after a roll</param>
    public void ProcessRoll(int pinsKnocked) {
        if (HasGameEnded) {
            OnGameOver.Invoke();
            return;
        }

        CurrentFrame.SetNumOfPinsKnocked(pinsKnocked);
        _frameBonusCalculator.CalculateBonus();

        if (CurrentFrame.IsFinished) {
            AdvanceToNextFrame();
        }

        OnRollCompleted.Invoke();
    }

    private void AdvanceToNextFrame() {
        if (CurrentFrameIndex < _gameConfig.MaxFrames) {
            CurrentFrameIndex++;
        }
    }
    
    /// <summary>
    /// Resets the data in the scriptableobject.
    /// <summary>
    public void Reset() => CurrentFrameIndex = 0;
}