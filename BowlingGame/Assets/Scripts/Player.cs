using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BowlingBall bowlingBall;
    [SerializeField] private Transform ballHoldPosition; 
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        bowlingBall.OnHold(ballHoldPosition);
    }

    public void StartCharging()
    {
        bowlingBall.StartCharging();
    }

    public void ReleaseCharge()
    {
        animator.SetTrigger("Throw");
        bowlingBall.ReleaseCharge();
    }

    public void Throw()
    {
        bowlingBall.Throw();
    }
}
