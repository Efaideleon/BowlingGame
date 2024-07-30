using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BowlingBall bowlingBall;
    [SerializeField] private Transform holdBallPosition; 
    [SerializeField] private Transform swingBallPosition;
    [SerializeField] private Animator animator;
    private CharacterController characterController;
    private readonly float speed = 30;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        animator = GetComponent<Animator>();
        bowlingBall.Hold(holdBallPosition);
        characterController = GetComponent<CharacterController>();
    }

    public void HoldBall()
    {
        animator.SetTrigger("HoldBall");
        bowlingBall.Hold(holdBallPosition);
    }

    public void Charge()
    {
        bowlingBall.Charge();
    }

    public void Swing()
    {
        animator.SetTrigger("Throw");
        bowlingBall.Swing(swingBallPosition);
    }

    public void Throw()
    {
        bowlingBall.Throw();
    }

    public void Move(Vector2 moveInput)
    {
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
