using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BowlingBall bowlingBall;
    [SerializeField] private Transform holdBallPosition; 
    [SerializeField] private Transform swingBallPosition;
    [SerializeField] private Animator animator;
    private CharacterController characterController;
    private readonly float speed = 6;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        animator = GetComponent<Animator>();
        bowlingBall.OnHold(holdBallPosition);
        characterController = GetComponent<CharacterController>();
    }

    public void HoldBall()
    {
        animator.SetTrigger("HoldBall");
        bowlingBall.OnHold(holdBallPosition);
    }

    public void StartCharging()
    {
        bowlingBall.StartCharging();
    }

    public void StopCharging()
    {
        animator.SetTrigger("Throw");
        bowlingBall.StopCharging();
        bowlingBall.SwingPosition(swingBallPosition);
    }

    public void Throw()
    {
        bowlingBall.Throw();
    }

    public void Move(Vector2 moveInput)
    {
        Debug.Log("move direction: " + moveInput.x);
        moveDirection = speed * Time.deltaTime * new Vector3(moveInput.x, 0f, 0f);
    }

    private void Update()
    {
        characterController.Move(moveDirection * Time.deltaTime);
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("IsMovingLeft", moveDirection.x < 0f);
        animator.SetBool("IsMovingRight", moveDirection.x > 0f);
    } 
}
