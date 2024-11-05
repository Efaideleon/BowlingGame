using UnityEngine;

namespace Actions {
    public class ChargedAction {
        float _maxChargeTime = 2f;
        float _chargeStartTime;
        bool _isCharging = false;
        float _chargeAmount = 0f;


        public void Start() {
            _isCharging = true;
            _chargeStartTime = Time.time;
        }

        public void Stop() {
            _isCharging = false;
        }

        public float ChargePercentage => Mathf.Clamp01(GetCurrentCharge() / _maxChargeTime);

        private float GetCurrentCharge() {
            if (_isCharging) {
                _chargeAmount = Mathf.Min(Time.time - _chargeStartTime, _maxChargeTime);
                return _chargeAmount;
            }
            else {
                return _chargeAmount;
            }
        }

        public void Reset() => _chargeAmount = 0;
    }
}