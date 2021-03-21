using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.ViewModels.DropHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class GroupsManagmentViewModel : BaseViewModel
    {
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();

        public CardDropHandler CardDropHandler { get; set; } = new CardDropHandler();
        public RelayCommand SaveSubjects { get; private set; }
        public RelayCommand DeleteSubjects { get; private set; }
        public RelayCommand RevertSubjects { get; private set; }
        public GroupsManagmentViewModel(IDataService<Group> groupDataService, IDataService<Subject> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;

            SaveSubjects = new RelayCommand(SaveSubjectsAsync);

            InitGroupsAsync();
            InitSubjectsAsync();      

        }

        private async void SaveSubjectsAsync(object obj)
        {
            foreach (var item in Groups)
            {
                await _groupDataService.Update(item.Id, item);
            }
        }

        private async void DeleteSubjectsAsync(object obj)
        {
            foreach (var item in Subjects)
            {
                
            }
        }

        private async void InitGroupsAsync()
        {
            var groups = await _groupDataService.GetAllGroups();
            AppendItems(groups);
 
            CardDropHandler.Groups = Groups;
        }

        private async void InitSubjectsAsync()
        {
            var subjects = await _subjectDataService.GetAll();
            AppendItems(subjects);
        }

        private void AppendItems<T>(IEnumerable<T> items)
        {
            if(typeof(T).Name is "Group")
            {
                foreach (var item in items)
                    Groups.Add(item as Group);
            }
            else
            {
                foreach (var item in items)
                    Subjects.Add(item as Subject);
            }               
        }
    }
}
