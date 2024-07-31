using UnityEngine;
using BowlingBallLib;
using System;
using StateMachine;

public class BowlingBall: MonoBehaviour
{
    private StateMachine<BowlingBall> _stateMachine;

    public BowlingBallPhysics BowlingBallPhysics { get; private set;}
    public event Action OnBallThrown;
    public float ChargePercentage => BowlingBallPhysics.ChargePercentage;

    void Awake()
    {
        BowlingBallPhysics = GetComponent<BowlingBallPhysics>();
        _stateMachine = new StateMachine<BowlingBall>(this);
        _stateMachine.ChangeState(new BowlingBallHoldState(null));
    }

    public void Hold(Transform parent)
    {
        _stateMachine.ChangeState(new BowlingBallHoldState(parent));
    }

    public void Charge()
    {
        _stateMachine.ChangeState(new BowlingBallChargeState());
    }

    public void Swing(Transform parent)
    {
        _stateMachine.ChangeState(new BowlingBallSwingState(parent));
    }

    public void Throw()
    {
        _stateMachine.ChangeState(new BowlingBallThrowState());
        OnBallThrown.Invoke();
    }
}
