using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AStudio.Models;
using AStudio.Views;
using AStudio.VM;

namespace AStudio.Commands
{
    public class VisualizeFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> action;
        Func<object, bool> canExecute;

        public VisualizeFileCommand(Action<object> action, Func<object, bool> canExecute = null)
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
