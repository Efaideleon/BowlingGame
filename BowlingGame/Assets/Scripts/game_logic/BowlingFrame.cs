using BowlingGameEnums;
using System;

namespace game_logic {
    public class Roll {
        private readonly RollNumber _rollNumber;
        private int? _numOfPinsKnocked;
        private readonly int _maxPins;

        public int? NumOfPinsKnocked {
            get { return _numOfPinsKnocked; }
            private set { _numOfPinsKnocked = value < 0 ? null : value; }
        }

        public int NumberOfPinsKnockedOrZero => NumOfPinsKnocked ?? 0;
        public RollNumber RollNumber => _rollNumber;

        public Roll(RollNumber rollNumber, int maxPins) {
            _rollNumber = rollNumber;
            _maxPins = maxPins;
        }

        public void SetNumOfPinsKnocked(int numOfPinsKnocked) {
            if (numOfPinsKnocked < 0 || numOfPinsKnocked > _maxPins) {
                throw new ArgumentOutOfRangeException(
                    nameof(numOfPinsKnocked), "pinsKnocked must be between 0 and " + _maxPins
                );
            }
            _numOfPinsKnocked = numOfPinsKnocked;
        }

        public bool IsStrike => NumberOfPinsKnockedOrZero == _maxPins;
    }

    public class BowlingFrame {
        readonly IBowlingGameConfig _gameConfig;
        private int _frameNumber;
        private int _bonus = 0;
        private readonly Roll _firstRoll;
        private readonly Roll _secondRoll;
        private readonly Roll _thirdRoll;
        private Roll CurrentRoll; 

        public RollNumber CurrentRollNumber => CurrentRoll.RollNumber;

        // Starts from 1.
        public int FrameNumber {
            get => _frameNumber;
            private set => _frameNumber = value < 0 
                    ?  throw new ArgumentOutOfRangeException(nameof(value), "frame number must be greater or equal to 0.")
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
            _thirdRoll = new(RollNumber.Second, _gameConfig.MaxPins);
            CurrentRoll = _firstRoll;
            FrameNumber = frame;
        }

        public void SetCurrentRollPinsKnocked(int numOfPinsKnocked) => CurrentRoll.SetNumOfPinsKnocked(numOfPinsKnocked);

        public void MoveToNextRoll() {
            CurrentRoll = CurrentRollNumber switch {
                RollNumber.First => _secondRoll,
                RollNumber.Second => _thirdRoll,
                _ => CurrentRoll
            };
        }

        public bool IsFinished =>(CurrentRoll.IsStrike && !IsLastFrame)
                                 || (CurrentRollNumber == RollNumber.Second && !IsLastFrame)
                                 || CurrentRollNumber == RollNumber.Third;

        public void SetBonus(int bonus) => _bonus = bonus;

        public bool HasStrike => _firstRoll.IsStrike || _secondRoll.IsStrike;
        public bool IsSpare => !HasStrike && _firstRoll.NumberOfPinsKnockedOrZero + _secondRoll.NumberOfPinsKnockedOrZero == _gameConfig.MaxPins;
        public bool IsLastFrame => FrameNumber == _gameConfig.MaxFrames;
    }
}