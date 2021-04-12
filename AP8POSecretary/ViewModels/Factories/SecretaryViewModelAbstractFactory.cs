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
        private readonly ISecretaryViewModelFactory<EmployeesViewModel> _employeeViewModelFactory;
        private readonly ISecretaryViewModelFactory<GroupsManagmentViewModel> _groupsManagmentViewModelFactory;
        private readonly ISecretaryViewModelFactory<WorkingLabelsViewModel> _workingLabelsViewModelFactory;
        private readonly ISecretaryViewModelFactory<SettingsViewModel> _settingsViewModelFactory;

        public SecretaryViewModelAbstractFactory(ISecretaryViewModelFactory<GroupsViewModel> groupsViewModelFactory, 
            ISecretaryViewModelFactory<SubjectsViewModel> subjectsViewModelFactory,
            ISecretaryViewModelFactory<EmployeesViewModel> employeeViewModelFactory,
            ISecretaryViewModelFactory<GroupsManagmentViewModel> groupsManagmentViewModelFactory,
            ISecretaryViewModelFactory<WorkingLabelsViewModel> workingLabelsViewModelFactory,
            ISecretaryViewModelFactory<SettingsViewModel> settingsViewModelFactory
            )
        {
            _groupsViewModelFactory = groupsViewModelFactory;
            _subjectsViewModelFactory = subjectsViewModelFactory;
            _employeeViewModelFactory = employeeViewModelFactory;
            _groupsManagmentViewModelFactory = groupsManagmentViewModelFactory;
            _workingLabelsViewModelFactory = workingLabelsViewModelFactory;
            _settingsViewModelFactory = settingsViewModelFactory;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Groups:
                    return _groupsViewModelFactory.CreateViewModel();
                case ViewType.WorkingLabels:
                    return _workingLabelsViewModelFactory.CreateViewModel();
                case ViewType.Subjects:
                    return _subjectsViewModelFactory.CreateViewModel();
                case ViewType.Employees:
                    return _employeeViewModelFactory.CreateViewModel();
                case ViewType.GroupsManagment:
                    return _groupsManagmentViewModelFactory.CreateViewModel();
                case ViewType.Settings:
                    return _settingsViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("View type doesnt exist");
            }
        }
    }
}
