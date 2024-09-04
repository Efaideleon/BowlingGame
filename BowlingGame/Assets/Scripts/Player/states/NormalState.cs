using player;
using UnityEngine;

namespace states
{
    public class NormalState : BaseState
    {
        public NormalState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            _animator.CrossFade(NormalHash, _crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            _player.HandleMovement();
        }
    }
}