using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class WorkingLabelsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _groupDataService;
        private readonly IDataService<WorkingLabel> _subjectDataService;

        public WorkingLabelsViewModel(IDataService<Employee> groupDataService, IDataService<WorkingLabel> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
        }
    }
}
