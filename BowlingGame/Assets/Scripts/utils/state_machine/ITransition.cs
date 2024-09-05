namespace state_machine {
    public interface ITransition {
        IState To { get; }
        IPredicate Condition { get; }
    }
}