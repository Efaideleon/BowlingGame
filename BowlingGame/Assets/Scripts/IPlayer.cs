using UnityEngine;

public interface IPlayer
{
    public void Hold();
    public void Swing();
    public void Charge();
    public void Throw();
    public void Move(Vector2 moveInput);
}
