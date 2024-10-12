using System.Collections.Generic;

namespace game_logic {
    public class FrameBonusCalculator {
        private List<BowlingFrame> _allFrames;

        public FrameBonusCalculator(List<BowlingFrame> allFrames) {
            _allFrames = allFrames;
        }

        public void CalculateBonus() {
            for (int i = 0; i < _allFrames.Count; i++) {
                _allFrames[i].SetBonus(GetBonus(i));
            }
        }

        private int GetBonus(int currentFrameIndex) {
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