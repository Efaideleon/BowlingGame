using UnityEngine;
using state_machine;

namespace player {
    public class PlayerController : MonoBehaviour {
        [Header("References")]
        [SerializeField] private InputReader _input;

        Animator _animator;
        CharacterController _characterController;
        StateMachine _stateMachine;

        ChargedThrowActionPlayer _chargedThrowActionPlayer;
        public ChargedThrowAction ChargedThrowAction => _chargedThrowActionPlayer.ChargedThrowAction;

        [Header("Player Settings")]
        [SerializeField] float _movementSpeed = 1f;

        public Vector3 GetDirection() => _input.Direction;

        void Awake() {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _stateMachine = StateMachineBuilder.Build(this, _animator);
            _chargedThrowActionPlayer = GetComponent<ChargedThrowActionPlayer>();
        }

        void Start() => Hold();
        void Update() => _stateMachine.Update();
        void FixedUpdate() => _stateMachine.FixedUpdate();

        #region Public Methods
        public void HandleMovement() => _characterController.Move(_movementSpeed * Time.deltaTime * _input.Direction);
        public void Hold() => ChargedThrowAction.Hold();
        #endregion
    }
}
