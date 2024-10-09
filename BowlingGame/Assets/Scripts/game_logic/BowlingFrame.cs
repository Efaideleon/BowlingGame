using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using BowlingGameEnums;
using UnityEngine;

public class BowlingFrame {
    readonly IBowlingGameConfig _gameConfig;
    private int? _firstRollScore;
    private int? _secondRollScore;
    private int? _thirdRollScore;
    private int _frameNumber;
    private List<BowlingFrame> _frames;
    private int _bonus = 0;

    public RollNumber CurrentRoll { get; private set; } = RollNumber.First;
    public bool IsFinished { get; private set; } = false;

    // Starts from 1.
    public int FrameNumber {
        get { return _frameNumber; }
        private set {
            if (value < 0) {
                throw new ArgumentOutOfRangeException(nameof(value), "frame number must be greater or equal to 0.");
            }
            _frameNumber = value;
        }
    }

    public int? FirstRollScore {
        get { return _firstRollScore; }
        private set { _firstRollScore = value < 0 ? null : value; }
    }

    public int? SecondRollScore {
        get { return _secondRollScore; }
        private set { _secondRollScore = value < 0 ? null : value; }
    }

    public int? ThirdRollScore {
        get { return _thirdRollScore; }
        private set { _thirdRollScore = value < 0 ? null : value; }
    }

    public int Score => (FirstRollScore ?? 0) + (SecondRollScore ?? 0) + (ThirdRollScore ?? 0) + _bonus;

    public BowlingFrame(int frame, IBowlingGameConfig config, List<BowlingFrame> frames) {
        FrameNumber = frame;
        _gameConfig = config;
        _frames = frames;
    }

    public void UpdateRollScore(int pinsKnocked) {
        if (pinsKnocked < 0 || pinsKnocked > _gameConfig.MaxPins) {
            throw new ArgumentOutOfRangeException(
                nameof(pinsKnocked), "pinsKnocked must be between 0 and " + _gameConfig.MaxPins
            );
        }
        switch (CurrentRoll) {
            case RollNumber.First:
                FirstRollScore = pinsKnocked;
                if (IsStrike && !IsLastFrame) EndFrame();
                break;
            case RollNumber.Second:
                SecondRollScore = pinsKnocked;
                if (!IsLastFrame) EndFrame();
                break;
            case RollNumber.Third:
                if (!IsLastFrame) {
                    throw new InvalidOperationException("Third roll is only allowed in the last frame");
                }
                ThirdRollScore = pinsKnocked;
                EndFrame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(RollNumber), "rollNumber must be > 0 and < 4");
        }
        CurrentRoll++;
    }

    private void EndFrame() => IsFinished = true;
    public void UpdateBonus(int bonus) => _bonus = bonus;

    public bool IsStrike => (FirstRollScore ?? 0) == _gameConfig.MaxPins;
    public bool IsSpare => !IsStrike && (FirstRollScore ?? 0) + (SecondRollScore ?? 0) == _gameConfig.MaxPins;
    public bool IsLastRoll => CurrentRoll == RollNumber.First || (IsLastFrame && CurrentRoll == RollNumber.Third);
    public bool IsLastFrame => FrameNumber == _gameConfig.MaxFrames;
}