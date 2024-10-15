using BowlingGameEnums;
using System;

namespace game_logic
{
    public readonly struct Roll {
        public readonly RollNumber RollNumber;
        public readonly int NumOfPinsKnocked;
        public readonly int MaxPins;

        public Roll(RollNumber rollNumber, int maxPins, int numOfPinsKnocked) {
            if (numOfPinsKnocked < 0 || numOfPinsKnocked > maxPins) {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(numOfPinsKnocked),
                    message: "pinsKnocked must be between 0 and " + maxPins
                );
            }
            RollNumber = rollNumber;
            MaxPins = maxPins;
            NumOfPinsKnocked = numOfPinsKnocked;
        }

        public readonly bool IsStrike => NumOfPinsKnocked == MaxPins;
    }
}