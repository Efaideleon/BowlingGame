using game_logic.states;
using state_machine;
using System;
using UnityEngine;

namespace game_logic {
    public class GameStateManager : MonoBehaviour {
        [SerializeField]
        private GameManager m_GameManager;
        private StateMachine m_StateMachine;

        void Start() {
            if (m_GameManager == null) {
                throw new NullReferenceException("GameManager is not initialized");
            }
            m_StateMachine = new StateMachine();

            var idle = new GameIdle(m_GameManager);
            var ballThrown = new GameBallThrown(m_GameManager);
            var resettingPins = new GameResettingPins(m_GameManager);

            // Transitions
            m_StateMachine.AddTransition(idle, ballThrown, new FuncPredicate(() => m_GameManager.Ball.IsRolling));
            m_StateMachine.AddTransition(ballThrown, resettingPins, new FuncPredicate(() => m_GameManager.Ball.IsSettled && 
                                                                                            m_GameManager.PinManager.AreAllPinsSettled));
            m_StateMachine.AddTransition(resettingPins, idle, new FuncPredicate(() => m_GameManager.PinManager.CountFallenPins() == 0));

            // Setting initial state
            m_StateMachine.SetState(idle);
        }

        void Update() {
            m_StateMachine.Update();
        }

        void FixedUpdate() {
            m_StateMachine.FixedUpdate();
        }
    }
}