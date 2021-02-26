using AP8POSecretary.Commands;
using AP8POSecretary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AP8POSecretary.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel _currectViewModel;
        public BaseViewModel CurrentViewModel {
            get
            {
                return _currectViewModel;
            }
            set
            {
                _currectViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
