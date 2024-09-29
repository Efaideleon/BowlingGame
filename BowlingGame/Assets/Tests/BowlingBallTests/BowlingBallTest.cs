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
        var mockConfig = Substitute.For<IBowlingGameConfig>();
        mockConfig.MaxFrames.Returns(10);
        mockConfig.MaxPins.Returns(10);

        var bowlingGame = new BowlingGame(mockConfig);

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
