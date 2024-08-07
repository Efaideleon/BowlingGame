using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class PlayerAnimationTests
{
    public class MoveAnimations: InputTestFixture
    {
        private PlayerAnimationsTDD playerAnimations;
        private Animator playerAnimator;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            var gameObject = new GameObject();
            gameObject.AddComponent<Animator>();
            gameObject.AddComponent<PlayerAnimationsTDD>();

            playerAnimations = gameObject.GetComponent<PlayerAnimationsTDD>();
            playerAnimator = gameObject.GetComponent<Animator>();
            playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerAnimatorController");
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(playerAnimations);
            base.TearDown();
        }

        [Test]
        public void Moving_To_Left_Sets_Player_Moving_Left_Animation()
        {
            Assert.IsTrue(playerAnimator.GetBool("IsMovingLeft"));
        }
    }
}
