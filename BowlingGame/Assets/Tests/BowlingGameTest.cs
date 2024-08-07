using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine.Scripting;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Assets.Tests.Mocks;

public class BowlingGameTest : InputTestFixture
{
    // [Test]
    // public void Player_ComponentExists()
    // {
    //     // Arrange
    //     GameObject gameObject = new();
    //     gameObject.AddComponent<MockCharacterController>(); // <- Make a Mock instead
    //     gameObject.AddComponent<PlayerController>();
    //     gameObject.AddComponent<PlayerMovement>();
    //     gameObject.AddComponent<PlayerActions>();
    //     gameObject.AddComponent<Player>();

    //     // Act
    //     Player player = gameObject.GetComponent<Player>();

    //     // Assert
    //     Assert.IsNotNull(player);
    // }


    // [Test]
    // public void BowlingGame_Roll_ValidPins_UpdatesScore()
    // {
    //     // Arrange
    //     var game = new BowlingGame();

    //     // Act
    //     game.Roll(5);
    //     game.Roll(3);

    //     // Assert
    //     Assert.AreEqual(18, game.Score);
    // }

    // [Test]
    // public void BowlingGame_Roll_Strike_AddsNextTwoRollBonus()
    // {
    //     // Arrange
    //     var game = new BowlingGame();

    //     // Act
    //     game.Roll(10);
    //     game.Roll(3);
    //     game.Roll(6);

    //     // Assert
    //     Assert.AreEqual(28, game.Score);
    // }

    // [Test]
    // public void BowlingGame_Roll_PerfectGame_Scores300()
    // {
    //     // Arrange
    //     var game = new BowlingGame();

    //     // Act
    //     for (int i = 0; i < 12; i++)
    //     {
    //         game.Roll(10);
    //     }

    //     // Assert
    //     Assert.AreEqual(300, game.Score);
    // }

    // [UnityTest]
    // public IEnumerator BowlingGamePhysics_Charge_IncreasesThrowForce()
    // {
    //     // Arrange
    //     var gameObject = new GameObject();
    //     var bowlingBallPhysics = gameObject.AddComponent<BowlingBallPhysics>();

    //     // Act
    //     bowlingBallPhysics.StartCharging();
    //     yield return new WaitForSeconds(1f);
    //     bowlingBallPhysics.StopCharging();

    //     // Assert
    //     Assert.Greater(bowlingBallPhysics._throwForce.magnitude, 0f, "Throw force should be greated than 0 after charging.");
    // }

    // [UnityTest]
    // public IEnumerator BowlingViewModel_Roll_InvokesGameStateChange()
    // {
    //     // Arrange
    //     var viewModel = new GameObject().AddComponent<BowlingViewModel>();
    //     bool gameStatechanged = false;
    //     viewModel.OnGameStateChange += () => gameStatechanged = true;

    //     // Act
    //     viewModel.Roll(5);

    //     // Assert
    //     yield return null;
    //     Assert.IsTrue(gameStatechanged);
    // }

    // [UnityTest]
    // public IEnumerator PinManger_WaitForPinsToSettle_InvokesEventWhenSettled()
    // {
    //     // Arrange
    //     var gameObject = new GameObject();
    //     var pinManager = gameObject.AddComponent<PinManager>();
    //     var pinObject = new GameObject();
    //     var pin = pinObject.AddComponent<Pin>();

    //     bool eventInvoked = false;
    //     pinManager.OnPinsSettled += () => eventInvoked = true;

    //     // Act
    //     pinManager.CheckForPinsToSettle();
    //     yield return new WaitForSeconds(pinManager.pinSettleTime + 0.1f);

    //     // Assert
    //     Assert.IsTrue(eventInvoked);
    // }

    // [Test]
    // public void BowlingBall_Hold_SetsParentAndPosition()
    // {
    //     // Arrange
    //     var bowlingBall = new GameObject().AddComponent<BowlingBall>();
    //     var parent = new GameObject().transform;

    //     // Act
    //     bowlingBall.Hold(parent);

    //     // Assert
    //     Assert.AreEqual(parent, bowlingBall.transform.parent);
    //     Assert.AreEqual(Vector3.zero, bowlingBall.transform.localPosition);
    //     Assert.AreEqual(Quaternion.identity, bowlingBall.transform.localRotation);
    // }

    // [Test]
    // public void BowlingBall_Swing_SetsParentAndPosition()
    // {
    //     // Arrange
    //     var bowlingBall = new GameObject().AddComponent<BowlingBall>();
    //     var parent = new GameObject().transform;

    //     // Act
    //     bowlingBall.Swing(parent);

    //     // Assert
    //     Assert.AreEqual(parent, bowlingBall.transform.parent);
    //     Assert.AreEqual(Vector3.zero, bowlingBall.transform.localPosition);
    //     Assert.AreEqual(Quaternion.identity, bowlingBall.transform.localRotation);
    // }

    // [Test]
    // public void BowlingBall_Throw_CallsThrowOnPhysics()
    // {
    //     // Arrange
    //     var bowlingBall = new GameObject().AddComponent<BowlingBall>();
    //     var physicsMock = Substitute.For<BowlingBallPhysics>();
    //     bowlingBall.BowlingBallPhysics = physicsMock;

    //     // Act
    //     bowlingBall.Throw();

    //     // Assert
    //     physicsMock.Received(1).Throw();
    // }
}
