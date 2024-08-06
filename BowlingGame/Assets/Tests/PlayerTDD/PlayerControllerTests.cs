using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class PlayerControllerTests 
{
    public class PlayerControllerInstance: InputTestFixture
    {
        private Keyboard keyboard;
        private PlayerControllerTDD playerController;

        [UnitySetUp]
        public override void Setup()
        {
            base.Setup();
            var gameObject = new GameObject();
            gameObject.AddComponent<PlayerControllerTDD>();

            playerController = gameObject.GetComponent<PlayerControllerTDD>();

            keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [UnityTearDown]
        public override void TearDown()
        {
            Object.Destroy(playerController.gameObject);
            base.TearDown();
        }

        [Test]
        public void Keyboard_Is_Recognized()
        {
            Assert.IsNotNull(Keyboard.current);
        }        

        [Test]
        public void Test_Player_Controller_Is_Created()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<PlayerControllerTDD>();
        }

        [UnityTest]
        public IEnumerator Left_Arrow_Key_Points_Vector_To_Left()
        {
            // Arrange
            Vector2 movementInput = Vector2.zero;

            playerController.OnMoveStarted += (motion) =>
            {
                movementInput = motion;
            };

            // Act
            Press(keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.1f);

            // Assert
            Assert.AreEqual(Vector2.left, movementInput);
        }

        [UnityTest]
        public IEnumerator Right_Arrow_Key_Points_Vector_To_Right()
        {
            // Arrange
            Vector2 movementInput = Vector2.zero;

            playerController.OnMoveStarted += (motion) =>
            {
                movementInput = motion;
            };

            // Act
            Press(keyboard.rightArrowKey);
            yield return new WaitForSeconds(0.1f);

            // Assert
            Assert.AreEqual(Vector2.right, movementInput);
        }

        [UnityTest]
        public IEnumerator Releasing_Left_Arrow_Key_Sets_Move_Vector_To_0()
        {
            Vector2 movementInput = Vector2.left;

            playerController.OnMoveCancelled += (motion) => 
            {
                movementInput = motion;
            };

            Press(keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.1f);
            Release(keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(Vector2.zero, movementInput); 
        }

        [UnityTest]
        public IEnumerator Pressing_Space_To_Start_Charging()
        {
            bool charging = false;
            playerController.OnChargeStarted += () =>
            {
                charging = true;
            };

            Press(keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(charging);
        }

        [UnityTest]
        public IEnumerator Releasing_Spacing_To_Stop_Charing()
        {
            bool charging = true;
            playerController.OnChargeCancelled += () =>
            {
                charging = false;
            };

            Press(keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);
            Release(keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);

            Assert.IsFalse(charging);
        }
    }
}
