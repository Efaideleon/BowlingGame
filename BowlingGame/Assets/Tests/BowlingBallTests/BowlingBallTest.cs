using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BowlingBallTest
{

    [Test]
    public void BowlingBall_TotalScore_Test()
    {

        BowlingGameConfig _config = ScriptableObject.CreateInstance<BowlingGameConfig>(); // Or use a mocking framework
        _config.MaxFrames = 10;
        _config.MaxPins = 10;


        BowlingGame bowlingGame = ScriptableObject.CreateInstance<BowlingGame>();
        bowlingGame.Config = _config;
        bowlingGame.OnValidate();

        // Frame 1
        bowlingGame.Roll(5);
        bowlingGame.Roll(5);
        // Frame 2
        bowlingGame.Roll(4);
        bowlingGame.Roll(5);
        // Frame 3
        bowlingGame.Roll(8);
        bowlingGame.Roll(2);
        // Frame 4
        bowlingGame.Roll(10);
        // Frame 5
        bowlingGame.Roll(0);
        bowlingGame.Roll(10);
        // Frame 6
        bowlingGame.Roll(10);
        // Frame 7
        bowlingGame.Roll(6);
        bowlingGame.Roll(2);
        // Frame 8
        bowlingGame.Roll(10);
        // Frame 9
        bowlingGame.Roll(4);
        bowlingGame.Roll(6);
        // Frame 10
        bowlingGame.Roll(10);
        bowlingGame.Roll(10);

        Assert.AreEqual(169, bowlingGame.TotalScore);
    }
}
