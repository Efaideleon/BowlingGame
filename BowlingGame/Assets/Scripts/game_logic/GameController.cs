using bowling_ball;
using player;
using System;
using System.Collections.Generic;
using ui;
using UnityEngine;

/// <summary>
/// The Controller in the MVC architecture between the model (BowlingBall) and Views.
/// </summary>
public class GameController : MonoBehaviour {
    [Header("References")]
    [SerializeField] PinManager _pinManager;
    [SerializeField] PlayerController _playerController;
    [SerializeField] BowlingBall _bowlingBall;
    [SerializeField] UIManager _uiManager;
    [SerializeField] ScoreTable _scoreTable;

    private BowlingGame _game;

    void Awake() {
        _game = new BowlingGame();
        _bowlingBall.OnBallThrown += _pinManager.CheckForPinsToSettle;
        _pinManager.OnPinsSettled += _game.Roll;
        _game.OnGameStateUpdate += _uiManager.UpdateUI;
        _game.OnUpdateScoreBoard += HandleUpdateScoreBoard; 
        _game.OnRollEnded += RollEnded;
        _game.OnGameOver += HandleGameOver;
    }

    void HandleUpdateScoreBoard(List<BowlingFrame> frames) {
        foreach (BowlingFrame frame in frames) {
            Debug.Log(frame.FrameNumber + " " + frame.FirstRollScore + " " + frame.SecondRollScore);
            _scoreTable.UpdateScore(frame);
        }
    }
    
    void HandleGameOver() => Debug.Log("Game Over! Final Score: " + _game.TotalScore);

    void OnDestroy() {
        _bowlingBall.OnBallThrown -= _pinManager.CheckForPinsToSettle;
        _pinManager.OnPinsSettled -= _game.Roll;
        _game.OnGameStateUpdate -= _uiManager.UpdateUI;
        _game.OnUpdateScoreBoard -= HandleUpdateScoreBoard; 
        _game.OnRollEnded -= RollEnded;
        _game.OnGameOver -= HandleGameOver;
    }

    void RollEnded(bool isLastRoll) {
        if (isLastRoll) _uiManager.ActivatePanels();
        _pinManager.ResetPins(isLastRoll);
        _playerController.Hold();
    }
}