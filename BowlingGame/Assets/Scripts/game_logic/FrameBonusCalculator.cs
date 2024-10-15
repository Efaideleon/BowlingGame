using BowlingGameEnums;
using System.Collections.Generic;
using System.Linq;

namespace game_logic {
    public class FrameBonusCalculator {
        private List<BowlingFrame> _allFrames;

        public FrameBonusCalculator(List<BowlingFrame> allFrames) {
            _allFrames = allFrames;
        }

        public void CalculateBonus() {
            foreach (var frame in _allFrames) {
                frame.SetBonus(GetBonus(frame));
            }
        }

        private int GetBonus(BowlingFrame currentFrame) {
            int currentFrameIndex = _allFrames.IndexOf(currentFrame);
            if (currentFrameIndex >= _allFrames.Count - 1) return 0;
            var nextFrame = _allFrames[currentFrameIndex + 1];

            return currentFrame.HasStrike
                    ? nextFrame.TotalPinsKnocked : currentFrame.IsSpare
                        ? nextFrame.GetRollScore(RollNumber.First) ?? 0
                        : 0;
        }
    }
}