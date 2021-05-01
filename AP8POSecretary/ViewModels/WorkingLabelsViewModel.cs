using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.ViewModels.DropHandlers;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ToastNotifications.Messages;

namespace AP8POSecretary.ViewModels
{
    public class WorkingLabelsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<WorkingPointsWeight> _workingPointsWeight;
        private readonly IDataService<Subject> _subjectDataService;

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<WorkingLabel> WorkingLabels { get; set; } = new ObservableCollection<WorkingLabel>();
        public IList<WorkingLabel> DeletedWorkingLabels { get; set; }
        public IList<Group> Groups { get; set; }
        public LabelDropHandler LabelDropHandler { get; set; } = new LabelDropHandler();
        public IList<WorkingLabel> JoinedToEmployee { get; set; } = new List<WorkingLabel>();
        public IList<WorkingPointsWeight> WorkingPointsWeights { get; set; } = new List<WorkingPointsWeight>();
        public IList<Subject> Subjects { get; set; }
        public Subject TempSubject { get; set; }

        public RelayCommand GenerateLabels { get; private set; }
        public RelayCommand SaveEmployees { get; private set; }
        public RelayCommand ReturnLab { get; private set; }
        public RelayCommand ReturnOneLab { get; private set; }
        public RelayCommand UpdateLabel { get; private set; }
        public RelayCommand AddLabel { get; private set; }
        public RelayCommand DeleteSpecificLabel { get; private set; }
        public RelayCommand DeleteAllLabels { get; private set; }
        public RelayCommand RegenerateLabels { get; private set; }
        public RelayCommand OnSubjectSelectionChanged { get; private set; }
        public RelayCommand OnSubjectChangeForLabels { get; set; }

        public WorkingLabelsViewModel(IDataService<Employee> employeeDataService,
            IDataService<WorkingLabel> workingLabelDataService,
            IDataService<Group> groupDataService,
            IDataService<WorkingPointsWeight> workingPointsWeight,
            IDataService<Subject> subjectDataService
            )
        {
            _employeeDataService = employeeDataService;
            _workingLabelDataService = workingLabelDataService;
            _groupDataService = groupDataService;
            _workingPointsWeight = workingPointsWeight;
            _subjectDataService = subjectDataService;

            GenerateLabels = new RelayCommand(GenerateWorkingLabels);
            SaveEmployees = new RelayCommand(SaveEmployyesAsync);
            ReturnLab = new RelayCommand(ReturnLabels);
            ReturnOneLab = new RelayCommand(ReturnOneLabel);
            UpdateLabel = new RelayCommand(UpdateLabelAsync);
            AddLabel = new RelayCommand(AddLabelAsync);
            DeleteSpecificLabel = new RelayCommand(DeleteSpecificLabelAsync);
            DeleteAllLabels = new RelayCommand(DeleteAllLabelsAsync);
            RegenerateLabels = new RelayCommand(RegenerateWorkingLabels);
            OnSubjectSelectionChanged = new RelayCommand(OnSubjSelectionChanged);
            OnSubjectChangeForLabels = new RelayCommand(ChangeSubjectForLabel);


            DeletedWorkingLabels = new List<WorkingLabel>();

            InitEmployeesAsync();
            InitGroupsAsync();
            InitWorkingLabelsAsync();
            InitWorkingPointsWeight();
            InitSubjectAsync();
        }

