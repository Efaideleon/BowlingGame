using UnityEngine;
using state_machine;

namespace game_logic.states {
    public class GameResettingPins : IState {
        private GameManager m_GameManager;

        public GameResettingPins(GameManager gameManager) {
            m_GameManager = gameManager;
        }

        public void FixedUpdate() {
            // noop
        }

        public void OnEnter() {
            Debug.Log("Game Resetting Pins State");
            var CurrentFrame = m_GameManager.BowlingGame.CurrentFrame;
            m_GameManager.BowlingGame.ProcessRoll(m_GameManager.PinManager.CountFallenPins());
            m_GameManager.PinManager.ResetPins(CurrentFrame.IsFinished);
            // m_GameManager.BowlingGame.HandleFrameAndRollState();
        }

        public void OnExit() {
            // noop
        }

        public void Update() {
            // noop
        }
    }
}