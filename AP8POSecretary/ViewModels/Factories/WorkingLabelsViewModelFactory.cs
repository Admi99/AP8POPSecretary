using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class WorkingLabelsViewModelFactory : ISecretaryViewModelFactory<WorkingLabelsViewModel>
    {
        private readonly IDataService<Employee> _groupDataService;
        private readonly IDataService<WorkingLabel> _subjectDataService;

        public WorkingLabelsViewModelFactory(IDataService<Employee> groupDataService, IDataService<WorkingLabel> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
        }

        public WorkingLabelsViewModel CreateViewModel()
        {
            return new WorkingLabelsViewModel(_groupDataService, _subjectDataService);
        }
    }
}
