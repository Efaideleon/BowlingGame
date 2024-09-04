using System;

namespace player {
    public abstract class Timer {
        protected float Time { get; set; }
        protected float _initialTime;
        public bool IsRunning { get; protected set; }
        public float Progress => Time / _initialTime;
        public event Action OnStart = delegate { };
        public event Action OnStop = delegate { };

        protected Timer(float initialTime) {
            _initialTime = initialTime;
            IsRunning = false;
        }

        public void Start() {
            Time = _initialTime;
            if (!IsRunning) {
                IsRunning = true;
                OnStart?.Invoke();
            }
        }

        public void Stop() {
            if (IsRunning) {
                IsRunning = false;
                OnStop?.Invoke();
            }
        }

        public void Resume() => IsRunning = true;

        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);
    }


    public class CountDownTimer : Timer {
        public CountDownTimer(float initialTime) : base(initialTime) { }

        public override void Tick(float deltaTime) {
            if (IsRunning && Time > 0) {
                Time -= deltaTime;
            }
            else if (IsRunning && Time <= 0) {
                Stop();
            }
        }

        public bool IsFinished() => Time <= 0;

        public void Reset() => Time = _initialTime;

        public void Reset(float newTime) {
            _initialTime = newTime;
            Reset();
        }
    }
}
