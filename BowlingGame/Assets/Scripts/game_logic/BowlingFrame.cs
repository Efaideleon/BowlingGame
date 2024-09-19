public class BowlingFrame {
    const int PINS_PER_FRAME = 10;

    public int FrameNumber;
    public int FirstRollScore; 
    public int SecondRollScore;
    public int ThirdRollScore;
    public int Score;


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
                break;
            default:
                break;
        }
    }

    public bool IsStrike() => FirstRollScore == PINS_PER_FRAME;

    public bool IsSpare() => FirstRollScore + SecondRollScore == PINS_PER_FRAME;
};
