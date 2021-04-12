using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class SettingsViewModelFactory : ISecretaryViewModelFactory<SettingsViewModel>
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<WorkingPointsWeight> _workingPointsWeight;

        public SettingsViewModelFactory(IDataService<Employee> employeeDataService
            , IDataService<Group> groupDataService
            , IDataService<Subject> subjectDataService
            , IDataService<WorkingLabel> workingLabelDataService
            , IDataService<WorkingPointsWeight> workingPointsWeight)
        {
            _employeeDataService = employeeDataService;
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
            _workingLabelDataService = workingLabelDataService;
            _workingPointsWeight = workingPointsWeight;
        }

        public SettingsViewModel CreateViewModel()
        {
            return new SettingsViewModel(_employeeDataService,
                _groupDataService, 
                _subjectDataService, 
                _workingLabelDataService,
                _workingPointsWeight);
        }
    }
}
