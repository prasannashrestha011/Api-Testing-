using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSharing.Commands
{
    public class relayCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Func<object,bool> canExecute;
        public Action<object> execute;
        public relayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            execute(parameter);
        }

    }
}
