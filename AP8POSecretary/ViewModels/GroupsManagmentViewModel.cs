using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class GroupsManagmentViewModel : BaseViewModel
    {
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public GroupsManagmentViewModel(IDataService<Group> groupDataService, IDataService<Subject> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;

            Groups = new ObservableCollection<Group>()
            {
                new Group()
                {
                    Shortcut="ahoj",
                    Language="Zdar",
                    SemesterType = SemesterType.WINTER,
                    StudentsCount = 10,
                    StudyYear = 20,
                    StudyType = StudyType.DAILY
                },
                new Group()
                {
                    Shortcut="ahoj",
                    Language="Zdar",
                    SemesterType = SemesterType.WINTER,
                    StudentsCount = 10,
                    StudyYear = 20,
                    StudyType = StudyType.DAILY
                },
                   new Group()
                {
                    Shortcut="ahoj",
                    Language="Zdar",
                    SemesterType = SemesterType.WINTER,
                    StudentsCount = 10,
                    StudyYear = 20,
                    StudyType = StudyType.DAILY
                }
            };
        }
    }
}
