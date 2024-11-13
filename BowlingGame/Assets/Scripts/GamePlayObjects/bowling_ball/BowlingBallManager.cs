using UnityEngine;
using System;
using player;

namespace bowling_ball
{
    [RequireComponent(typeof(BowlingBall))]
    public class BowlingBallManager : Throwable
    {
        BowlingBall BowlingBall { get; set; }

        void Awake()
        {
            BowlingBall = GetComponent<BowlingBall>();
            if (BowlingBall == null)
            {
                Debug.LogError("BowlingBall needs to be added to the BowlingBallManager", this);
            }
        }

        public event Action OnBallThrown;

        public override void Hold(Transform parent)
        {
            transform.parent = parent;
            transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
            BowlingBall.OnHold();
        }

        public override void Swing(Transform parent)
        {
            transform.parent = parent;
            transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        public override void Throw(float power, Vector3 throwDirection)
        {
            BowlingBall.Throw(power, throwDirection);
            transform.parent = null;
            OnBallThrown?.Invoke();
        }
    }
}