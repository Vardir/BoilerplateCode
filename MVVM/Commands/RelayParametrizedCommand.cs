using System;

namespace BPCode.MVVM
{
    public class RelayParametrizedCommand : BaseCommand
    {
        private Action<object> action;
        private Func<object, bool> canExecute;

        public RelayParametrizedCommand(Action<object> action, Func<object, bool> canExecute)
        {
            EnsureAction(action);

            this.action = action;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => action(parameter);
    }
}