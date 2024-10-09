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
            m_GameManager.BowlingGame.ProcessRoll(m_GameManager.PinManager.CountFallenPins());
            m_GameManager.PinManager.ResetPins(m_GameManager.BowlingGame.IsLastRoll());
        }

        public void OnExit() {
            // noop
        }

        public void Update() {
            // noop
        }
    }
}