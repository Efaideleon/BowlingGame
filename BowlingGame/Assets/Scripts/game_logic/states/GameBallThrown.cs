using UnityEngine;
using state_machine;

namespace game_logic.states {
    public class GameBallThrown : IState {
        private GameManager m_GameManager;

        public GameBallThrown(GameManager gameManager) {
            m_GameManager = gameManager;
        }

        public void FixedUpdate() {
            // noop
        }

        public void OnEnter() {
            Debug.Log("Game BallThrown State");
            // noop
        }

        public void OnExit() {
            // noop
        }

        public void Update() {
            // noop
        }
    }
}