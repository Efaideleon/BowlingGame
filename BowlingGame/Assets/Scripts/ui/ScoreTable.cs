using System.Collections.Generic;
using UnityEngine;

namespace ui {
    public class ScoreTable : MonoBehaviour {
        [SerializeField] List<ScorePanel> ScorePanels = new(10);
        [SerializeField] InputReader _inputReader;

        void Awake() {
            _inputReader.OnMenuOpen += ToggleScoreBoard;
        }

        public void UpdatePanelForFrame(BowlingFrame frame) {
            ScorePanels[frame.FrameNumber - 1].UpdateUI(frame);
        }
        void OnDestroy() {
            _inputReader.OnMenuOpen -= ToggleScoreBoard;
        }

        void ToggleScoreBoard() => gameObject.SetActive(!gameObject.activeSelf);
    }
}