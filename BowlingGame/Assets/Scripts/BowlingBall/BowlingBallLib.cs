using UnityEngine;
using StateMachine;

namespace BowlingBallLib
{
    // Hold
    public class BowlingBallHoldState : IState<BowlingBall>
    {
        private readonly Transform _parent;
        public BowlingBallHoldState(Transform parent)
        {
            _parent = parent;
        }

        public void Enter(BowlingBall bowlingBall)
        {
            bowlingBall.transform.parent = _parent;
            bowlingBall.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        public void Execute(BowlingBall bowlingBall)
        {
            bowlingBall.BowlingBallPhysics.OnHold();
        }
        public void Exit(BowlingBall bowlingBall)
        {
            bowlingBall.transform.parent = null;
        }
    }

    // Charge
    public class BowlingBallChargeState : IState<BowlingBall>
    {
        public void Enter(BowlingBall bowlingBall)
        {
            bowlingBall.BowlingBallPhysics.StartCharging();
        }
        public void Execute(BowlingBall bowlingBall) { }
        public void Exit(BowlingBall bowlingBall)
        {
            bowlingBall.BowlingBallPhysics.StopCharging();
        }
    }

    // Swing
    public class BowlingBallSwingState : IState<BowlingBall>
    {
        private readonly Transform _parent;
        public BowlingBallSwingState(Transform parent)
        {
            _parent = parent;
        }

        public void Enter(BowlingBall bowlingBall)
        {
            bowlingBall.transform.parent = _parent;
            bowlingBall.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        public void Execute(BowlingBall bowlingBall) { }
        public void Exit(BowlingBall bowlingBall)
        {
            bowlingBall.transform.parent = null;
        }
    }

    // Throw
    public class BowlingBallThrowState : IState<BowlingBall>
    {
        public void Enter(BowlingBall bowlingBall) { }
        public void Execute(BowlingBall bowlingBall)
        {
            bowlingBall.BowlingBallPhysics.Throw();
        }
        public void Exit(BowlingBall bowlingBall)
        {
        }
    }
}