using System;
using System.Collections.Generic;
using BowlingGameEnums;

public class BowlingFrame {
    readonly IBowlingGameConfig _config;
    private int? _firstRollScore;
    private int? _secondRollScore;
    private int? _thirdRollScore;
    private int _frameNumber;
    private List<BowlingFrame> m_frames;

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

    public int Bonus() {
        if (FrameNumber >= m_frames.Count) return 0;
        var nextFrame = m_frames[FrameNumber];

        return IsStrike()
                ? (nextFrame.FirstRollScore ?? 0) + (nextFrame.SecondRollScore ?? 0)
                : IsSpare()
                    ? nextFrame.FirstRollScore ?? 0
                    : 0;
    }

    public int Score => (FirstRollScore ?? 0) + (SecondRollScore ?? 0) + (ThirdRollScore ?? 0) + Bonus();
    public bool IsLastFrame => FrameNumber == _config.MaxFrames;

    public BowlingFrame(int frame, IBowlingGameConfig config, List<BowlingFrame> frames) {
        FrameNumber = frame;
        _config = config;
        m_frames = frames;
    } 

    public void RecordRollScore(RollNumber rollNumber, int pinsKnocked) {
        if (pinsKnocked < 0 || pinsKnocked > _config.MaxPins) {
            throw new ArgumentOutOfRangeException(nameof(pinsKnocked), "pinsKnocked must be between 0 and " + _config.MaxPins);
        }
        switch (rollNumber) {
            case RollNumber.First:
                FirstRollScore = pinsKnocked;
                break;
            case RollNumber.Second:
                SecondRollScore = pinsKnocked;
                break;
            case RollNumber.Third:
                if (!IsLastFrame) {
                    throw new InvalidOperationException("Third roll is only allowed in the last frame");
                }
                ThirdRollScore = pinsKnocked;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(rollNumber), "rollNumber must be > 0 and < 4");
        }
    }

    public bool IsStrike() => (FirstRollScore ?? 0) == _config.MaxPins;
    public bool IsSpare() => !IsStrike() && (FirstRollScore ?? 0) + (SecondRollScore ?? 0) == _config.MaxPins;
};
