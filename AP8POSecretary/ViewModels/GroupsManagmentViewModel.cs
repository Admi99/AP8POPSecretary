using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.ViewModels.DropHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ToastNotifications.Messages;

namespace AP8POSecretary.ViewModels
{
    public class GroupsManagmentViewModel : BaseViewModel
    {
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();
        public IList<GroupSubject> GroupSubjectUpdated { get; set; }

        public CardDropHandler CardDropHandler { get; set; } = new CardDropHandler();
        public RelayCommand SaveSubjects { get; private set; }
        public RelayCommand DeleteGroupSubjects { get; private set; }
        public RelayCommand RevertSubjects { get; private set; }
        public GroupsManagmentViewModel(IDataService<Group> groupDataService, IDataService<Subject> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;

            GroupSubjectUpdated = new List<GroupSubject>();

            SaveSubjects = new RelayCommand(SaveSubjectsAsync);
            DeleteGroupSubjects = new RelayCommand(DeleteSubjectsAsync);
            RevertSubjects = new RelayCommand(RevertSubjectsAsync);

            InitGroupsAsync();
            InitSubjectsAsync();      

        }

        private async void SaveSubjectsAsync(object obj)
        {
            try
            {
                await _groupDataService.AddRange(GroupSubjectUpdated);
                GroupSubjectUpdated.Clear();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Saving failed with error: " + ex);
            }
            Notifier.ShowSuccess("Data were saved succesfully");
        }

        private async void DeleteSubjectsAsync(object obj)
        {
            try
            {
                await _groupDataService.DeleteAllGroupSubject();
                Groups.Clear();
                GroupSubjectUpdated.Clear();
                InitGroupsAsync();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to delete a data from database with error: " + ex);
            }
            Notifier.ShowSuccess("Subject connection was deleted succesfully");
        }

        private void RevertSubjectsAsync(object obj)
        {
            try
            {
                Groups.Clear();
                GroupSubjectUpdated.Clear();
                InitGroupsAsync();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to revert and load data from database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were reverted succesfully");
        }

        private async void InitGroupsAsync()
        {
            try
            {
                var groups = await _groupDataService.GetAllGroups();
                AppendItems(groups);

                CardDropHandler.Groups = Groups;
                CardDropHandler.GroupSubjectsUpdated = GroupSubjectUpdated;
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
        }

        private async void InitSubjectsAsync()
        {
            try
            {
                var subjects = await _subjectDataService.GetAll();
                AppendItems(subjects);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
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
