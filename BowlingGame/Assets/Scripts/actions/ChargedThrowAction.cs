using actions;
using UnityEngine;

namespace player {
    public class ChargedThrowAction : ChargedAction {
        readonly ThrowActionConfig _throwActionConfig;
        readonly CountDownTimer _countDownTimer;
        readonly Player _player;

        public bool IsReady { get; private set; }

        public ChargedThrowAction(ThrowActionConfig throwActionConfig, Player player) {
            _throwActionConfig = throwActionConfig;
            _countDownTimer = new CountDownTimer(_throwActionConfig.ThrowDuration);
            _player = player;
        }

        public void OnChargeStarted() => Start();

        public void OnChargeFinished() {
            Stop();
            _player.Item.Swing(_player.SwingPosition);
            _countDownTimer.Start();
        }

        public void Hold() {
            _player.Item.Hold(_player.HoldPosition);
            IsReady = true;
        }

        public void Throw() {
            _player.Item.Throw(ChargePercentage);
            IsReady = false;
        }

        public void Update() {
            _countDownTimer.Tick(Time.deltaTime);
        }

        public bool IsRunning => _countDownTimer.IsRunning;
    }
}
