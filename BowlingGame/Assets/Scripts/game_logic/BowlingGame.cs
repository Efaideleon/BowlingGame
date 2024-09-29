using BowlingGameEnums;
using System;
using System.Collections.Generic;

public class BowlingGame {
    readonly IBowlingGameConfig _config;
    List<BowlingFrame> _frames = new();

    public List<BowlingFrame> Frames => _frames;
    public int TotalScore { get; private set; } = 0;
    public int CurrentFrameIndex { get; private set; } = 1;
    public RollNumber CurrentRoll { get; private set; } = RollNumber.First;
    public bool IsGameOver => CurrentFrameIndex > _config.MaxFrames;

    public event Action OnRollCompleted = delegate { };
    public event Action OnGameOver = delegate { };

    public BowlingGame(IBowlingGameConfig config) {
        _config = config;

        for (int i = 1; i <= _config.MaxFrames; i++) {
            _frames.Add(new BowlingFrame(i, config));
        }
    }

    public void Roll(int pinsKnocked) {
        if (IsGameOver) {
            OnGameOver.Invoke();
            return;
        }

        if (pinsKnocked < 0 || pinsKnocked > _config.MaxPins) {
            throw new ArgumentException("Invalid number of pins knocked down.");
        }

        var frame = _frames[CurrentFrameIndex - 1];
        frame.RecordRollScore(CurrentRoll, pinsKnocked);

        if (CurrentFrameIndex < _config.MaxFrames) {
            HandleRegularFrameRoll(pinsKnocked);
        }
        else {
            HandleFinalFrameRoll(pinsKnocked);
        }

        CalculateFrameScores();
        OnRollCompleted.Invoke();
    }


    void HandleRegularFrameRoll(int pinsKnocked) {
        if (CurrentRoll == RollNumber.First && pinsKnocked == _config.MaxPins) {
            MoveToNextFrame();
        }
        else if (CurrentRoll == RollNumber.Second) {
            MoveToNextFrame();
        }
        else {
            CurrentRoll++;
        }
    }

    void HandleFinalFrameRoll(int pinsKnocked) {
        if (CurrentRoll == RollNumber.First || CurrentRoll == RollNumber.Second) {
            CurrentRoll++;
        }
        else {
            MoveToNextFrame();
        }
    }

    void MoveToNextFrame() {
        CurrentFrameIndex++;
        CurrentRoll = RollNumber.First;
    }

    void CalculateFrameScores() {
        TotalScore = 0;

        for (int i = 0; i < _config.MaxFrames; i++) {
            var frame = _frames[i];
            frame.Bonus = CalculateBonus(i);
            TotalScore += frame.Score;
        }
    }

    int CalculateBonus(int currentFrameIndex) {
        if (currentFrameIndex >= _config.MaxFrames - 1) return 0;

        var frame = _frames[currentFrameIndex];
        var nextFrame = _frames[currentFrameIndex + 1];

        return frame.IsStrike()
                ? (nextFrame.FirstRollScore ?? 0) + (nextFrame.SecondRollScore ?? 0)
                : frame.IsSpare()
                    ? nextFrame.FirstRollScore ?? 0
                    : 0;
    }

    public bool IsLastRoll() => CurrentRoll == RollNumber.First || (CurrentFrameIndex == _config.MaxFrames && CurrentRoll == RollNumber.Third);
}
