using UnityEngine;
using player;

namespace states
{
    public class HoldingState : BaseState
    {
        public HoldingState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            _animator.CrossFade(HoldingHash, _crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            _player.HandleMovement();
        }
    }
}