using System;

public class BowlingGame {
    private const int MAX_FRAMES = 10;
    private const int PINS_PER_FRAME = 10;

    private int currentFrame = 1;
    private int currentRoll = 1;
    private readonly int[] rolls = new int[21];
    private int currentRollIndex = 0;

    public int CurrentFrame => currentFrame;
    public int CurrentRoll => currentRoll;
    public int Score => CalculateScore();

    public bool IsGameOver => currentFrame > MAX_FRAMES;

    public void Roll(int pinsKnocked) {
        if (IsGameOver) return;

        if (pinsKnocked < 0 || pinsKnocked > 10) {
            throw new ArgumentException("Invalid number of pins knocked down.");
        }

        rolls[currentRollIndex++] = pinsKnocked;


        if (currentFrame < MAX_FRAMES) {
            if (currentRoll == 1 && pinsKnocked == PINS_PER_FRAME) {
                MoveToNextFrame();
            }
            else if (currentRoll == 2) {
                MoveToNextFrame();
            }
            else {
                currentRoll++;
            }
        }
        else {
            if (currentRoll < 3 && (pinsKnocked == PINS_PER_FRAME || currentRoll == 2)) {
                currentRoll++;
            }
            else {
                MoveToNextFrame();
            }
        }
    }

    private void MoveToNextFrame() {
        currentFrame++;
        currentRoll = 1;
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

    private bool IsStrike(int rollIndex) => rolls[rollIndex] == PINS_PER_FRAME;

    private bool IsSpare(int rollIndex) => rolls[rollIndex] + rolls[rollIndex + 1] == PINS_PER_FRAME;

    private int StrikeBonus(int rollIndex) => rolls[rollIndex + 1] + rolls[rollIndex + 2];

    private int SpareBonus(int rollIndex) => rolls[rollIndex + 2];

    private int SumOfBallInFrame(int rollIndex) => rolls[rollIndex] + rolls[rollIndex + 1];
}