        private async void InitWorkingPointsWeight()
        {
            try
            {
                var items = await _workingPointsWeight.GetAll();
                WorkingPointsWeights = items.ToList();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
        }

        private async void SaveEmployyesAsync(object obj)
        {
            try
            {
                await _employeeDataService.Update(Employees);
                await _workingLabelDataService.Update(WorkingLabels);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Saving failed with error: " + ex);
            }
            Notifier.ShowSuccess("Data were saved succesfully");
        }
        private async void ReturnLabels(object obj = null)
        {
            foreach (var item in Employees)
            {
                if (item.WorkingLabels == null)
                    continue;
                foreach (var innerItem in item.WorkingLabels)
                {
                    JoinedToEmployee.Add(innerItem);
                }
                item.WorkingLabels = null;
            }

            if(JoinedToEmployee.Count != 0)
            {
                IList<Employee> employees = new List<Employee>(Employees);
                Employees.Clear();
                AppendItems(employees);
                PrependItems(JoinedToEmployee);

                Parallel.ForEach(WorkingLabels, item =>
                {
                    item.EmployeeId = null;
                    item.Employee = null;
                });

                try
                {
                    await _workingLabelDataService.Update(WorkingLabels);
                    JoinedToEmployee.Clear();
                }
                catch(Exception ex)
                {
                    Notifier.ShowError("Failed to return labels with error: " + ex);
                }
            }

            Notifier.ShowSuccess("Working labels were returned succesfully");
        }

        private void ReturnOneLabel(object obj)
        {
            foreach (var item in Employees)
            {
                var label = item.WorkingLabels.Where(item => item.Id == (int)obj).FirstOrDefault();
                if(label != null)
                {
                    item.WorkingLabels.Remove(label);

                    var index = Employees.IndexOf(item);
                    var employee = Employees.ElementAt(index);
                    
                    if(label.Language == SubjectLanguage.CZECH.ToString())
                    {
                        item.WorkingPoints -= label.EmploymentPoints;
                    }
                    item.WorkingPointsWithEng -= label.EmploymentPoints;

                    Employees.RemoveAt(index);
                    Employees.Insert(index, employee);

                    label.Employee = null;
                    label.EmployeeId = null;
                    WorkingLabels.Insert(0, label);
                    break;
                }
            }
            Notifier.ShowInformation("Label was returned, but no saved");

        }

        public async void UpdateLabelAsync(object obj)
        {
            try
            {
                var id = (int)obj;
                var item = WorkingLabels.Where(item => item.Id == id).First();
                await _workingLabelDataService.Update(id, item);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to update a data in database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were updated successfuly ");
        }
        private async void AddLabelAsync(object obj)
        {
            var newWorkingLabel = new WorkingLabel()
            {
                Name = this.Name,
                Language = this.Language,
                HoursCount = this.HoursCount,
                EventType = this.EventType,
                EmploymentPoints = this.EmploymentPoints,
                StudentsCount = this.StudentsCount,
                WeekCount = this.WeekCount
            };
            try
            {
                await _workingLabelDataService.Create(newWorkingLabel);

                WorkingLabels.Insert(0, newWorkingLabel);

                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to add a data to database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were added successfuly ");
        }

        public async void DeleteSpecificLabelAsync(object obj)
        {
            try
            {
                var id = (int)obj;
                var item = WorkingLabels.Where(item => item.Id == id).First();
                await _workingLabelDataService.Delete(id);
                WorkingLabels.Remove(item);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to delete a data from database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were deleted successfuly ");
        }

        private async void InitWorkingLabelsAsync()
        {
            try
            {
                var workingLabels = await _workingLabelDataService.GetAllWorkingLabels();
                IList<WorkingLabel> onlyNotJoinedToEmployee = new List<WorkingLabel>();
                foreach (var item in workingLabels)
                {
                    if (item.EmployeeId == null)
                        onlyNotJoinedToEmployee.Add(item);
                }
                AppendItems(onlyNotJoinedToEmployee);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
        }

        private async void InitEmployeesAsync()
        {
            try
            {
                var employees = await _employeeDataService.GetAllEmployees();
                AppendItems(employees);

                LabelDropHandler.Employees = Employees;
                LabelDropHandler.WorkingLabels = WorkingLabels;
                LabelDropHandler.DeletedWorkingLabels = DeletedWorkingLabels;
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
        }
        private async void InitGroupsAsync()
        {
            try
            {
                var groups = await _groupDataService.GetAllGroups();
                Groups = new List<Group>(groups);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }           
        }

        private async void InitSubjectAsync()
        {
            try
            {
                var subjects = await _subjectDataService.GetAll();
                Subjects = new List<Subject>(subjects);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
            
        }

        private async void DeleteAllLabelsAsync(object obj)
        {
            try
            {
                await _workingLabelDataService.DeleteAll(WorkingLabels);
                WorkingLabels.Clear();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to delete a data from database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were deleted successfuly ");
        }

        private void AppendItems<T>(IEnumerable<T> items)
        {
            if (typeof(T).Name is "Employee")
            {
                foreach (var item in items)
                    Employees.Add(item as Employee);
            }
            else
            {
                foreach (var item in items)
                    WorkingLabels.Add(item as WorkingLabel);
            }
        }
        private void PrependItems<T>(IEnumerable<T> items)
        {
            if (typeof(T).Name is "Employee")
            {
                foreach (var item in items)
                    Employees.Insert(0, item as Employee);
            }
            else
            {
                foreach (var item in items)
                    WorkingLabels.Insert(0, item as WorkingLabel);
            }
        }

        public async void GenerateWorkingLabels(object obj)
        {
            try
            {
                if (Groups != null)
                {
                    foreach (var item in Groups)
                    {
                        var generatedLabelsFromGroup = GenerateLectures(item);
                        await AddWorkingLabels(generatedLabelsFromGroup);
                        AppendItems(generatedLabelsFromGroup);
                    }
                }
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to generate labels with error: " + ex);
            }
            Notifier.ShowSuccess("Labels were generated succesfully");
        }

        public void OnSubjSelectionChanged(object obj)
        {
            if(obj != null)
            {
                TempSubject = (Subject)obj;
            }          
        }

        public async void ChangeSubjectForLabel(object obj)
        {
            if(obj != null)
            {
                int workingLabelId = (int)obj;
                var workingLabel = WorkingLabels.Where(u => u.Id == workingLabelId).First();
                var index = WorkingLabels.IndexOf(workingLabel);

                workingLabel.SubjectId = TempSubject.Id;
                workingLabel.Subject = TempSubject;

                WorkingLabels.RemoveAt(index);
                WorkingLabels.Insert(index, workingLabel);
                try
                {
                    await _workingLabelDataService.Update(workingLabel.Id, workingLabel);
                }
                catch(Exception ex)
                {
                    Notifier.ShowError("Failed to update a data in database with error: " + ex);
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            Notifier.ShowSuccess("Data were updated successfuly ");
        }

        private IList<WorkingLabel> GenerateLectures(Group group)
        {
            double students = group.StudentsCount;
            IList<WorkingLabel> labels = new List<WorkingLabel>();

            foreach (var item in group.GroupSubjects)
            {
                double classSize = item.Subject.ClassSize;
                double labelsCount = Math.Ceiling(students / classSize);
                int numberOfStudentsPerClass = Convert.ToInt32(Math.Ceiling(students / labelsCount));


                // Lecture
                if (item.Subject.CompletionType == CompletionType.EXAM)
                {
                    var lectureConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Lecture).Value;
                    var lectureEngConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.LectureEng).Value;
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? lectureConf : lectureEngConf;

                    labels.Add(new WorkingLabel()
                    {
                        Name = item.Subject.Name + " - " + "Lecture",
                        StudentsCount = group.StudentsCount,
                        EmploymentPoints = Math.Round(item.Subject.LectureCount * coeficient, 2),
                        Language = item.Subject.Language.ToString(),
                        WeekCount = item.Subject.WeeksCount,
                        HoursCount = item.Subject.LectureCount,
                        EventType = EventType.LECTURE,
                        SubjectId = item.SubjectId
                    });
                }

                // Practise and seminare

                for (int i = 0; i < (int)labelsCount; i++)
                {
                    var lectureConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Practise).Value;
                    var lectureEngConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.PractiseEng).Value;
                    double coeficientPractise = item.Subject.Language == SubjectLanguage.CZECH ? lectureConf : lectureEngConf;

                    var seminareConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Seminare).Value;
                    var seminareEngConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.SeminareEng).Value;
                    double coeficienSeminare = item.Subject.Language == SubjectLanguage.CZECH ? seminareConf : seminareEngConf;

                    if (item.Subject.PractiseCount != 0)
                    {
                        labels.Add(new WorkingLabel()
                        {
                            Name = item.Subject.Name + " - " + "practise",
                            StudentsCount = numberOfStudentsPerClass,
                            EmploymentPoints = Math.Round(item.Subject.PractiseCount * coeficientPractise, 2),
                            Language = item.Subject.Language.ToString(),
                            WeekCount = item.Subject.WeeksCount,
                            HoursCount = item.Subject.LectureCount,
                            EventType = EventType.PRACTISE,
                            SubjectId = item.SubjectId
                        });
                    }
                    if(item.Subject.SeminareCount != 0)
                    {
                        labels.Add(new WorkingLabel()
                        {
                            Name = item.Subject.Name + " - " + "seminare",
                            StudentsCount = numberOfStudentsPerClass,
                            EmploymentPoints = Math.Round(item.Subject.SeminareCount * coeficienSeminare, 2),
                            Language = item.Subject.Language.ToString(),
                            WeekCount = item.Subject.WeeksCount,
                            HoursCount = item.Subject.LectureCount,
                            EventType = EventType.PRACTISE,
                            SubjectId = item.SubjectId
                        });
                    }
                }

                WorkingLabel examPredicate = new WorkingLabel()
                {
                    StudentsCount = 12,
                    Language = item.Subject.Language.ToString(),
                    SubjectId = item.SubjectId
                };

                if (item.Subject.CompletionType == CompletionType.CLASSIFIED)
                {
                    var classifiedConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ClassifiedCredit).Value;
                    var classifiedEngConf = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ClassifiedCreditEng).Value;
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? classifiedConf : classifiedEngConf;
                    examPredicate.Name = item.Subject.Name + " - " + "Classified exam";
                    examPredicate.EventType = EventType.CLASSIFIEDCREDIT;
                    examPredicate.EmploymentPoints = coeficient;
                    labels.Add(examPredicate);
                }
                else
                {
                    var credit = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Credit).Value;
                    var creditEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.CreditEng).Value;
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? credit : creditEng;
                    examPredicate.Name = item.Subject.Name + " - " + "Credit";
                    examPredicate.EventType = EventType.CREDIT;
                    examPredicate.EmploymentPoints = coeficient;
                    labels.Add(examPredicate);

                    var exam = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Exam).Value;
                    var examEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ExamEng).Value;
                    coeficient = item.Subject.Language == SubjectLanguage.CZECH ? exam : examEng;
                    WorkingLabel examPredicate2 = new WorkingLabel()
                    {
                        Name = item.Subject.Name + " - " + "Exam",
                        StudentsCount = 12,
                        Language = item.Subject.Language.ToString(),
                        SubjectId = item.SubjectId,
                        EventType = EventType.EXAM,
                        EmploymentPoints = coeficient
                    };
                    labels.Add(examPredicate2);
                }
            }
            return labels;
        }

        public async void RegenerateWorkingLabels(object obj)
        {
            DeleteAllLabelsAsync(null);
            List<WorkingLabel> deleteAllAsignedLabels = new List<WorkingLabel>();
            foreach (var item in Employees)
            {
                deleteAllAsignedLabels.AddRange(item.WorkingLabels);
                item.WorkingLabels = null;
                item.WorkingPoints = 0;
                item.WorkingPointsWithEng = 0;
            }

            await _workingLabelDataService.DeleteAll(deleteAllAsignedLabels);

            GenerateWorkingLabels(null);

            var employees = new List<Employee>(Employees);
            Employees.Clear();
            AppendItems(employees);
        }

        private async Task AddWorkingLabels(IList<WorkingLabel> labels)
        {
            await _workingLabelDataService.AddWorkingLabels(labels);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value.ToString();
                OnPropertyChanged(nameof(Language));
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
        private double _employmentPoints;
        public double EmploymentPoints
        {
            get { return _employmentPoints; }
            set
            {
                _employmentPoints = value;
                OnPropertyChanged(nameof(EmploymentPoints));
            }
        }
        private EventType _eventType;
        public EventType EventType
        {
            get { return _eventType; }
            set
            {
                _eventType = value;
                OnPropertyChanged(nameof(EventType));
            }
        }
        private int _hoursCount;
        public int HoursCount
        {
            get { return _hoursCount; }
            set
            {
                _hoursCount = value;
                OnPropertyChanged(nameof(HoursCount));
            }
        }
        private int _weekCount;
        public int WeekCount
        {
            get { return _weekCount; }
            set
            {
                _weekCount = value;
                OnPropertyChanged(nameof(WeekCount));
            }
        }

    }

}
