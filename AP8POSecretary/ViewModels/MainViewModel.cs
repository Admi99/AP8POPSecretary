using AP8POSecretary.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
