using System;

namespace state_machine {
    public class FuncPredicate : IPredicate {
        readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func) {
            _func = func;
        }
        public bool Evaluate() => _func.Invoke();
    }
}