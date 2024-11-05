using System;

public interface IPinManager {
    public void ResetPins(bool all);
    public bool AreAllPinsSettled { get; }
    public int CountFallenPins();
}