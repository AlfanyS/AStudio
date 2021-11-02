using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AStudio.Commands;
using AStudio.Models;
using AStudio.Views;
using Microsoft.Win32;

namespace AStudio.VM
{
    public class TextPresenterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropetyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ICommand CloseCommand { get; } = new CloseWindowCommand((sender) => { TextPresenterWindow.Instance.Close(); });
        public ICommand MinimizeCommand { get; } = new MinimizeWindowCommand((sender) => { TextPresenterWindow.Instance.WindowState = System.Windows.WindowState.Minimized; });

    }
}
