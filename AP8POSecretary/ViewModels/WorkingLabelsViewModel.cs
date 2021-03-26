using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.ViewModels.DropHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class WorkingLabelsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _groupDataService;
        private readonly IDataService<WorkingLabel> _subjectDataService;

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<WorkingLabel> WorkingLabels { get; set; } = new ObservableCollection<WorkingLabel>();
        public LabelDropHandler LabelDropHandler { get; set; } = new LabelDropHandler();
        public WorkingLabelsViewModel(IDataService<Employee> groupDataService, IDataService<WorkingLabel> subjectDataService)
        {
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;

            InitEmployeesAsync();
        }
        private async void InitEmployeesAsync()
        {
            var employees = await _groupDataService.GetAllEmployees();
            AppendItems(employees);

            /*CardDropHandler.Groups = Groups;
            CardDropHandler.GroupSubjectsUpdated = GroupSubjectUpdated;*/
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


    }
}
