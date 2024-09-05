using state_machine;
using states;
using UnityEngine;

namespace player {
    public class StateMachineBuilder {
        public static StateMachine Build(PlayerController playerController, Animator animator) {
            var stateMachine = new StateMachine();

            // Declare States
            var holdingState = new HoldingState(playerController, animator);
            var normalState = new NormalState(playerController, animator);
            var throwState = new ThrowState(playerController, animator);

            // Transitions
            stateMachine.AddTransition(holdingState, throwState, new FuncPredicate(() => playerController.ChargedThrowAction.IsRunning));
            
            stateMachine.AddTransition(throwState, normalState, new FuncPredicate(() => !playerController.ChargedThrowAction.IsRunning));

            stateMachine.AddTransition(normalState, holdingState, new FuncPredicate(() => playerController.ChargedThrowAction.IsReady));

            // Set inital state
            stateMachine.SetState(holdingState);

            return stateMachine;
        }
    }
}
