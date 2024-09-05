using UnityEngine;

namespace player {
    public class Player : MonoBehaviour {
        [Header("References")]
        [SerializeField] private InputReader _input;

        [Header("Object to Throw")]
        [Tooltip("The object the player is going to throw.")]
        [SerializeField] Throwable _object;

        [Header("Throw Postions")]
        [Tooltip("The position the player holds the object while in the HoldState.")]
        [SerializeField] Transform _holdPosition;
        [Tooltip("The position the player holds the object while swinging before throwing the object.")]
        [SerializeField] Transform _swingPosition;

        public Throwable Item => _object;
        public Transform HoldPosition => _holdPosition;
        public Transform SwingPosition => _swingPosition;
        public InputReader Input => _input;
    }
}
