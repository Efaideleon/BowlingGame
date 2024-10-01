using bowling_ball;
using player;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Bowling Ball")]
    [Tooltip("The ball the game is played with")]
    [SerializeField] BowlingBall _ball;

    [Header("Player Controller")]
    [Tooltip("Controls the players movements")]
    [SerializeField] PlayerController _playerController;

    [SerializeField] PinManager _pinManager;
    [SerializeField] BowlingGame _game;

    void OnEnable() {
        _ball.OnBallThrown += HandleBallThrown;
        _pinManager.OnPinsSettled += Roll;
        _game.OnRollCompleted += HandleRollCompleted;
    }

    void HandleBallThrown() {
        _pinManager.CheckForPinsToSettle();
    }

    void Roll(int pinKnocked) {
        _game.Roll(pinKnocked);
    }

    void HandleRollCompleted() {
        var isLastRoll = _game.IsLastRoll();
        _pinManager.ResetPins(isLastRoll);
        _playerController.Hold();
    }

    void OnDisable() {
        _ball.OnBallThrown -= HandleBallThrown;
        _pinManager.OnPinsSettled -= Roll;
        _game.OnRollCompleted -= HandleRollCompleted;
    }
}