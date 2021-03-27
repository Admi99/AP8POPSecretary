using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.ViewModels.DropHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.ViewModels
{
    public class WorkingLabelsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<Group> _groupDataService;

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<WorkingLabel> WorkingLabels { get; set; } = new ObservableCollection<WorkingLabel>();
        public IList<Group> Groups { get; set; }
        public LabelDropHandler LabelDropHandler { get; set; } = new LabelDropHandler();
        public WorkingLabelsViewModel(IDataService<Employee> employeeDataService,
            IDataService<WorkingLabel> workingLabelDataService,
            IDataService<Group> groupDataService)
        {
            _employeeDataService = employeeDataService;
            _workingLabelDataService = workingLabelDataService;
            _groupDataService = groupDataService;

            InitEmployeesAsync();
            InitGroupsAsync();
        }
        private async void InitEmployeesAsync()
        {
            var employees = await _employeeDataService.GetAllEmployees();
            AppendItems(employees);

            LabelDropHandler.Employees = Employees;
        }
        private async void InitGroupsAsync()
        {
            var groups = await _groupDataService.GetAllGroups();
            Groups = new List<Group>(groups);
            GenerateWorkingLabels();
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
        public async void GenerateWorkingLabels()
        {
            if(Groups != null)
            {
                foreach (var item in Groups)
                {
                    var generatedLabelsFromGroup = GenerateLectures(item);
                    await AddWorkingLabels(generatedLabelsFromGroup);
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

                labels.Add(new WorkingLabel()
                {
                    Name = item.Subject.Name + " - " + group.Shortcut,
                    StudentsCount = group.StudentsCount,
                    EmploymentPoints = item.Subject.LectureCount * 1.8,
                    Language = group.Language,
                    WeekCount = item.Subject.WeeksCount,
                    HoursCount = item.Subject.LectureCount,
                    EventType = EventType.LECTURE,
                    //Subject = item.Subject,
                    SubjectId = item.SubjectId
                });

                for (int i = 0; i < (int)labelsCount; i++)
                {
                    labels.Add(new WorkingLabel()
                    {
                        Name = i.ToString() + "." + item.Subject.Name,
                        StudentsCount = numberOfStudentsPerClass,
                        EmploymentPoints = item.Subject.PractiseCount * 1.2,
                        Language = group.Language,
                        WeekCount = item.Subject.WeeksCount,
                        HoursCount = item.Subject.LectureCount,
                        EventType = EventType.PRACTISE,
                        //Subject = item.Subject,
                        SubjectId = item.SubjectId
                    });
                }

                WorkingLabel examPredicate = new WorkingLabel()
                {
                    StudentsCount = 12,
                    Language = group.Language,
                    //Subject = item.Subject,
                    SubjectId = item.SubjectId
                };

                if(item.Subject.CompletionType == CompletionType.CLASSIFIED)
                {
                    examPredicate.EventType = EventType.CLASSIFIEDCREDIT;
                    examPredicate.EmploymentPoints = 0.3;
                    labels.Add(examPredicate);
                }
                else
                {
                    examPredicate.EventType = EventType.CREDIT;
                    examPredicate.EmploymentPoints = 0.2;
                    labels.Add(examPredicate);

                    examPredicate.EventType = EventType.EXAM;
                    examPredicate.EmploymentPoints = 0.4;
                    labels.Add(examPredicate);

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
