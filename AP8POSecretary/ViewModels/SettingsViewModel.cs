using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AP8POSecretary.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<WorkingPointsWeight> _workingPointsWeight;

        public IList<WorkingPointsWeight> WorkingPointsWeights { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        

        public RelayCommand TransformToXml { get; private set; }
        public RelayCommand UpdatePoints { get; private set; }

        public SettingsViewModel(IDataService<Employee> employeeDataService
            , IDataService<Group> groupDataService
            , IDataService<Subject> subjectDataService
            , IDataService<WorkingLabel> workingLabelDataService
            , IDataService<WorkingPointsWeight> workingPointsWeight)
        {
            _employeeDataService = employeeDataService;
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
            _workingLabelDataService = workingLabelDataService;
            _workingPointsWeight = workingPointsWeight;

            TransformToXml = new RelayCommand(GenerateXml);
            UpdatePoints = new RelayCommand(UpdatePointsHandler);

            GetEmployees();
            GetWorkingPointsWeights();
           
        }

        private async void GetEmployees()
        {
            Employees = await _employeeDataService.GetAll();
        }

        private async void GetWorkingPointsWeights()
        {
            var points = await _workingPointsWeight.GetAll();
            WorkingPointsWeights = points.ToList();

            LectureType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Lecture).Value;

        }

        private void GenerateXml(object obj)
        {
            DataContractSerializer xs = new DataContractSerializer(typeof(List<Employee>));
            TextWriter filestream = new StreamWriter(@"C:\School\haha.xml");

            using (var xmlWriter = XmlWriter.Create(filestream, new XmlWriterSettings { Indent = true }))
            {
                xs.WriteObject(xmlWriter, Employees);
            }

            filestream.Close();

        }

        private void UpdatePointsHandler(object obj)
        {
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Lecture).Value = LectureType;

            _workingPointsWeight.Update(WorkingPointsWeights);

        }

        private double _lectureType;
        public double LectureType
        {
            get { return _lectureType; }
            set
            {
                _lectureType = value;
                OnPropertyChanged(nameof(LectureType));
            }
        }

    }
}
