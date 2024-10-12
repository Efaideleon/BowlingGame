using BowlingGameEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace game_logic {
    public class BowlingFrame {
        readonly IBowlingGameConfig _gameConfig;
        private int _frameNumber;
        private List<Roll> _rolls;
        private int _bonus = 0;
        private int _currentRollIndex = 0;
        private Roll CurrentRoll => _rolls[_currentRollIndex];

        public RollNumber CurrentRollNumber => CurrentRoll.RollNumber;

        // Starts from 1.
        public int FrameNumber {
            get => _frameNumber;
            private set => _frameNumber = value <= 0
                    ? throw new ArgumentOutOfRangeException(nameof(value), "frame number must be greater than 0.")
                    : value;
        }

        public int? GetRollScore(RollNumber rollNumber) => _rolls.Find((roll) => roll.RollNumber == rollNumber).NumOfPinsKnocked;

        public int Score => AllRollsNumsOfPinsKnocked + _bonus;

        public BowlingFrame(int frame, IBowlingGameConfig config, int numOfRolls) {
            _gameConfig = config;
            FrameNumber = frame;
            InitializeRolls(numOfRolls);
        }

        private void InitializeRolls(int numOfRolls) {
            _rolls = Enumerable.Range(0, numOfRolls).Select(i => new Roll((RollNumber)(i + 1), _gameConfig.MaxPins)).ToList();
        }

        public void SetNumOfPinsKnocked(int numOfPinsKnocked) => CurrentRoll.SetNumOfPinsKnocked(numOfPinsKnocked);

        public void MoveToNextRoll() {
            if (_currentRollIndex < _rolls.Count - 1) {
                _currentRollIndex++;
            }
        }

        public void SetBonus(int bonus) => _bonus = bonus;

        /// <summary>
        /// Determines if all the rolls have been performed in the frame.
        /// </summary>
        public bool IsFinished => _rolls.All((roll) => roll.IsFinished) || !IsLastFrame && HasStrike;

        /// <summary>
        /// Checks if any of the rolls in the frame is a strike
        /// </summary>
        public bool HasStrike => _rolls.Any((roll) => roll.IsStrike);

        /// <summary>
        /// Checks if the frame is a spare.
        /// </summary>
        public bool IsSpare => !HasStrike && AllRollsNumsOfPinsKnocked == _gameConfig.MaxPins;

        /// <summary>
        /// Checks if this is the last frame in the game.
        /// </summary>
        public bool IsLastFrame => FrameNumber == _gameConfig.MaxFrames;
        private int AllRollsNumsOfPinsKnocked => _rolls.Sum(roll => roll.NumberOfPinsKnockedOrZero);
    }
}