using System;

namespace Vardirsoft.Shared.MVVM
{
    public class RelayCommand : BaseCommand
    {
        private Action action;
        private Func<bool> canExecute;

        public RelayCommand(Action action, Func<bool> canExecute = null)
        {
            EnsureAction(action);

            this.action = action;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object _) => canExecute?.Invoke() ?? true;

        public override void Execute(object _) => action();
    }

    public class RelayCommand<T> : BaseCommand
    {
        private Action<T> action;
        private Func<T, bool> canExecute;
        private Func<object, T> conversion;

        public RelayCommand(Action<T> action, Func<T, bool> canExecute = null, Func<object, T> conversion = null)
        {
            EnsureAction(action);

            this.action = action;
            this.canExecute = canExecute;
            this.conversion = conversion;
        }

        public override bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;

            T castedPatameter = conversion != null ? conversion(parameter) : (T)parameter;

            return canExecute(castedPatameter);
        }

        public override void Execute(object parameter)
        {
            T castedPatameter = conversion != null ? conversion(parameter) : (T)parameter;

            action(castedPatameter);
        }
    }
}