using AP8POSecretary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AP8POSecretary.State.Navigators
{
    public enum ViewType
    {
        Subjects,
        Groups,
        Employees,
        GroupsManagment,
        WorkingLabels,
        Settings
    };
    public interface INavigator
    {
        public BaseViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
