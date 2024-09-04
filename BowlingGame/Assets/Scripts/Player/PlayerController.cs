using UnityEngine;
using state_machine;
using states;
using System.Collections.Generic;

namespace player {
    public class PlayerController : MonoBehaviour {
        [Header("References")]
        [SerializeField] InputReader _input;

        private readonly float _throwDuration = 2f;

        Animator _animator;
        CharacterController _characterController;
        StateMachine _stateMachine;
        CountDownTimer _throwTimer;
        ChargedThrowSystem _chargedThrowSystem;

        List<CountDownTimer> _timers;

        // Player Settings
        readonly float _speed = 1f;

        // Animation Hashes
        static readonly int DirectionHash = Animator.StringToHash("Direction");

        void Awake() {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();

            _chargedThrowSystem = GetComponent<ChargedThrowSystem>();
            _throwTimer = new CountDownTimer(_throwDuration);
            _timers = new(1) { _throwTimer };

            SetupStateMachine();
        }

        void SetupStateMachine() {
            _stateMachine = new StateMachine();

            // Declare States
            var holdingState = new HoldingState(this, _animator);
            var normalState = new NormalState(this, _animator);
            var throwState = new ThrowState(this, _animator);

            // Transition
            At(holdingState, throwState, new FuncPredicate(() => _throwTimer.IsRunning));
            At(throwState, normalState, new FuncPredicate(() => !_throwTimer.IsRunning));
            At(normalState, holdingState, new FuncPredicate(() => _chargedThrowSystem.IsLoaded));

            // Set inital state
            _stateMachine.SetState(holdingState);
        }

        void OnEnable() {
            _input.ChargeFinished += ctx => _throwTimer.Start();
        }

        void OnDisable() {
            _input.ChargeFinished -= ctx => _throwTimer.Start();
        }

        void Update() {
            _stateMachine.Update();
            UpdateAnimator();
            HandleTimers();
        }

        void FixedUpdate() {
            _stateMachine.FixedUpdate();
        }

        void HandleTimers() {
            foreach (var timer in _timers)
                timer.Tick(Time.deltaTime);
        }

        void UpdateAnimator() => _animator.SetFloat(DirectionHash, _input.Direction.x);

        void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);

        void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

        #region Public Methods
        public void HandleMovement() => _characterController.Move(_speed * Time.deltaTime * _input.Direction);
        #endregion

    }
}
