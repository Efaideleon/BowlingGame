using UnityEngine;

public class PlayerMovementTDD: MonoBehaviour
{
    private CharacterController _characterController;
    private readonly float _speed = 1;
    public Vector2 Direction { get; private set; }

    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 direction)
    {
        Direction = _speed * direction;
    }

    private void Update()
    {
        _characterController.Move(Direction * Time.deltaTime);
    }
}