using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Assets.Tests.Mocks
{
    public class MockCharacterController: CharacterController
    {
        public new void Move(Vector3 motion)
        {
            Debug.Log("Mock Move in MockPlayerController");
        }
    }
}
