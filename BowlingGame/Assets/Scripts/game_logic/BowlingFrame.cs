public class BowlingFrame {
    const int PINS_PER_FRAME = 10;

    public int FrameNumber;
    public int? FirstRollScore;
    public int? SecondRollScore;
    public int? ThirdRollScore;
    public int Score;
    public bool IsLastFrame = false;

    public BowlingFrame(int frame) {
        FrameNumber = frame;
    }

    public void UpdateScore(int currentRoll, int pinsKnocked) {
        switch (currentRoll) {
            case 1:
                FirstRollScore = pinsKnocked;
                break;
            case 2:
                SecondRollScore = pinsKnocked;
                break;
            case 3:
                ThirdRollScore = pinsKnocked;
                IsLastFrame = true;
                break;
            default:
                break;
        }
    }

    public bool IsStrike() => (FirstRollScore ?? 0) == PINS_PER_FRAME;

    public bool IsSpare() => !IsStrike() && (FirstRollScore ?? 0) + (SecondRollScore ?? 0) == PINS_PER_FRAME;

    public int FrameScore => (FirstRollScore ?? 0) + (SecondRollScore ?? 0);

    public int GetFirstRollScore() {
        if (FirstRollScore.HasValue)
            return FirstRollScore.Value;
        return 0;
    }

    public int GetSumOfRollScores(int frameIndex) {
        int score = 0;
        if (frameIndex == 9 && ThirdRollScore.HasValue)
            score += ThirdRollScore.Value;

        score += FrameScore;
        return score;
    }
};
