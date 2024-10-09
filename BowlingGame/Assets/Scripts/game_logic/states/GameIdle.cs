using state_machine;
using UnityEngine;

namespace game_logic.states {
    public class GameIdle : IState {
        private readonly GameManager m_GameManager;

        public GameIdle(GameManager gameManager) {
            m_GameManager = gameManager;
        }

        public void FixedUpdate() {
        }

        public void OnEnter() {
            Debug.Log("Game Idle State");
            m_GameManager.PlayerController.Hold();
        }

        public void OnExit() {
            // noop
        }

        public void Update() {
            // noop
        }
    }
}