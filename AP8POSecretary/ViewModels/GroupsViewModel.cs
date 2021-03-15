using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    { 
        private readonly IDataService<Group> _dataService;

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
        public RelayCommand AddButtonCommand { get; private set; }
        public RelayCommand ModifySubjectsCommand { get; private set; }
        public RelayCommand DeleteSubjectsCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public IEnumerable<string> StudyTypes => new[] { StudyType.DAILY.ToString(), StudyType.DISTANCE.ToString() };
        public IEnumerable<string> SemesterTypes => new[] { SemesterType.SPRING.ToString(), SemesterType.WINTER.ToString() };

        public GroupsViewModel(IDataService<Group> dataService)
        {
            _dataService = dataService;
            AddButtonCommand = new RelayCommand(AddData, CheckDataBeforeAdding);
            ModifySubjectsCommand = new RelayCommand(ModifyAllData);
            DeleteSubjectsCommand = new RelayCommand(DeleteAllData);
            DeleteCommand = new RelayCommand(DeleteData);

            InitAsync();
        }

        private async void DeleteData(object obj)
        {
            if (obj != null)
            {
                IsDeleted = true;
                await _dataService.Delete((obj as Group).Id);
                Groups.Remove(obj as Group);
                IsDeleted = false;
            }
        }

        private async void DeleteAllData(object obj)
        {
            IsDeleted = true;
            foreach (var item in Groups)
            {
                await _dataService.Delete(item.Id);
            }
           Groups.Clear();
            IsDeleted = false;
        }

        private async void ModifyAllData(object obj)
        {
            IsSaved = true;
            foreach (var item in Groups)
            {
                await _dataService.Update(item.Id, item);
            }
            IsSaved = false;
        }

        public bool CheckDataBeforeAdding(object obj = null)
            => !String.IsNullOrEmpty(Shortcut) && !String.IsNullOrEmpty(Language);

        private async void AddData(object obj = null)
        {
            Group newGroup = new Group()
            {
                Shortcut = this.Shortcut,
                Language = this.Language,
                StudyYear = this.StudyYear,
                StudentsCount = this.StudentsCount,
                StudyType = this.StudyType,
                SemesterType = this.SemesterType                
            };
            Groups.Add(newGroup);
            await _dataService.Create(newGroup);
        }

        private async void InitAsync()
        {
            var groups = await _dataService.GetAll();
            Groups = new ObservableCollection<Group>(groups);
        }

        private string _shortcut;
        public string Shortcut
        {
            get { return _shortcut; }
            set
            {
                _shortcut = value;
                OnPropertyChanged(nameof(Shortcut));
            }
        }
        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private int _studyYear;
        public int StudyYear {
            get { return _studyYear; }
            set {
                _studyYear = value;
                OnPropertyChanged(nameof(StudyYear));
            }
        }

        private int _studentsCount;
        public int StudentsCount
        {
            get { return _studentsCount; }
            set
            {
                _studentsCount = value;
                OnPropertyChanged(nameof(StudentsCount));
            }
        }

        private StudyType _studyType;
        public StudyType StudyType 
        {
            get { return _studyType; }
            set
            {
                _studyType = value;
                OnPropertyChanged(nameof(StudyType));
            }
        }
        private SemesterType _semesterType;
        public SemesterType SemesterType
        {
            get { return _semesterType; }
            set
            {
                _semesterType = value;
                OnPropertyChanged(nameof(SemesterType));
            }
        }


    }
}
