using System;
using System.Collections.Generic;
using UnityEngine;

namespace ui {
    public class ScoreTable : MonoBehaviour {
        [SerializeField] List<ScorePanel> ScorePanels = new(10);

        public void UpdateScore(BowlingFrame frame) {
            var scorePanel = ScorePanels[frame.FrameNumber - 1];

            scorePanel.Frame.text = frame.FrameNumber.ToString();
            scorePanel.FirstScore.text = frame.FirstRollScore.ToString();
            scorePanel.SecondScore.text = frame.SecondRollScore.ToString();
            scorePanel.TotalScore.text = frame.Score.ToString();
        }
    }
}