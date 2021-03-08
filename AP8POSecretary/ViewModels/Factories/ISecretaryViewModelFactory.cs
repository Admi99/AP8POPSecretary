using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public interface ISecretaryViewModelFactory<T> where T : BaseViewModel
    {
        T CreateViewModel();
    }
}
