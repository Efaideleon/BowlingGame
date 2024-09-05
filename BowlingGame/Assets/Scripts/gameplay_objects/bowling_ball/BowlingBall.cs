using UnityEngine;
using System;
using player;

namespace bowling_ball {
    [RequireComponent(typeof(BowlingBallPhysics))]
    public class BowlingBall : Throwable {
        BowlingBallPhysics BowlingBallPhysics { get; set; }

        void Awake() {
            BowlingBallPhysics = GetComponent<BowlingBallPhysics>();
        }

        public event Action OnBallThrown;

        public override void Hold(Transform parent) {
            transform.parent = parent;
            transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
            BowlingBallPhysics.OnHold();
        }

        public override void Swing(Transform parent) {
            transform.parent = parent;
            transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        public override void Throw(float power) {
            BowlingBallPhysics.Throw(power);
            transform.parent = null;
            OnBallThrown?.Invoke();
        }
    }
}
