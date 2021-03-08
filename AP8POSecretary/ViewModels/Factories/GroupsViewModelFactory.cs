using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class GroupsViewModelFactory : ISecretaryViewModelFactory<GroupsViewModel>
    {
        private readonly IDataService<Group> _dataService;
        public GroupsViewModelFactory(IDataService<Group> dataService)
        {
            _dataService = dataService;
        }

        public GroupsViewModel CreateViewModel()
        {
            return new GroupsViewModel(_dataService);
        }
    }
}
