using System;

public class BowlingFrame {
    const int PINS_PER_FRAME = 10;

    public int FrameNumber => _frameNumber;
    public int? FirstRollScore => _firstRollScore;
    public int? SecondRollScore => _secondRollScore;
    public int? ThirdRollScore => _thirdRollScore;
    public int Score => (FirstRollScore ?? 0) + (SecondRollScore ?? 0) + (ThirdRollScore ?? 0) + _bonus;
    public bool IsLastFrame => _isLastFrame;
    private int? _firstRollScore;
    private int? _secondRollScore;
    private int? _thirdRollScore;
    private bool _isLastFrame = false;
    private readonly int _frameNumber;
    private int _bonus = 0;

    public BowlingFrame(int frame) => _frameNumber = frame;

    public void UpdateFrame(int currentRoll, int pinsKnocked) {
        switch (currentRoll) {
            case 1:
                _firstRollScore = pinsKnocked;
                break;
            case 2:
                _secondRollScore = pinsKnocked;
                break;
            case 3:
                _thirdRollScore = pinsKnocked;
                _isLastFrame = true;
                break;
            default:
                break;
        }
    }

    public bool IsStrike() => (FirstRollScore ?? 0) == PINS_PER_FRAME;

    public bool IsSpare() => !IsStrike() && (FirstRollScore ?? 0) + (SecondRollScore ?? 0) == PINS_PER_FRAME;

    public void SetBonus(int bonus) => _bonus = bonus;
};
