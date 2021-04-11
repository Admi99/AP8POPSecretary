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

namespace AP8POSecretary.ViewModels
{
    public class WorkingLabelsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<Group> _groupDataService;

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<WorkingLabel> WorkingLabels { get; set; } = new ObservableCollection<WorkingLabel>();
        public IList<WorkingLabel> DeletedWorkingLabels { get; set; }
        public IList<Group> Groups { get; set; }
        public LabelDropHandler LabelDropHandler { get; set; } = new LabelDropHandler();
        public IList<WorkingLabel> JoinedToEmployee { get; set; } = new List<WorkingLabel>();

        public RelayCommand GenerateLabels { get; private set; }
        public RelayCommand SaveEmployees { get; private set; }
        public RelayCommand ReturnLab { get; private set; }
        public RelayCommand ReturnOneLab { get; private set; }
        public RelayCommand UpdateLabel { get; private set; }
        public RelayCommand DeleteSpecificLabel { get; private set; }

        public WorkingLabelsViewModel(IDataService<Employee> employeeDataService,
            IDataService<WorkingLabel> workingLabelDataService,
            IDataService<Group> groupDataService)
        {
            _employeeDataService = employeeDataService;
            _workingLabelDataService = workingLabelDataService;
            _groupDataService = groupDataService;

            GenerateLabels = new RelayCommand(GenerateWorkingLabels);
            SaveEmployees = new RelayCommand(SaveEmployyesAsync);
            ReturnLab = new RelayCommand(ReturnLabels);
            ReturnOneLab = new RelayCommand(ReturnOneLabel);
            UpdateLabel = new RelayCommand(UpdateLabelAsync);
            DeleteSpecificLabel = new RelayCommand(DeleteSpecificLabelAsync);

            DeletedWorkingLabels = new List<WorkingLabel>();

            InitEmployeesAsync();
            InitGroupsAsync();
            InitWorkingLabelsAsync();
        }

        private async void SaveEmployyesAsync(object obj)
        {
            await _employeeDataService.Update(Employees);
            await _workingLabelDataService.Update(WorkingLabels);
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

                await _workingLabelDataService.Update(WorkingLabels);
                JoinedToEmployee.Clear();
            }           
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
                
                    Employees.RemoveAt(index);
                    Employees.Insert(index, employee);

                    label.Employee = null;
                    label.EmployeeId = null;
                    WorkingLabels.Insert(0, label);
                    break;
                }
            }

        }

        public async void UpdateLabelAsync(object obj)
        {
            var id = (int)obj;
            var item = WorkingLabels.Where(item => item.Id == id).First();
            await _workingLabelDataService.Update(id, item);
            DialogHost.CloseDialogCommand.Execute(null, null);               
        }
        public async void DeleteSpecificLabelAsync(object obj)
        {
            var id = (int)obj;
            var item = WorkingLabels.Where(item => item.Id == id).First();
            await _workingLabelDataService.Delete(id);
            WorkingLabels.Remove(item);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private async void InitWorkingLabelsAsync()
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

        private async void InitEmployeesAsync()
        {
            var employees = await _employeeDataService.GetAllEmployees();
            AppendItems(employees);

            LabelDropHandler.Employees = Employees;
            LabelDropHandler.WorkingLabels = WorkingLabels;
            LabelDropHandler.DeletedWorkingLabels = DeletedWorkingLabels;
        }
        private async void InitGroupsAsync()
        {
            var groups = await _groupDataService.GetAllGroups();
            Groups = new List<Group>(groups);
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
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? 1.8 : 2.4;

                    labels.Add(new WorkingLabel()
                    {
                        Name = item.Subject.Name + " - " + "Lecture",
                        StudentsCount = group.StudentsCount,
                        EmploymentPoints = item.Subject.LectureCount * coeficient,
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
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? 1.2 : 1.8;
                    if (item.Subject.PractiseCount != 0)
                    {
                        labels.Add(new WorkingLabel()
                        {
                            Name = item.Subject.Name + " - " + "practise",
                            StudentsCount = numberOfStudentsPerClass,
                            EmploymentPoints = item.Subject.PractiseCount * coeficient,
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
                            EmploymentPoints = item.Subject.PractiseCount * coeficient,
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
                    Language = group.Language,
                    SubjectId = item.SubjectId
                };

                if (item.Subject.CompletionType == CompletionType.CLASSIFIED)
                {
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? 0.3 : 0.3;
                    examPredicate.Name = item.Subject.Name + " - " + "Classified exam";
                    examPredicate.EventType = EventType.CLASSIFIEDCREDIT;
                    examPredicate.EmploymentPoints = 0.3;
                    labels.Add(examPredicate);
                }
                else
                {
                    double coeficient = item.Subject.Language == SubjectLanguage.CZECH ? 0.2 : 0.2;
                    examPredicate.Name = item.Subject.Name + " - " + "Credit";
                    examPredicate.EventType = EventType.CREDIT;
                    examPredicate.EmploymentPoints = coeficient;
                    labels.Add(examPredicate);

                    coeficient = item.Subject.Language == SubjectLanguage.CZECH ? 0.4 : 0.4;
                    WorkingLabel examPredicate2 = new WorkingLabel()
                    {
                        Name = item.Subject.Name + " - " + "Exam",
                        StudentsCount = 24,
                        Language = group.Language,
                        SubjectId = item.SubjectId,
                        EventType = EventType.EXAM,
                        EmploymentPoints = coeficient
                    };
                    labels.Add(examPredicate2);
                }
            }
            return labels;
        }

        private async Task AddWorkingLabels(IList<WorkingLabel> labels)
        {
            await _workingLabelDataService.AddWorkingLabels(labels);
        }

    }
}
