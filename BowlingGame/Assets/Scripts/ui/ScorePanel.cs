using UnityEngine;
using TMPro;

namespace ui
{
    [System.Serializable]
    public class ScorePanel : MonoBehaviour {
        public TextMeshProUGUI Frame;
        public TextMeshProUGUI FirstScore;
        public TextMeshProUGUI SecondScore;
        public TextMeshProUGUI TotalScore;

        public void UpdateUI(BowlingFrame frame) {
            UpdateFirstScoreBox(frame);
            UpdateSecondScoreBox(frame);
            UpdateFrameNumber(frame);
            UpdateTotalScoreBox(frame);
        }

        void UpdateFrameNumber(BowlingFrame frame) => Frame.text = frame.FrameNumber.ToString();

        void UpdateTotalScoreBox(BowlingFrame frame) {
            if (frame.FirstRollScore != null) {
                TotalScore.text = frame.Score.ToString();
            }
        }

        void UpdateFirstScoreBox(BowlingFrame frame) {
            if (frame.FirstRollScore == 10 && !frame.IsLastFrame) {
                FirstScore.text = "X";
                SecondScore.text = "";
            }
            else {
                FirstScore.text = frame.FirstRollScore.ToString();
            }
        }

        void UpdateSecondScoreBox(BowlingFrame frame) {
            if (frame.SecondRollScore == 10) {
                SecondScore.text = "X";
            }
            else {
                SecondScore.text = frame.SecondRollScore.ToString();
            }

            if (frame.IsSpare()) {
                SecondScore.text = "/";
            }
        }
    }
}