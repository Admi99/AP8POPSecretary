using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class GroupsManagmentViewModel : BaseViewModel
    {
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;

        public GroupsManagmentViewModel(IDataService<Group> groupDataService, IDataService<Subject> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
        }
    }
}
