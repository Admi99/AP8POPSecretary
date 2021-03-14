using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels.Factories
{
    public class GroupsManagmentViewModelFactory : ISecretaryViewModelFactory<GroupsManagmentViewModel>
    {
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;

        public GroupsManagmentViewModelFactory(IDataService<Group> groupDataService, IDataService<Subject> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
        }

        public GroupsManagmentViewModel CreateViewModel()
        {
            return new GroupsManagmentViewModel(_groupDataService, _subjectDataService);
        }
    }
}
