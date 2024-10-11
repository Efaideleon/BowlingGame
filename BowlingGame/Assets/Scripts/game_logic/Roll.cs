using BowlingGameEnums;
using System;

namespace game_logic
{
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
}