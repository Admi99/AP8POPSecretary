using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class EmployeesViewModelFactory : ISecretaryViewModelFactory<EmployeesViewModel>
    {
        private readonly IDataService<Employee> _dataService;
        public EmployeesViewModelFactory(IDataService<Employee> dataService)
        {
            _dataService = dataService;
        }
        public EmployeesViewModel CreateViewModel()
        {
            return new EmployeesViewModel(_dataService);
        }
    }
}
