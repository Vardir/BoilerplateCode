using System;

namespace Vardirsoft.Shared.MVVM
{
    public class RelayParametrizedCommand : BaseCommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;

        public RelayParametrizedCommand(Action<object> action, Func<object, bool> canExecute)
        {
            EnsureAction(action);

            _action = action;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _action(parameter);
    }
}