using player;
using UnityEngine;

namespace states
{
    public class ThrowState : BaseState
    {
        public ThrowState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            Debug.Log("Throw State");
            _animator.CrossFade(ThrowHash, _crossFadeDuration);
        }
    }
}