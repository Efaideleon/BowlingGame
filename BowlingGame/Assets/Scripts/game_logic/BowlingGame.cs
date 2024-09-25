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
        frame.UpdateFrame(_currentRoll, pinsKnocked);

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

    void HandleRegularFrameRoll(int pinsKnocked) {
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

    void HandleFinalFrameRoll(int pinsKnocked) {
        if (_currentRoll < THIRD_ROLL && (pinsKnocked == PINS_PER_FRAME || _currentRoll == SECOND_ROLL)) {
            _currentRoll++;
        }
        else {
            MoveToNextFrame();
        }
    }

    void MoveToNextFrame() {
        _currentFrameIndex++;
        _currentRoll = FIRST_ROLL;
    }

    void CalculateFrameScores() {
        _totalScore = 0;

        for (int i = 0; i < MAX_FRAMES; i++) {
            var frame = _frames[i];

            if (frame.IsStrike() && i < MAX_FRAMES - 1) {
                frame.SetBonus((_frames[i + 1].FirstRollScore ?? 0) + (_frames[i + 1].SecondRollScore ?? 0));
            }
            else if (frame.IsSpare() && i < MAX_FRAMES - 1) {
                frame.SetBonus(_frames[i + 1].FirstRollScore ?? 0);
            }

            _totalScore += frame.Score;
        }
    }

    public bool IsLastRoll() => _currentRoll == FIRST_ROLL || (_currentFrameIndex == MAX_FRAMES && _currentRoll == THIRD_ROLL);
}
