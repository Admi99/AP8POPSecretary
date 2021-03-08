﻿using AP8POSecretary.Commands;
using AP8POSecretary.ViewModels;
using AP8POSecretary.ViewModels.Factories;
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

        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public Navigator(ISecretaryViewModelAbstractFactory secretaryViewModelAbstractFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, secretaryViewModelAbstractFactory);
        }
    }
}
