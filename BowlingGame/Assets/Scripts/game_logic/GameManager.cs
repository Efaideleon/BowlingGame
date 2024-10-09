using UnityEngine;
using bowling_ball;
using player;
using System;

namespace game_logic {
    public class GameManager : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField, Tooltip("The ball the game is played with")]
        private BowlingBall m_Ball;

        [SerializeField, Tooltip("Controls the players movements")]
        private PlayerController m_playerController;

        [SerializeField, Tooltip("Controls the pins in the game")]
        private PinManager m_PinManager;

        [SerializeField, Tooltip("Stores the data of the game in a SO")]
        private BowlingGame m_Game;

        public BowlingBall Ball => m_Ball;
        public PlayerController PlayerController => m_playerController;
        public PinManager PinManager => m_PinManager;
        public BowlingGame BowlingGame => m_Game;

        void Awake() {
            if (m_Ball == null || m_PinManager == null || m_Game == null || m_playerController == null) {
                throw new NullReferenceException("Properties must be initialized.");
            }
        }
    }
}