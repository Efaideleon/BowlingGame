using player;
using UnityEngine;

namespace states
{
    public class ThrowState : BaseState
    {
        public ThrowState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            _animator.CrossFade(ThrowHash, _crossFadeDuration);
        }
    }
}