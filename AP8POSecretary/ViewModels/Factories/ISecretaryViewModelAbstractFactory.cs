using AP8POSecretary.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public interface ISecretaryViewModelAbstractFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
