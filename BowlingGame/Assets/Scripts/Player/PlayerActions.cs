using System;
using UnityEngine;

public class PlayerActions: MonoBehaviour 
{
    [SerializeField] private BowlingBall _bowlingBall;
    [SerializeField] private Transform _holdBallPosition;
    [SerializeField] private Transform _swingBallPosition;

    public event Action OnHold;
    public event Action OnCharge;
    public event Action OnSwing;
    public event Action OnThrow;

    public void Hold()
    {
        OnHold?.Invoke();
        _bowlingBall.Hold(_holdBallPosition);
    }

    public void Charge()
    {
        OnCharge?.Invoke();
        _bowlingBall.Charge();
    }

    public void Swing()
    {
        OnSwing?.Invoke();
        _bowlingBall.Swing(_swingBallPosition);
    }

    public void Throw()
    {
        OnThrow?.Invoke();
        _bowlingBall.Throw();
    }
}
