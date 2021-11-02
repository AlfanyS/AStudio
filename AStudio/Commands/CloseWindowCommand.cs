using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AStudio.Views;

namespace AStudio.Commands
{
    class CloseWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> action;
        Func<object, bool> canExecute;

        public CloseWindowCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            action.Invoke(parameter);
        }
    }
}
