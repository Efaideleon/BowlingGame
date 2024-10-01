using BowlingGameEnums;
using System;
using System.Collections.Generic;

public interface IBowlingGame {

    public List<BowlingFrame> Frames { get; }
    public int TotalScore { get; }
    public int CurrentFrameIndex { get; }
    public RollNumber CurrentRoll { get; }
    public bool IsGameOver { get; }

    public event Action OnRollCompleted;
    public event Action OnGameOver;

    public void Roll(int pinsKnocked);
    public bool IsLastRoll();
}