using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class WorkingLabelsViewModelFactory : ISecretaryViewModelFactory<WorkingLabelsViewModel>
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<WorkingPointsWeight> _workingPointsWeight;

        public WorkingLabelsViewModelFactory(IDataService<Employee> employeeDataService, 
            IDataService<WorkingLabel> workingLabelDataService, 
            IDataService<Group> groupDataService,
            IDataService<WorkingPointsWeight> workingPointsWeight)
        {
            _employeeDataService = employeeDataService;
            _workingLabelDataService = workingLabelDataService;
            _groupDataService = groupDataService;
            _workingPointsWeight = workingPointsWeight;
        }

        public WorkingLabelsViewModel CreateViewModel()
        {
            return new WorkingLabelsViewModel(_employeeDataService, _workingLabelDataService, _groupDataService, _workingPointsWeight);
        }
    }
}
