using BowlingGameEnums;
using game_logic;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BowlingGame", menuName = "BowlingBall/BowlingGame")]
public class BowlingGame : ScriptableObject, IBowlingGame {
    [SerializeField] BowlingGameConfig m_GameConfig;
    private BonusCalculator _bonusCalculator;

    public BowlingGameConfig Config {
        get { return m_GameConfig; }
        set { m_GameConfig = value; }
    }

    public List<BowlingFrame> AllFrames { get; } = new();
    public int TotalScore {
        get {
            int sum = 0;
            foreach (var frame in AllFrames)
                sum += frame.Score;
            return sum;
        }
    }

    public BowlingFrame CurrentFrame => AllFrames[CurrentFrameIndex];
    public int CurrentFrameIndex { get; private set; } = 0;
    public bool HasGameEnded => CurrentFrameIndex >= m_GameConfig.MaxFrames;

    public event Action OnRollCompleted = delegate { };
    public event Action OnGameOver = delegate { };

    public void OnValidate() {
        InitializeFrames();
        _bonusCalculator = new BonusCalculator(AllFrames);
    }

    void InitializeFrames() {
        if (AllFrames.Count == 0) {
            for (int frameNumber = 1; frameNumber <= m_GameConfig.MaxFrames; frameNumber++) {
                AllFrames.Add(new BowlingFrame(frameNumber, m_GameConfig));
            }
        }
    }

    public void ProcessRoll(int pinsKnocked) {
        if (HasGameEnded) {
            OnGameOver.Invoke();
            return;
        }

        CurrentFrame.SetCurrentRollPinsKnocked(pinsKnocked);

        if (CurrentFrame.IsFinished) {
            CurrentFrameIndex++;
        } else {
            CurrentFrame.MoveToNextRoll();
        }

        OnRollCompleted.Invoke();
        SetAllFramesBonus();
    }

    private void SetAllFramesBonus() {
        for (int frameIndex = 0; frameIndex < AllFrames.Count; frameIndex++) {
            AllFrames[frameIndex].SetBonus(_bonusCalculator.GetBonus(frameIndex));
        }
    }

    public void Reset() => CurrentFrameIndex = 0;
}
