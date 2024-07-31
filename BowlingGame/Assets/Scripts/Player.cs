using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private BowlingBall _bowlingBall;
    [SerializeField] private Transform _holdBallPosition; 
    [SerializeField] private Transform _swingBallPosition;
    [SerializeField] private Animator _animator;

    private CharacterController _characterController;
    private readonly float _speed = 30;
    private Vector3 _moveDirection = Vector3.zero;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _bowlingBall.Hold(_holdBallPosition);
        _characterController = GetComponent<CharacterController>();
    }

    public void HoldBall()
    {
        _animator.SetTrigger("HoldBall");
        _bowlingBall.Hold(_holdBallPosition);
    }

    public void Charge()
    {
        _bowlingBall.Charge();
    }

    public void Swing()
    {
        _animator.SetTrigger("Throw");
        _bowlingBall.Swing(_swingBallPosition);
    }

    public void Throw()
    {
        _bowlingBall.Throw();
    }

    public void Move(Vector2 moveInput)
    {
        _moveDirection = _speed * Time.deltaTime * new Vector3(moveInput.x, 0f, 0f);
    }

    private void Update()
    {
        _characterController.Move(_moveDirection * Time.deltaTime);
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _animator.SetBool("IsMovingLeft", _moveDirection.x < 0f);
        _animator.SetBool("IsMovingRight", _moveDirection.x > 0f);
    } 
}
