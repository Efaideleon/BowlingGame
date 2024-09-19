using System;
using System.Collections.Generic;

public class BowlingGame {
    private const int MAX_FRAMES = 10;
    private const int PINS_PER_FRAME = 10;

    private int _currentFrameIndex = 1;
    private int _currentRoll = 1;
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

        // Calculates if we should move to the next frame or just increase the roll count
        if (_currentFrameIndex < MAX_FRAMES) {
            if (_currentRoll == 1 && pinsKnocked == PINS_PER_FRAME) {
                MoveToNextFrame();
            }
            else if (_currentRoll == 2) {
                MoveToNextFrame();
            }
            else {
                _currentRoll++;
            }
        }
        else {
            if (_currentRoll < 3 && (pinsKnocked == PINS_PER_FRAME || _currentRoll == 2)) {
                _currentRoll++;
            }
            else {
                MoveToNextFrame();
            }
        }

        CalculateFrameScores();
        OnUpdateScoreBoard.Invoke(_frames);
        OnGameStateUpdate.Invoke(_currentFrameIndex, _currentRoll, TotalScore);
        OnRollEnded.Invoke(IsLastRoll());
    }

    private void MoveToNextFrame() {
        _currentFrameIndex++;
        _currentRoll = 1;
    }

    // Find a way to calculate the frame scores
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
            _frames[i].TotalScore = TotalScore;
            score = 0;
        }
    }

    public bool IsLastRoll() {
        return _currentRoll == 1 || (_currentFrameIndex == 10 && _currentRoll == 3);
    }

    private int StrikeBonus(int frameIndex) => _frames[frameIndex + 1].FirstRollScore + _frames[frameIndex + 1].SecondRollScore;

    private int SpareBonus(int frameIndex) => _frames[frameIndex + 1].FirstRollScore;

    private int SumOfBallInFrame(int frameIndex) {
        int score = 0;
        if (frameIndex == 9) {
            score += _frames[frameIndex].ThirdRollScore;
        }
        score += _frames[frameIndex].FirstRollScore + _frames[frameIndex].SecondRollScore;
        return score;
    }
}
