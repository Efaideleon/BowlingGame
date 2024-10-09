using UnityEngine;
using System;
using player;

namespace bowling_ball {
    [RequireComponent(typeof(BowlingBall))]
    public class BowlingBallManager : Throwable {
        BowlingBall BowlingBall { get; set; }

        void Awake() {
            BowlingBall = GetComponent<BowlingBall>();
        }

        public event Action OnBallThrown;

        public override void Hold(Transform parent) {
            transform.parent = parent;
            transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
            BowlingBall.OnHold();
        }

        public override void Swing(Transform parent) {
            transform.parent = parent;
            transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        public override void Throw(float power) {
            BowlingBall.Throw(power);
            transform.parent = null;
            OnBallThrown?.Invoke();
        }
    }
}
