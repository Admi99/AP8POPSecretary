using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _dataService;

        public EmployeesViewModel(IDataService<Employee> dataService)
        {
            _dataService = dataService;
        }
    }
}
