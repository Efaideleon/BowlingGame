using System;
using System.Collections.Generic;

public class BowlingGame {
    private const int MAX_FRAMES = 10;
    private const int PINS_PER_FRAME = 10;

    private const int FIRST_ROLL = 1;
    private const int SECOND_ROLL = 2;
    private const int THIRD_ROLL = 3;

    private int _currentFrameIndex = 1;
    private int _currentRoll = FIRST_ROLL;
    private int _totalScore = 0;
    private List<BowlingFrame> _frames = new();

    public int TotalScore => _totalScore;
    public bool IsGameOver => _currentFrameIndex > MAX_FRAMES;

    public event Action<int, int, int> OnGameStateUpdate = delegate { };
    public event Action<List<BowlingFrame>> OnUpdateScoreBoard = delegate { };
    public event Action<bool> OnRollEnded = delegate { };
    public event Action OnGameOver = delegate { };

    public BowlingGame() {
        for (int i = 1; i <= MAX_FRAMES; i++) {
            _frames.Add(new BowlingFrame(i));
        }
    }

    public void Roll(int pinsKnocked) {
        if (IsGameOver) OnGameOver.Invoke();

        if (pinsKnocked < 0 || pinsKnocked > 10) {
            throw new ArgumentException("Invalid number of pins knocked down.");
        }

        var frame = _frames[_currentFrameIndex - 1];
        frame.UpdateScore(_currentRoll, pinsKnocked);

        if (_currentFrameIndex < MAX_FRAMES) {
            HandleRegularFrameRoll(pinsKnocked);
        }
        else {
            HandleFinalFrameRoll(pinsKnocked);
        }

        CalculateFrameScores();
        OnUpdateScoreBoard.Invoke(_frames);
        OnGameStateUpdate.Invoke(_currentFrameIndex, _currentRoll, TotalScore);
        OnRollEnded.Invoke(IsLastRoll());
    }

    private void HandleRegularFrameRoll(int pinsKnocked) {
        if (_currentRoll == FIRST_ROLL && pinsKnocked == PINS_PER_FRAME) {
            MoveToNextFrame();
        }
        else if (_currentRoll == SECOND_ROLL) {
            MoveToNextFrame();
        }
        else {
            _currentRoll++;
        }
    }

    private void HandleFinalFrameRoll(int pinsKnocked) {
        if (_currentRoll < THIRD_ROLL && (pinsKnocked == PINS_PER_FRAME || _currentRoll == SECOND_ROLL)) {
            _currentRoll++;
        }
        else {
            MoveToNextFrame();
        }
    }

    private void MoveToNextFrame() {
        _currentFrameIndex++;
        _currentRoll = FIRST_ROLL;
    }

    private void CalculateFrameScores() {
        int score = 0;
        _totalScore = 0;

        for (int i = 0; i < MAX_FRAMES; i++) {
            var frame = _frames[i];

            if (frame.IsStrike() && i < MAX_FRAMES - 1) {
                score += 10 + StrikeBonus(i);
            }
            else if (frame.IsSpare() && i < MAX_FRAMES - 1) {
                score += 10 + SpareBonus(i);
            }
            else {
                score += SumOfBallInFrame(i);
            }

            _totalScore += score;
            frame.Score = TotalScore;
            score = 0;
        }
    }

    public bool IsLastRoll() {
        return _currentRoll == FIRST_ROLL || (_currentFrameIndex == MAX_FRAMES && _currentRoll == THIRD_ROLL);
    }

    private int StrikeBonus(int frameIndex) => _frames[frameIndex + 1].FirstRollScore + _frames[frameIndex + 1].SecondRollScore;

    private int SpareBonus(int frameIndex) => _frames[frameIndex + 1].FirstRollScore;

    private int SumOfBallInFrame(int frameIndex) {
        int score = 0;
        if (frameIndex == 9) score += _frames[frameIndex].ThirdRollScore;

        score += _frames[frameIndex].FirstRollScore + _frames[frameIndex].SecondRollScore;
        return score;
    }
}
