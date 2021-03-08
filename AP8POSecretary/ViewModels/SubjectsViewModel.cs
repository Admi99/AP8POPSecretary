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
        public SubjectsViewModel(IDataService<Subject> dataService)
        {
            _dataService = dataService;
            AddButtonCommand = new RelayCommand(AddData);
            InitAsync();
        }

        public async void AddData(object obj = null)
        {
            Subject newSubject = new Subject(){
                Name = this.Name,
                Shortcut = this.Shortcut,
                Language = this.Language,
                ClassSize = this.ClassSize,
                CompletionType = CompletionType.CLASSIFIED,
                Credit = this.Credit,
                LectureCount = this.LectureCount,
                SeminareCount = this.SeminareCount,
                PractiseCount = this.PractiseCount,
                WeeksCount = this.WeeksCount,
            };
            Subjects.Add(newSubject);
            await _dataService.Create(newSubject);
        }

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
        public int LectureCount { get; set; } = 2;
        public int SeminareCount { get; set; } = 2;
        public int PractiseCount { get; set; } = 2;
        private int _credit = 2;
        public int Credit
        {
            get { return _credit; }
            set
            {
                _credit = 2;
                OnPropertyChanged(nameof(Credit));
            }
        }
        private int _weeksCount = 2;
        public int WeeksCount
        {
            get { return _weeksCount; }
            set
            {
                _weeksCount = 2;
                OnPropertyChanged(nameof(WeeksCount));
            }
        }
        private int _classSize = 2;
        public int ClassSize
        {
            get { return _classSize; }
            set
            {
                _classSize = 2;
                OnPropertyChanged(nameof(ClassSize));
            }
        }
    }
}
