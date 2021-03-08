using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AP8POSecretary.Commands
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _predicate;

        public RelayCommand(Action<object> execute, Predicate<object> predicate)
        {
            if (execute == null)
                throw new NullReferenceException("execute");
            _execute = execute;
            _predicate = predicate;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null ? true : _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
