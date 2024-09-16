using System;
using UnityEngine.Apple;

public class BowlingGame {
    private const int MAX_FRAMES = 10;
    private const int PINS_PER_FRAME = 10;

    private int _currentFrame = 1;
    private int _currentRoll = 1;
    private readonly int[] rolls = new int[21];
    private int currentRollIndex = 0;

    public int Score => CalculateScore();
    public bool IsGameOver => _currentFrame > MAX_FRAMES;

    public event Action<int, int, int> OnGameStateUpdate = delegate { };
    public event Action<bool> OnRollEnded = delegate { };
    public event Action OnGameOver = delegate { };

    public void Roll(int pinsKnocked) {
        if (IsGameOver) OnGameOver.Invoke();

        if (pinsKnocked < 0 || pinsKnocked > 10) {
            throw new ArgumentException("Invalid number of pins knocked down.");
        }

        rolls[currentRollIndex++] = pinsKnocked;


        if (_currentFrame < MAX_FRAMES) {
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

        OnGameStateUpdate.Invoke(_currentFrame, _currentRoll, Score);
        OnRollEnded.Invoke(IsLastRoll());
    }

    private void MoveToNextFrame() {
        _currentFrame++;
        _currentRoll = 1;
    }

    private int CalculateScore() {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 1; frame <= MAX_FRAMES; frame++) {
            if (IsStrike(rollIndex)) {
                score += 10 + StrikeBonus(rollIndex);
                rollIndex++;
            }
            else if (IsSpare(rollIndex)) {
                score += 10 + SpareBonus(rollIndex);
                rollIndex += 2;
            }
            else {
                score += SumOfBallInFrame(rollIndex);
                rollIndex += 2;
            }
        }

        return score;
    }

    public bool IsLastRoll() {
        return _currentRoll == 1 || (_currentFrame == 10 && _currentRoll == 3);
    }

    private bool IsStrike(int rollIndex) => rolls[rollIndex] == PINS_PER_FRAME;

    private bool IsSpare(int rollIndex) => rolls[rollIndex] + rolls[rollIndex + 1] == PINS_PER_FRAME;

    private int StrikeBonus(int rollIndex) => rolls[rollIndex + 1] + rolls[rollIndex + 2];

    private int SpareBonus(int rollIndex) => rolls[rollIndex + 2];

    private int SumOfBallInFrame(int rollIndex) => rolls[rollIndex] + rolls[rollIndex + 1];
}
