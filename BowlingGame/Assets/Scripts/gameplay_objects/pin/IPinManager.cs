using System;

public interface IPinManager {
    public event Action<int> OnPinsSettled;
    public void ResetPins(bool all); 
    public void CheckForPinsToSettle();
}