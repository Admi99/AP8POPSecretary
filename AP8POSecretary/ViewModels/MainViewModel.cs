using AP8POSecretary.State.Navigators;
using AP8POSecretary.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly ISecretaryViewModelAbstractFactory _secretaryViewModelAbstractFactory;

        public MainViewModel(INavigator navigator, ISecretaryViewModelAbstractFactory secretaryViewModelAbstractFactory)
        {
            Navigator = navigator;
            _secretaryViewModelAbstractFactory = secretaryViewModelAbstractFactory;
            ViewType viewType = ViewType.Subjects;
            Navigator.CurrentViewModel = _secretaryViewModelAbstractFactory.CreateViewModel(viewType);
        }

       
    }
}
