using System;
using System.Windows.Input;

namespace Vardirsoft.Shared.MVVM
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
        public void NotifyCanExecuteChanged(object sender, EventArgs e) => CanExecuteChanged?.Invoke(sender, e);

        protected static void EnsureAction(Delegate action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Relay command must have an action to execute");
        }
    }
}