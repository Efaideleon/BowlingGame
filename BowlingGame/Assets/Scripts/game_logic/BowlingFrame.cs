using BowlingGameEnums;
using System;

namespace game_logic {
    public class BowlingFrame {
        readonly IBowlingGameConfig _gameConfig;
        private int _frameNumber;
        private readonly Roll _firstRoll;
        private readonly Roll _secondRoll;
        private readonly Roll _thirdRoll;
        private int _bonus = 0;
        private Roll CurrentRoll;

        public RollNumber CurrentRollNumber => CurrentRoll.RollNumber;

        // Starts from 1.
        public int FrameNumber {
            get => _frameNumber;
            private set => _frameNumber = value < 0
                    ? throw new ArgumentOutOfRangeException(nameof(value), "frame number must be greater or equal to 0.")
                    : value;
        }

        public int? FirstRollScore => _firstRoll.NumOfPinsKnocked;
        public int? SecondRollScore => _secondRoll.NumOfPinsKnocked;
        public int? ThirdRollScore => _thirdRoll.NumOfPinsKnocked;

        public int Score => _firstRoll.NumberOfPinsKnockedOrZero + _secondRoll.NumberOfPinsKnockedOrZero + _thirdRoll.NumberOfPinsKnockedOrZero + _bonus;

        public BowlingFrame(int frame, IBowlingGameConfig config) {
            _gameConfig = config;
            _firstRoll = new(RollNumber.First, _gameConfig.MaxPins);
            _secondRoll = new(RollNumber.Second, _gameConfig.MaxPins);
            _thirdRoll = new(RollNumber.Third, _gameConfig.MaxPins);
            CurrentRoll = _firstRoll;
            FrameNumber = frame;
        }

        public void SetNumOfPinsKnocked(int numOfPinsKnocked) => CurrentRoll.SetNumOfPinsKnocked(numOfPinsKnocked);

        public void MoveToNextRoll() {
            CurrentRoll = CurrentRollNumber switch {
                RollNumber.First => _secondRoll,
                RollNumber.Second => _thirdRoll,
                _ => CurrentRoll
            };
        }

        public void SetBonus(int bonus) => _bonus = bonus;

        /// <summary>
        /// Determines if all the rolls have been performed in the frame.
        /// </summary>
        public bool IsFinished => (CurrentRoll.IsStrike && !IsLastFrame)
                                 || (CurrentRollNumber == RollNumber.Second && !IsLastFrame)
                                 || CurrentRollNumber == RollNumber.Third;

        /// <summary>
        /// Checks if the frame contains a strike.
        /// </summary>
        public bool HasStrike => _firstRoll.IsStrike || _secondRoll.IsStrike;

        /// <summary>
        /// Checks if the frame is a spare.
        /// </summary>
        public bool IsSpare => !HasStrike && _firstRoll.NumberOfPinsKnockedOrZero + _secondRoll.NumberOfPinsKnockedOrZero == _gameConfig.MaxPins;

        /// <summary>
        /// Checks if this frame is the last in the game.
        /// </summary>
        public bool IsLastFrame => FrameNumber == _gameConfig.MaxFrames;
    }
}