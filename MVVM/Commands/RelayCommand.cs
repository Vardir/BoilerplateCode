using System;

namespace Vardirsoft.Shared.MVVM
{
    public class RelayCommand : BaseCommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action action, Func<bool> canExecute = null)
        {
            EnsureAction(action);

            _action = action;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object _) => _canExecute?.Invoke() ?? true;

        public override void Execute(object _) => _action();
    }

    public class RelayCommand<T> : BaseCommand
    {
        private readonly Action<T> _action;
        private readonly Func<T, bool> _canExecute;
        private readonly Func<object, T> _conversion;

        public RelayCommand(Action<T> action, Func<T, bool> canExecute = null, Func<object, T> conversion = null)
        {
            EnsureAction(action);

            _action = action;
            _canExecute = canExecute;
            _conversion = conversion;
        }

        public override bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            var castedPatameter = _conversion != null ? _conversion(parameter) : (T)parameter;

            return _canExecute(castedPatameter);
        }

        public override void Execute(object parameter)
        {
            var castedPatameter = _conversion != null ? _conversion(parameter) : (T)parameter;

            _action(castedPatameter);
        }
    }
}