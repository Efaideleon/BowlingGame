using UnityEngine;

namespace player {
    public class PlayerAnimatorController : MonoBehaviour {
        Animator _animator;
        PlayerController _playerController;

        // Animation Hashes
        static readonly int DirectionHash = Animator.StringToHash("Direction");

        void Awake() {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
        }

        void Update() {
            _animator.SetFloat(DirectionHash, _playerController.GetDirection().x);
        }
    }
}
