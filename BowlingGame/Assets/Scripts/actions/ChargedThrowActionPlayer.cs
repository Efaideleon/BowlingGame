using UnityEngine;

namespace player {
    public class ChargedThrowActionPlayer : MonoBehaviour {
        [Header("Throw Action")]
        [SerializeField] ThrowActionConfig _throwActionConfig;
        private Player _player;

        private Vector3 _throwDirection = Vector3.right;
        public ChargedThrowAction ChargedThrowAction { get; private set; }

        void Awake() {
            _player = GetComponent<Player>();
            ChargedThrowAction = new ChargedThrowAction(_throwActionConfig,_player);
        }

        void OnEnable() {
            _player.Input.ChargeStarted += OnChargeStarted;
            _player.Input.ChargeFinished += OnChargeFinished;
            _player.Input.OnToggleStarted += ToggleThrowDirection;
        }

        void OnDisable() {
            _player.Input.ChargeStarted -= OnChargeStarted;
            _player.Input.ChargeFinished -= OnChargeFinished;
            _player.Input.OnToggleStarted -= ToggleThrowDirection;
        }

        void ToggleThrowDirection()
        {
            _throwDirection = _throwDirection == Vector3.right ? Vector3.left : Vector3.right;
        }

        void Update() {
            ChargedThrowAction.Update();
        }

        private void OnChargeStarted() { 
            ChargedThrowAction.OnChargeStarted();
        }

        private void OnChargeFinished() { 
            ChargedThrowAction.OnChargeFinished();
        }

        public void Hold() { 
            ChargedThrowAction.Hold();
        }

        public void Throw() => ChargedThrowAction.Throw(_throwDirection);
    }
}