using BowlingGameEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace game_logic {
    public class BowlingFrame {
        private List<Roll> _rolls = new();
        private readonly IBowlingGameConfig _gameConfig;
        private Roll _currentRoll;
        private int _bonus = 0;
        private int _frameNumber;

        /// <summary>
        /// The Rollnumber that identifies the CurrentRoll
        /// </summary>
        public RollNumber CurrentRollNumber => _currentRoll.RollNumber;

        /// <summary>
        /// How many pins were knocked during each roll
        /// </summary>
        /// <returns>The sum of all the pins knocked during each roll.</returns>
        public int TotalPinsKnocked => _rolls.Sum(roll => roll.NumOfPinsKnocked);

        /// <summary>
        /// Returns the sum of the score for all roles plus the bonus
        /// </summary>
        public int Score => TotalPinsKnocked + _bonus;

        public int FrameNumber {
            get => _frameNumber;
            private set => _frameNumber = value <= 0
                    ? throw new ArgumentOutOfRangeException(nameof(value), "frame number must be greater than 0.")
                    : value;
        }
        
        public BowlingFrame(int frame, IBowlingGameConfig config) {
            FrameNumber = frame;
            _gameConfig = config;
            if (!_rolls.Any()) {
                _currentRoll = CreateNewRoll();
            }
        }

        /// <summary>
        /// After the player rolls the ball, the frame records the score for that roll.
        /// </summary>
        /// <param name="numOfPinsKnocked">Pins knocked down by the bowling ball for a roll</param>
        public void SetNumOfPinsKnocked(int numOfPinsKnocked) {
            _currentRoll.SetNumOfPinsKnocked(numOfPinsKnocked);
            if (!IsFinished) _currentRoll = CreateNewRoll();
        }

        private Roll CreateNewRoll() {
            Roll roll;
            if (!_rolls.Any()) {
                roll = new Roll(RollNumber.First, _gameConfig.MaxPins);
            } 
            else {
                roll = new Roll(_currentRoll.RollNumber + 1, _gameConfig.MaxPins);
            }
            _rolls.Add(roll);
            return roll;
        }

        /// <summary>
        /// The number of pins knocked down for each role
        /// </summary>
        /// <param name="rollNumber">Rollnumber Enum that identifies each roll.</param>
        /// <returns> The number of pins knocked for the specified roll</returns>
        public int? GetRollScore(RollNumber rollNumber) {
            return _rolls.Find((roll) => roll.RollNumber == rollNumber)?.NumOfPinsKnocked;
        }

        /// <summary>
        /// The bonus depends on other frames's scores.
        /// </summary>
        /// <param name="bonus"></param>
        public void SetBonus(int bonus) => _bonus = bonus;

        /// <summary>
        /// Determines if all the rolls have been performed in the frame.
        /// </summary>
        public bool IsFinished => !IsLastFrame && (IsSecondRollFinished || this.HasStrike) || IsThirdRollFinished; 

        private bool IsSecondRollFinished => CurrentRollNumber == RollNumber.Second && _currentRoll.HasScoreRecorded;
        private bool IsThirdRollFinished => CurrentRollNumber == RollNumber.Third && _currentRoll.HasScoreRecorded;

        /// <summary>
        /// Checks if any of the rolls in the frame is a strike
        /// </summary>
        public bool HasStrike => _rolls.Any((roll) => roll.IsStrike);

        /// <summary>
        /// Checks if the frame is a spare.
        /// </summary>
        public bool IsSpare => !HasStrike && TotalPinsKnocked == _gameConfig.MaxPins;

        /// <summary>
        /// Checks if this is the last frame in the game.
        /// </summary>
        public bool IsLastFrame => FrameNumber == _gameConfig.MaxFrames;

        /// <summary>
        /// When the game resets the roll list is emptied.
        /// Current roll resets to the first roll and is added to the list.
        /// </summary>
        public void ClearRolls() {
            _rolls.Clear();
            _rolls = new List<Roll>();
            _currentRoll = null;
            _currentRoll = CreateNewRoll();
            _bonus = 0;
        }
    }
}