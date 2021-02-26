using AP8POSecretary.State.Navigators;
using AP8POSecretary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AP8POSecretary.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                switch (parameter)
                {
                    case ViewType.Groups:
                        _navigator.CurrentViewModel = new GroupsViewModel();
                        break;
                    case ViewType.WorkingLabels:
                        _navigator.CurrentViewModel = new WorkingLabelsViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
