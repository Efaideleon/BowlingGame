using UnityEngine;

namespace player {
    public class ChargedThrowActionPlayer : MonoBehaviour {
        [Header("References")]
        [SerializeField] private InputReader _input;

        [Header("Object to Throw")]
        [Tooltip("The object the player is going to throw.")]
        [SerializeField] Throwable _object;

        [Header("Throw Postions")]
        [Tooltip("The position the player holds the object while in the HoldState.")]
        [SerializeField] Transform _holdPosition;
        [Tooltip("The position the player holds the object while swinging before throwing the object.")]
        [SerializeField] Transform _swingPosition;

        [Header("Throw Action")]
        [SerializeField] ThrowActionConfig _throwActionConfig;

        public ChargedThrowAction ChargedThrowAction { get; private set; }

        void Awake() {
            ChargedThrowAction = new ChargedThrowAction(_throwActionConfig, _object, _holdPosition, _swingPosition);
        }

        void OnEnable() {
            _input.ChargeStarted += OnChargeStarted;
            _input.ChargeFinished += OnChargeFinished;
        }

        void OnDisable() {
            _input.ChargeStarted -= OnChargeStarted;
            _input.ChargeFinished -= OnChargeFinished;
        }

        void Update() {
            ChargedThrowAction.Update();
        }

        private void OnChargeStarted() => ChargedThrowAction.OnChargeStarted();
        private void OnChargeFinished() => ChargedThrowAction.OnChargeFinished();

        public void Hold() => ChargedThrowAction.Hold();
        public void Throw() => ChargedThrowAction.Throw();
    }
}
