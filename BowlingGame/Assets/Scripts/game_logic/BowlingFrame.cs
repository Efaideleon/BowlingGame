using BowlingGameEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace game_logic {
    public class BowlingFrame {
        private readonly List<Roll> _rolls = new();
        private readonly IBowlingGameConfig _gameConfig;
        private int _bonus = 0;
        private int _frameNumber;
        public int TotalPinsKnocked => _rolls.Sum(roll => roll.NumOfPinsKnocked);

        // Starts from 1.
        public int FrameNumber {
            get => _frameNumber;
            private set => _frameNumber = value <= 0
                    ? throw new ArgumentOutOfRangeException(nameof(value), "frame number must be greater than 0.")
                    : value;
        }

        /// <summary>
        /// The Rollnumber that identifies the CurrentRoll
        /// </summary>
        public RollNumber CurrentRollNumber => _rolls.Any() ? _rolls.Last().RollNumber : RollNumber.None;

        /// <summary>
        /// Returns the sum of the score for all roles plus the bonus
        /// </summary>
        public int Score => TotalPinsKnocked + _bonus;

        /// <summary>
        /// The number of pins knocked down for each role
        /// </summary>
        /// <param name="rollNumber">Rollnumber Enum that identifies each roll.</param>
        /// <returns> The number of pins knocked for the specified roll</returns>
        public int? GetRollScore(RollNumber rollNumber) {
            return _rolls.Find((roll) => roll.RollNumber == rollNumber).NumOfPinsKnocked;
        }

        public BowlingFrame(int frame, IBowlingGameConfig config) {
            _gameConfig = config;
            FrameNumber = frame;
        }

        /// <summary>
        /// After the player rolls the ball, the frame records the score for that roll.
        /// </summary>
        /// <param name="numOfPinsKnocked">Pins knocked down by the bowling ball for a roll</param>
        public void SetNumOfPinsKnocked(int numOfPinsKnocked) {
            RollNumber rollNumber = CurrentRollNumber + 1;
            _rolls.Add(new Roll(
                rollNumber: rollNumber,
                maxPins: _gameConfig.MaxPins,
                numOfPinsKnocked: numOfPinsKnocked
            ));
        }

        /// <summary>
        /// The bonus depends on other frames's scores.
        /// </summary>
        /// <param name="bonus"></param>
        public void SetBonus(int bonus) => _bonus = bonus;

        /// <summary>
        /// Determines if all the rolls have been performed in the frame.
        /// </summary>
        public bool IsFinished => !IsLastFrame && CurrentRollNumber == RollNumber.Second ||
                                  CurrentRollNumber == RollNumber.Third ||
                                  !IsLastFrame && HasStrike;

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
    }
}