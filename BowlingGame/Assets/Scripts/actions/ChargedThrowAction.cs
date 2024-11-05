using Actions;
using UnityEngine;

namespace player
{
    public class ChargedThrowAction : ChargedAction
    {
        private readonly ThrowActionConfig _throwActionConfig;
        private readonly CountDownTimer _countDownTimer;
        private readonly Player _player;

        /// <summary>
        /// A boolean to determine if player can throw the item / is holding an item.
        /// </summary>
        public bool IsReady { get; private set; }
        public bool IsRunning => _countDownTimer.IsRunning;

        public ChargedThrowAction(ThrowActionConfig throwActionConfig, Player player)
        {
            _throwActionConfig = throwActionConfig;
            _player = player;
            _countDownTimer = new CountDownTimer(_throwActionConfig.ThrowDuration);
        }

        public void OnChargeStarted()
        {
            if (!IsReady) { return; }
            Start();
        }

        public void OnChargeFinished()
        {
            if (!IsReady) { return; }
            Stop();
            _player.Item.Swing(_player.SwingPosition);
            _countDownTimer.Start();
        }

        /// <summary>
        /// Resets the Throwable item to the player's hold position.
        /// </summary>
        public void Hold()
        {
            _player.Item.Hold(_player.HoldPosition);
            Reset();
            IsReady = true;
        }

        /// <summary>
        /// Throws the item is the player is holding with a ChargePercentage.
        /// </summary>
        public void Throw()
        {
            _player.Item.Throw(ChargePercentage);
            IsReady = false;
        }

        public void Update()
        {
            _countDownTimer.Tick(Time.deltaTime);
        }
    }
}