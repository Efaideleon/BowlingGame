using bowling_ball;
using player;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Config")]
    [Tooltip("Defines the max number of frames and pins per frame.")]
    [SerializeField] BowlingGameConfig _config;

    [Header("Model")]
    [Tooltip("Holds the true data about the game")]
    [SerializeField] BowlingGame _game;

    [Header("Pin Manager")]
    [Tooltip("Controls the pins")]
    [SerializeField] PinManager _pinManager;
    
    [Header("Bowling Ball")]
    [Tooltip("The ball the game is played with")]
    [SerializeField] BowlingBall _ball;

    [Header("Player Controller")]
    [Tooltip("Controls the players movements")]
    [SerializeField] PlayerController _playerController;

    public BowlingGame Game { get { return _game; } }

    void Awake() {
        _game = new BowlingGame(_config);
        _ball.OnBallThrown += HandleBallThrown;
        _pinManager.OnPinsSettled += Roll;
        Game.OnRollCompleted += HandleRollCompleted;
    }

    void HandleBallThrown() {
        _pinManager.CheckForPinsToSettle();
    }

    void Roll(int pinKnocked) {
        Game.Roll(pinKnocked);
    }

    void HandleRollCompleted() {
        var isLastRoll = Game.IsLastRoll();
        _pinManager.ResetPins(isLastRoll);
        _playerController.Hold();
    }

    void OnDestroy() {
        _ball.OnBallThrown -= HandleBallThrown;
        _pinManager.OnPinsSettled -= Roll;
        Game.OnRollCompleted -= HandleRollCompleted;
    }
}