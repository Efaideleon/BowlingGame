using BowlingGameEnums;
using System;
using System.Collections.Generic;
using game_logic;

public interface IBowlingGame {

    public List<BowlingFrame> AllFrames { get; }
    public int TotalScore { get; }
    public int CurrentFrameIndex { get; }
    public BowlingFrame CurrentFrame { get; }
    public bool HasGameEnded { get; }

    public event Action OnRollCompleted;
    public event Action OnGameOver;

    public void ProcessRoll(int pinsKnocked);
}