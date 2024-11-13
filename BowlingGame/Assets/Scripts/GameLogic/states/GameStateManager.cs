using game_logic.states;
using state_machine;
using System;
using UnityEngine;

namespace game_logic {
    public class GameStateManager : MonoBehaviour {
        [SerializeField]
        private GameManager _gameManager;
        private StateMachine _stateMachine;

        void Start() {
            if (_gameManager == null) {
                throw new NullReferenceException("GameManager is not initialized");
            }

            _stateMachine = new StateMachine();

            var idle = new GameIdle(_gameManager);
            var ballThrown = new GameBallThrown(_gameManager);
            var resettingPins = new GameResettingPins(_gameManager);

            // Transitions
            _stateMachine.AddTransition(idle, ballThrown, new FuncPredicate(() => !_gameManager.Ball.IsSettled));
            _stateMachine.AddTransition(ballThrown, resettingPins, new FuncPredicate(() => BallAndPinsAreSettled));
            _stateMachine.AddTransition(resettingPins, idle, new FuncPredicate(() => _gameManager.PinManager.CountFallenPins() == 0));

            // Setting initial state
            _stateMachine.SetState(idle);
        }

        private bool BallAndPinsAreSettled => _gameManager.Ball.IsSettled && _gameManager.PinManager.AreAllPinsSettled;

        public void Reset() {
            _gameManager.PinManager.ResetPins(true);
            _gameManager.PlayerController.Hold();
            _stateMachine.Update();
            _gameManager.BowlingGame.Reset();
        }

        void Update() {
            _stateMachine.Update();
        }

        void FixedUpdate() {
            _stateMachine.FixedUpdate();
        }
    }
}