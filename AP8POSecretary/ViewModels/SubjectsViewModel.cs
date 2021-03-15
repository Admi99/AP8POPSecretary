using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class SubjectsViewModel : BaseViewModel
    {
        private readonly IDataService<Subject> _dataService;
        private ObservableCollection<Subject> _subjects;
        public ObservableCollection<Subject> Subjects
        {
            get
            {
                return _subjects;
            }
            set
            {
                _subjects = value;
                OnPropertyChanged(nameof(Subjects));
            }
        }

        public RelayCommand AddButtonCommand { get; private set; }
        public RelayCommand ModifySubjectsCommand { get; private set; }
        public RelayCommand DeleteSubjectsCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public SubjectsViewModel(IDataService<Subject> dataService)
        {
            _dataService = dataService;
            AddButtonCommand = new RelayCommand(AddData, CheckDataBeforeAdding);
            ModifySubjectsCommand = new RelayCommand(ModifyAllData);
            DeleteSubjectsCommand = new RelayCommand(DeleteAllData);
            DeleteCommand = new RelayCommand(DeleteData);

            InitAsync();
        }

        public IEnumerable<string> CompletationTypes => new[] { CompletionType.EXAM.ToString(), CompletionType.CLASSIFIED.ToString()};
        public IEnumerable<string> ClassSizes => new[] { "12", "24", "48", "96", "192", "384" };

        public async void AddData(object obj = null)
        {

            Subject newSubject = new Subject(){
                Name = this.Name,
                Shortcut = this.Shortcut,
                Language = this.Language,
                ClassSize = this.ClassSize,
                CompletionType = this.Completion,
                Credit = this.Credit,
                LectureCount = this.LectureCount,
                SeminareCount = this.SeminareCount,
                PractiseCount = this.PractiseCount,
                WeeksCount = this.WeeksCount,
            };
            Subjects.Add(newSubject);
            await _dataService.Create(newSubject);
        }

        public async void ModifyAllData(object obj = null)
        {
            IsSaved = true;
            foreach (var item in Subjects)
            {
                await _dataService.Update(item.Id, item);
            }
            IsSaved = false;
        }

        public async void DeleteAllData(object obj = null)
        {
            IsDeleted = true;
            foreach (var item in Subjects)
            {
                await _dataService.Delete(item.Id);
            }
            Subjects.Clear();
            IsDeleted = false;
        }
        public async void DeleteData(object obj)
        {
            if(obj != null)
            {
                IsDeleted = true;
                await _dataService.Delete((obj as Subject).Id);
                Subjects.Remove(obj as Subject);
                IsDeleted = false;
            }   
        }
        public bool CheckDataBeforeAdding(object obj = null)
            => !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Language) && !String.IsNullOrEmpty(Shortcut);
            

        private async void InitAsync()
        {
            var subjects = await _dataService.GetAll();
            Subjects = new ObservableCollection<Subject>(subjects);
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
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

        private int _lectureCount = 2;
        public int LectureCount 
        { 
            get { return _lectureCount; }
            set 
            { 
                _lectureCount = value;
                OnPropertyChanged(nameof(LectureCount)); 
            }
        }

        private int _seminareCount = 2;
        public int SeminareCount
        {
            get { return _seminareCount; }
            set 
            
            {
                _seminareCount = value;
                OnPropertyChanged(nameof(SeminareCount)); 
            }
        }
        private int _practiseCount = 2;
        public int PractiseCount
        {
            get { return _practiseCount; }
            set 
            {
                _practiseCount = value;
                OnPropertyChanged(nameof(PractiseCount)); 
            }
        }

        private int _credit = 5;
        public int Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChanged(nameof(Credit));
            }
        }
        private int _weeksCount = 13;
        public int WeeksCount
        {
            get { return _weeksCount; }
            set
            {
                _weeksCount = value;
                OnPropertyChanged(nameof(WeeksCount));
            }
        }
        private int _classSize = 12;
        public int ClassSize
        {
            get { return _classSize; }
            set
            {
                _classSize = value;
                OnPropertyChanged(nameof(ClassSize));
            }
        }
        private CompletionType _completion = CompletionType.EXAM;
        public CompletionType Completion
        {
            get { return _completion; }
            set
            {
                _completion = value;
                OnPropertyChanged(nameof(Completion));
            }
        }
    }
}
