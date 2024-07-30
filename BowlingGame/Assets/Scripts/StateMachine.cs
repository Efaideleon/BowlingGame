using System.Collections.Generic;

namespace StateMachine
{
    public interface IState<T>
    {
        public void Enter(T owner);
        public void Execute(T owner);
        public void Exit(T owner);
    }

    public class StateMachine<T>
    {
        private readonly T _owner;
        private IState<T> currentState;

        public StateMachine(T owner)
        {
            _owner = owner;
        }
    
        public void ChangeState(IState<T> newState)
        {
            currentState?.Exit(_owner);
            currentState = newState;
            currentState.Enter(_owner);
            currentState.Execute(_owner);
        }
    }
}
