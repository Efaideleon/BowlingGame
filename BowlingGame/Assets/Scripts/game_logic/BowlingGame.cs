using BowlingGameEnums;
using System;
using System.Collections.Generic;
using UnityEngine;
using game_logic;

[CreateAssetMenu(fileName = "BowlingGame", menuName = "BowlingBall/BowlingGame")]
public class BowlingGame : ScriptableObject, IBowlingGame {
    [SerializeField] BowlingGameConfig m_GameConfig;

    public BowlingGameConfig Config {
        get { return m_GameConfig; }
        set { m_GameConfig = value; }
    }

    public List<BowlingFrame> Frames { get; } = new();
    public int TotalScore {
        get {
            int sum = 0;
            foreach (var frame in Frames)
                sum += frame.Score;
            return sum;
        }
    }

    public RollNumber CurrentRoll => Frames[CurrentFrameIndex].CurrentRoll;
    public int CurrentFrameIndex { get; private set; } = 0;
    public bool HasGameEnded => CurrentFrameIndex >= m_GameConfig.MaxFrames;

    public event Action OnRollCompleted = delegate { };
    public event Action OnGameOver = delegate { };

    public void OnValidate() {
        InitializeFrames();
    }

    void InitializeFrames() {
        if (Frames.Count == 0) {
            for (int frameNumber = 1; frameNumber <= m_GameConfig.MaxFrames; frameNumber++) {
                Frames.Add(new BowlingFrame(frameNumber, m_GameConfig, Frames));
            }
        }
    }

    public void ProcessRoll(int pinsKnocked) {
        if (HasGameEnded) {
            OnGameOver.Invoke();
            return;
        }

        if (pinsKnocked < 0 || pinsKnocked > m_GameConfig.MaxPins) {
            throw new ArgumentException("Invalid number of pins knocked down.");
        }

        var frame = Frames[CurrentFrameIndex];
        frame.Update(pinsKnocked);

        if (frame.IsFinished) {
            CurrentFrameIndex++;
        }

        OnRollCompleted.Invoke();
    }

    public bool IsLastRoll() => Frames[CurrentFrameIndex].IsLastRoll(); 
    public void Reset() => CurrentFrameIndex = 0;
}
