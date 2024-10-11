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
        bowlingGame.ProcessRoll(5);
        bowlingGame.ProcessRoll(5);
        // Frame 2
        bowlingGame.ProcessRoll(4);
        bowlingGame.ProcessRoll(5);
        // Frame 3
        bowlingGame.ProcessRoll(8);
        bowlingGame.ProcessRoll(2);
        // Frame 4
        bowlingGame.ProcessRoll(10);
        // Frame 5
        bowlingGame.ProcessRoll(0);
        bowlingGame.ProcessRoll(10);
        // Frame 6
        bowlingGame.ProcessRoll(10);
        // Frame 7
        bowlingGame.ProcessRoll(6);
        bowlingGame.ProcessRoll(2);
        // Frame 8
        bowlingGame.ProcessRoll(10);
        // Frame 9
        bowlingGame.ProcessRoll(4);
        bowlingGame.ProcessRoll(6);
        // Frame 10
        bowlingGame.ProcessRoll(10);
        bowlingGame.ProcessRoll(10);

        Assert.AreEqual(169, bowlingGame.TotalScore);
    }

    [Test]
    public void BowlingBall_Frame1_IsFinished() {
        BowlingGameConfig _config = ScriptableObject.CreateInstance<BowlingGameConfig>(); // Or use a mocking framework
        _config.MaxFrames = 10;
        _config.MaxPins = 10;


        BowlingGame bowlingGame = ScriptableObject.CreateInstance<BowlingGame>();
        bowlingGame.Config = _config;
        bowlingGame.OnValidate();

        // Frame 1
        bowlingGame.ProcessRoll(5);
        bowlingGame.ProcessRoll(5);

        Assert.AreEqual(bowlingGame.AllFrames[0].IsFinished, true);
    }
    
    [Test]
    public void BowlingBall_Frame2_IsFinished() {
        BowlingGameConfig _config = ScriptableObject.CreateInstance<BowlingGameConfig>(); // Or use a mocking framework
        _config.MaxFrames = 10;
        _config.MaxPins = 10;


        BowlingGame bowlingGame = ScriptableObject.CreateInstance<BowlingGame>();
        bowlingGame.Config = _config;
        bowlingGame.OnValidate();

        // Frame 1
        bowlingGame.ProcessRoll(5);
        bowlingGame.ProcessRoll(5);

        // Frame 2
        bowlingGame.ProcessRoll(4);
        bowlingGame.ProcessRoll(5);
        Assert.AreEqual(bowlingGame.AllFrames[1].IsFinished, true);
    }

    [Test]
    public void BowlingBall_Frame3_IsFinished() {
        BowlingGameConfig _config = ScriptableObject.CreateInstance<BowlingGameConfig>(); // Or use a mocking framework
        _config.MaxFrames = 10;
        _config.MaxPins = 10;


        BowlingGame bowlingGame = ScriptableObject.CreateInstance<BowlingGame>();
        bowlingGame.Config = _config;
        bowlingGame.OnValidate();

        // Frame 1
        bowlingGame.ProcessRoll(5);
        bowlingGame.ProcessRoll(5);

        // Frame 2
        bowlingGame.ProcessRoll(4);
        bowlingGame.ProcessRoll(5);

        // Frame 3
        bowlingGame.ProcessRoll(8);
        bowlingGame.ProcessRoll(2);
        Assert.AreEqual(bowlingGame.AllFrames[2].IsFinished, true);
    }

    [Test]
    public void BowlingBall_Frame4_IsFinished() {
        BowlingGameConfig _config = ScriptableObject.CreateInstance<BowlingGameConfig>(); // Or use a mocking framework
        _config.MaxFrames = 10;
        _config.MaxPins = 10;


        BowlingGame bowlingGame = ScriptableObject.CreateInstance<BowlingGame>();
        bowlingGame.Config = _config;
        bowlingGame.OnValidate();

        // Frame 1
        bowlingGame.ProcessRoll(5);
        bowlingGame.ProcessRoll(5);

        // Frame 2
        bowlingGame.ProcessRoll(4);
        bowlingGame.ProcessRoll(5);

        // Frame 3
        bowlingGame.ProcessRoll(8);
        bowlingGame.ProcessRoll(2);

        // Frame 4
        bowlingGame.ProcessRoll(10);
        Assert.AreEqual(bowlingGame.AllFrames[3].IsFinished, true);
    }

    [Test]
    public void BowlingBall_Frame5_IsFinished() {
        BowlingGameConfig _config = ScriptableObject.CreateInstance<BowlingGameConfig>(); // Or use a mocking framework
        _config.MaxFrames = 10;
        _config.MaxPins = 10;


        BowlingGame bowlingGame = ScriptableObject.CreateInstance<BowlingGame>();
        bowlingGame.Config = _config;
        bowlingGame.OnValidate();

        // Frame 1
        bowlingGame.ProcessRoll(5);
        bowlingGame.ProcessRoll(5);

        // Frame 2
        bowlingGame.ProcessRoll(4);
        bowlingGame.ProcessRoll(5);

        // Frame 3
        bowlingGame.ProcessRoll(8);
        bowlingGame.ProcessRoll(2);

        // Frame 4
        bowlingGame.ProcessRoll(10);

        // Frame 5
        bowlingGame.ProcessRoll(0);
        bowlingGame.ProcessRoll(10);
        Assert.AreEqual(bowlingGame.AllFrames[4].IsFinished, true);
    }
}
