using actions;
using UnityEngine;

namespace player {
    public class ChargedThrowSystem : MonoBehaviour {
        [SerializeField] InputReader _input;
        [SerializeField] Throwable _object;
        [SerializeField] Transform _holdPosition;
        [SerializeField] Transform _swingPosition;

        public bool IsLoaded { get; private set; }
        ChargedAction _chragedThrow;

        void Awake() {
            _input.ChargeStarted += OnCharge;
            _input.ChargeFinished += OnCharge;
            _chragedThrow = new ChargedAction();
        }

        void Start() => Reset();

        void OnCharge(bool charging) {
            if (charging)
                _chragedThrow.Start();
            else {
                _chragedThrow.Stop();
                _object.Swing(_swingPosition);
            }
        }

        #region Public Methods
        public float ChargePercentage => _chragedThrow.ChargePercentage;

        public void Reset() {
            _object.Hold(_holdPosition);
            IsLoaded = true;
        }

        public void Throw() {
            _object.Throw(_chragedThrow.ChargePercentage);
            IsLoaded = false;
        }
        #endregion

        void OnDestroy() {
            _input.ChargeStarted -= OnCharge;
            _input.ChargeFinished -= OnCharge;
        }
    }
}
