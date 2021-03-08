using AP8POSecretary.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class SecretaryViewModelAbstractFactory : ISecretaryViewModelAbstractFactory
    {
        private readonly ISecretaryViewModelFactory<GroupsViewModel> _groupsViewModelFactory;
        private readonly ISecretaryViewModelFactory<SubjectsViewModel> _subjectsViewModelFactory;

        public SecretaryViewModelAbstractFactory(ISecretaryViewModelFactory<GroupsViewModel> groupsViewModelFactory, 
            ISecretaryViewModelFactory<SubjectsViewModel> subjectsViewModelFactory)
        {
            _groupsViewModelFactory = groupsViewModelFactory;
            _subjectsViewModelFactory = subjectsViewModelFactory;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Groups:
                    return _groupsViewModelFactory.CreateViewModel();
                case ViewType.WorkingLabels:
                    return new WorkingLabelsViewModel();
                case ViewType.Subjects:
                    return _subjectsViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("View type doesnt exist");
            }
        }
    }
}
