using BowlingGameEnums;
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

    void InitializeFrames() {
        AllFrames = Enumerable.Range(1, _gameConfig.MaxFrames)
                        .Select(frameNumber => new BowlingFrame(frameNumber, _gameConfig))
                        .Concat(new[] { new BowlingFrame(_gameConfig.MaxFrames, _gameConfig) })
                        .ToList();
    }

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

    public void Reset() => CurrentFrameIndex = 0;
}
