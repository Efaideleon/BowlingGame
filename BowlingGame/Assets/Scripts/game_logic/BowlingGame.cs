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

    public List<BowlingFrame> AllFrames { get; } = new();
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
        if (AllFrames.Count == 0) {
            for (int frameNumber = 1; frameNumber <= _gameConfig.MaxFrames - 1; frameNumber++) {
                AllFrames.Add(new BowlingFrame(frameNumber, _gameConfig, 2));
            }
            // The last frames has 3 rolls. The frame number is the total number of frames.
            AllFrames.Add(new BowlingFrame(_gameConfig.MaxFrames, _gameConfig, 3));
        }
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
        } else {
            MoveToNextRollInCurrentFrame();
        }

        OnRollCompleted.Invoke();
    }

    private void AdvanceToNextFrame() {
        if (CurrentFrameIndex < _gameConfig.MaxFrames) {
            CurrentFrameIndex++;
        }
    }

    private void MoveToNextRollInCurrentFrame() => CurrentFrame.MoveToNextRoll();
    public void Reset() => CurrentFrameIndex = 0;
}
