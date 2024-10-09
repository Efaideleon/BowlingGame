using BowlingGameEnums;

public class RollTracker {
    private readonly BowlingGameConfig m_config;
    public RollNumber CurrentRoll {get; private set; }= RollNumber.First;
    public int CurrentFrameIndex { get; private set; } = 0;

    public RollTracker(BowlingGameConfig config) {
        m_config = config;
    }

    public void ProceedToNextRoll(int pinsKnocked) {
        if (IsFinalFrame()) {
            HandleFinalFrameRoll();
        }
        else {
            HandleRegularFrameRoll(pinsKnocked);
        }
    }

    void HandleRegularFrameRoll(int pinsKnocked) {
        if (CurrentRoll == RollNumber.First && pinsKnocked == m_config.MaxPins) {
            MoveToNextFrame();
        }
        else if (CurrentRoll == RollNumber.Second) {
            MoveToNextFrame();
        }
        else {
            CurrentRoll++;
        }
    }

    void HandleFinalFrameRoll() {
        if (CurrentRoll == RollNumber.Third) {
            MoveToNextFrame();
        }
        else {
            CurrentRoll++;
        }
    }

    void MoveToNextFrame() {
        CurrentFrameIndex++;
        CurrentRoll = RollNumber.First;
    }

    public bool IsLastRoll() {
        return CurrentRoll == RollNumber.First ||
               (CurrentFrameIndex == m_config.MaxFrames - 1 && CurrentRoll == RollNumber.Third);
    }

    public void Reset() {
        CurrentFrameIndex = 0;
        CurrentRoll = RollNumber.First;
    }

    private bool IsFinalFrame() {
        return CurrentFrameIndex >= m_config.MaxFrames - 1;
    }
}