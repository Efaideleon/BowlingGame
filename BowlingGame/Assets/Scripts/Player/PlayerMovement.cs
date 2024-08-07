using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private readonly float _speed = 10;

    public Vector3 MoveDirection => _moveDirection;

    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 inputVector)
    {
        _moveDirection = _speed * Time.deltaTime * new Vector3(inputVector.x, 0f, 0f);
    }

    private void Update()
    {
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
