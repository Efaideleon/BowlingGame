using System.Collections.Generic;

// does it make sense to make this into class?
// move into the BowlingFrame class.
public class Bonus {
    private readonly List<BowlingFrame> m_frames;
    private readonly int m_MaxFrames;

    public Bonus(int MaxFrames, List<BowlingFrame> frames) {
        m_frames = frames;
        m_MaxFrames = MaxFrames;
    }

    public int Calculate(int currentFrameIndex) {
        if (currentFrameIndex >= m_MaxFrames - 1) return 0;

        var frame = m_frames[currentFrameIndex];
        var nextFrame = m_frames[currentFrameIndex + 1];

        return frame.IsStrike()
                ? (nextFrame.FirstRollScore ?? 0) + (nextFrame.SecondRollScore ?? 0)
                : frame.IsSpare()
                    ? nextFrame.FirstRollScore ?? 0
                    : 0;
    }
}