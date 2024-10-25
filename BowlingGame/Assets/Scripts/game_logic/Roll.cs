using BowlingGameEnums;
using System;

namespace game_logic
{
    public class Roll
    {
        private RollNumber _rollNumber;
        private int _numOfPinsKnocked;
        private int _maxPins;

        public RollNumber RollNumber => _rollNumber;
        public int NumOfPinsKnocked => _numOfPinsKnocked;
        public bool IsStrike => NumOfPinsKnocked == _maxPins;
        public bool HasScoreRecorded { get; private set; } = false;

        public Roll(RollNumber rollNumber, int maxPins)
        {
            _rollNumber = rollNumber;
            _maxPins = maxPins;
        }

        public void SetNumOfPinsKnocked(int numOfPinsKnocked)
        {
            if (numOfPinsKnocked < 0 || numOfPinsKnocked > _maxPins)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(numOfPinsKnocked),
                    message: "pinsKnocked must be between 0 and " + _maxPins
                );
            }
            _numOfPinsKnocked = numOfPinsKnocked;
            HasScoreRecorded = true;
        }
    }
}