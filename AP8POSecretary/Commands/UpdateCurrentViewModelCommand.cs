using AP8POSecretary.State.Navigators;
using AP8POSecretary.ViewModels;
using AP8POSecretary.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AP8POSecretary.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly ISecretaryViewModelAbstractFactory _secretaryViewModelAbstractFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, ISecretaryViewModelAbstractFactory secretaryViewModelAbstractFactory)
        {
            _navigator = navigator;
            _secretaryViewModelAbstractFactory = secretaryViewModelAbstractFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                _navigator.CurrentViewModel = _secretaryViewModelAbstractFactory.CreateViewModel(viewType);
            }
        }
    }
}
