using System.Collections.Generic;

namespace game_logic {
    public class BonusCalculator {
        private List<BowlingFrame> _allFrames;

        public BonusCalculator(List<BowlingFrame> allFrames) {
            _allFrames = allFrames;
        }

        public int GetBonus(int currentFrameIndex) {
            if (currentFrameIndex >= _allFrames.Count - 1) return 0;
            var currentFrame = _allFrames[currentFrameIndex];
            var nextFrame = _allFrames[currentFrameIndex + 1];

            return currentFrame.HasStrike
                    ? (nextFrame.FirstRollScore ?? 0) + (nextFrame.SecondRollScore ?? 0)
                    : currentFrame.IsSpare
                        ? nextFrame.FirstRollScore ?? 0
                        : 0;
        }
    }
}