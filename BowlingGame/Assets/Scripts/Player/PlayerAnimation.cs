using UnityEngine;

public class PlayerAnimation: MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerActions _playerActions;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerActions = GetComponent<PlayerActions>();

    }

    private void OnEnable()
    {
        _playerActions.OnSwing += SetSwingAnimation;
        _playerActions.OnHold += SetHoldAnimation;

    }

    private void OnDisable()
    {
        _playerActions.OnSwing -= SetSwingAnimation;
        _playerActions.OnHold -= SetHoldAnimation;
    }

    private void SetHoldAnimation()
    {
        _animator.SetTrigger("HoldBall");
    }

    private void SetSwingAnimation()
    {
        _animator.SetTrigger("Throw");
    }

    public void Update()
    {
        SetWalkingAnimation();
    }

    private void SetWalkingAnimation()
    {
        _animator.SetBool("IsMovingLeft", _playerMovement.MoveDirection.x < 0f);
        _animator.SetBool("IsMovingRight", _playerMovement.MoveDirection.x > 0f);
    }
}
