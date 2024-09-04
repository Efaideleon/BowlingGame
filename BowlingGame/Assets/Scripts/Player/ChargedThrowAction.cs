using actions;
using UnityEngine;

namespace player {
    public class ChargedThrowAction : ChargedAction {
        readonly ThrowActionConfig _throwActionConfig;
        readonly CountDownTimer _countDownTimer;

        private readonly Throwable _item;
        private readonly Transform _holdPosition;
        private readonly Transform _swingPosition;

        public bool IsReady { get; private set; }

        public ChargedThrowAction(
            ThrowActionConfig throwActionConfig,
            Throwable item,
            Transform holdPosition,
            Transform swingPosition) {
            _throwActionConfig = throwActionConfig;
            _countDownTimer = new CountDownTimer(_throwActionConfig.ThrowDuration);
            _item = item;
            _holdPosition = holdPosition;
            _swingPosition = swingPosition;
        }

        public void OnChargeStarted() => Start();

        public void OnChargeFinished() {
            Stop();
            _item.Swing(_swingPosition);
            _countDownTimer.Start();
        }

        public void Hold() {
            _item.Hold(_holdPosition);
            IsReady = true;
        }

        public void Throw() {
            _item.Throw(ChargePercentage);
            IsReady = false;
        }

        public void Update() {
            _countDownTimer.Tick(Time.deltaTime);
        }

        public bool IsRunning => _countDownTimer.IsRunning;
    }
}
