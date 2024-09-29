using bowling_ball;
using player;
using System.Collections.Generic;
using ui;
using UnityEngine;

/// <summary>
/// The Controller in the MVC architecture between the model (BowlingBall) and Views.
/// </summary>
public class UIController : MonoBehaviour {
    [Header("References")]
    [SerializeField] ScoreBar _uiManager;
    [SerializeField] ScoreTable _scoreTable;
    [SerializeField] GameManager _gameManager;

    void OnEnable() {
        _gameManager.Game.OnRollCompleted += HandleRollCompleted;
    }

    void OnDisable() {
        _gameManager.Game.OnRollCompleted -= HandleRollCompleted;
    }

    void HandleRollCompleted() {
        var isLastRoll = _gameManager.Game.IsLastRoll();
        if (isLastRoll) _uiManager.ActivatePanels();
        UpdateUI();
    }

    void UpdateUI() {
        _uiManager.UpdateUI(
            _gameManager.Game.CurrentFrameIndex,
            _gameManager.Game.CurrentRoll,
            _gameManager.Game.TotalScore
        );
        UpdateScoreBoard(_gameManager.Game.Frames);
    }

    void UpdateScoreBoard(List<BowlingFrame> frames) {
        foreach (BowlingFrame frame in frames) {
            _scoreTable.UpdatePanelForFrame(frame);
        }
    }
}