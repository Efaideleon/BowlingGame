using UnityEngine;
using BowlingBallLib;
using System;
using StateMachine;

public class BowlingBall: MonoBehaviour
{
    private StateMachine<BowlingBall> stateMachine;

    public BowlingBallPhysics BowlingBallPhysics { get; private set;}
    public event Action OnBallThrown;
    public float ChargePercentage => BowlingBallPhysics.ChargePercentage;

    void Awake()
    {
        BowlingBallPhysics = GetComponent<BowlingBallPhysics>();
        stateMachine = new StateMachine<BowlingBall>(this);
        stateMachine.ChangeState(new BowlingBallHoldState(null));
    }

    public void Hold(Transform parent)
    {
        stateMachine.ChangeState(new BowlingBallHoldState(parent));
    }

    public void Charge()
    {
        stateMachine.ChangeState(new BowlingBallChargeState());
    }

    public void Swing(Transform parent)
    {
        stateMachine.ChangeState(new BowlingBallSwingState(parent));
    }

    public void Throw()
    {
        stateMachine.ChangeState(new BowlingBallThrowState());
        OnBallThrown.Invoke();
    }
}
