using UnityEngine;
using TMPro;
using game_logic;
using BowlingGameEnums;

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
            if (frame.GetRollScore(RollNumber.First) != null) {
                TotalScore.text = frame.Score.ToString();
            }
        }

        void UpdateFirstScoreBox(BowlingFrame frame) {
            if (frame.GetRollScore(RollNumber.First) == 10 && !frame.IsLastFrame) {
                FirstScore.text = "X";
                SecondScore.text = "";
            }
            else {
                FirstScore.text = frame.GetRollScore(RollNumber.First).ToString();
            }
        }

        void UpdateSecondScoreBox(BowlingFrame frame) {
            if (frame.GetRollScore(RollNumber.Second) == 10) {
                SecondScore.text = "X";
            }
            else {
                SecondScore.text = frame.GetRollScore(RollNumber.Second).ToString();
            }

            if (frame.IsSpare) {
                SecondScore.text = "/";
            }
        }
    }
}