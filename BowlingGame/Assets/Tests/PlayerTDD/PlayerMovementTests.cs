using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    public class TheMoveMethod: InputTestFixture
    {
        private GameObject playerGameObject;
        private PlayerMovementTDD player;

        [UnitySetUp]
        public override void Setup()
        {
            base.Setup();
            playerGameObject = new GameObject();
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.AddComponent<PlayerMovementTDD>();
            player = playerGameObject.GetComponent<PlayerMovementTDD>(); 
        }

        [UnityTearDown]
        public override void TearDown()
        {
            Object.Destroy(player);
            base.TearDown();
        }

        [Test]
        public void Move_Method_Is_Called_With_Left_Direction()
        {
            // Arrange
            Vector2 expected = 1 * Vector2.left;
            float tolerance = 0.0001f; 

            // Act
            player.Move(Vector2.left);

            // Assert
            Assert.AreEqual(expected.x, player.Direction.x, tolerance);
            Assert.AreEqual(expected.y, player.Direction.y, tolerance);
        }

        [UnityTest]
        public IEnumerator Character_Moves_To_The_Left_When_Passing_Left_Vector_To_Move()
        {
            // Arrange
            float initialX = playerGameObject.transform.position.x;

            // Act
            player.Move(Vector2.left);
            yield return new WaitForSeconds(1f);

            // Assert
            Assert.Less(playerGameObject.transform.position.x, initialX);
        }

        [UnityTest]
        public IEnumerator Character_Moves_To_The_Right_When_Passing_Right_Vector_To_Move()
        {
            // Arrange
            float initialX = playerGameObject.transform.position.x;

            // Act
            player.Move(Vector2.right);
            yield return new WaitForSeconds(1f);

            // Assert
            Assert.Greater(playerGameObject.transform.position.x, initialX);
        }
    }
}