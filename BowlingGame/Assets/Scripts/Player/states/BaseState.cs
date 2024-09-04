using UnityEngine;
using state_machine;
using player;

namespace states
{
    // A class that represents the base state for the player
    public abstract class BaseState: IState
    {
        protected readonly PlayerController _player;
        protected readonly Animator _animator;

        protected static readonly int NormalHash = Animator.StringToHash("Normal");
        protected static readonly int ThrowHash = Animator.StringToHash("Throw");
        protected static readonly int HoldingHash = Animator.StringToHash("Holding");

        protected const float _crossFadeDuration = 0.1f;

        protected BaseState(PlayerController player, Animator animator)
        {
            _player = player;
            _animator = animator;
        }

        public virtual void OnEnter()
        {
            // noop
        }

        public virtual void Update()
        {
            // noop;
        }

        public virtual void FixedUpdate()
        {
            // noop;
        }

        public virtual void OnExit()
        {
            // noop;
        }
    }
}