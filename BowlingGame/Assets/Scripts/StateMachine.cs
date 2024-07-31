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
        private IState<T> _currentState;

        public StateMachine(T owner)
        {
            _owner = owner;
        }
    
        public void ChangeState(IState<T> newState)
        {
            _currentState?.Exit(_owner);
            _currentState = newState;
            _currentState.Enter(_owner);
            _currentState.Execute(_owner);
        }
    }
}
