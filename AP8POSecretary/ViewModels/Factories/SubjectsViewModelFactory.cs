using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class SubjectsViewModelFactory : ISecretaryViewModelFactory<SubjectsViewModel>
    {
        private readonly IDataService<Subject> _dataService;

        public SubjectsViewModelFactory(IDataService<Subject> dataService)
        {
            _dataService = dataService;
        }
        public SubjectsViewModel CreateViewModel()
        {
            return new SubjectsViewModel(_dataService);
        }
    }
}
