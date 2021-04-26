using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;


namespace AP8POSecretary.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _employeeDataService;
        private readonly IDataService<Group> _groupDataService;
        private readonly IDataService<Subject> _subjectDataService;
        private readonly IDataService<WorkingLabel> _workingLabelDataService;
        private readonly IDataService<WorkingPointsWeight> _workingPointsWeight;
        private readonly IDataService<GroupSubject> _groupSubjectDataService;

        public EntitiesWrapper EntitiesWrapper { get; set; } = new EntitiesWrapper();
        public IList<WorkingPointsWeight> WorkingPointsWeights { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<WorkingLabel> WorkingLabels { get; set; }
        public IEnumerable<GroupSubject> GroupSubjects { get; set; }
        public IEnumerable<WorkingPointsWeight> WorkingPointsWeightsXML { get; set; }


        public RelayCommand TransformToXml { get; private set; }
        public RelayCommand UpdatePoints { get; private set; }
        public RelayCommand DeserializeXml { get; private set; }

        public Notifier Notifier { get; set; }

        public SettingsViewModel(IDataService<Employee> employeeDataService
            , IDataService<Group> groupDataService
            , IDataService<Subject> subjectDataService
            , IDataService<WorkingLabel> workingLabelDataService
            , IDataService<WorkingPointsWeight> workingPointsWeight
            , IDataService<GroupSubject> groupSubjectDataService)
        {
            _employeeDataService = employeeDataService;
            _groupDataService = groupDataService;
            _subjectDataService = subjectDataService;
            _workingLabelDataService = workingLabelDataService;
            _workingPointsWeight = workingPointsWeight;
            _groupSubjectDataService = groupSubjectDataService;

            TransformToXml = new RelayCommand(GenerateXml);
            UpdatePoints = new RelayCommand(UpdatePointsHandler);
            DeserializeXml = new RelayCommand(ImportFromXml);

            Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });

            GetEntities();
            GetWorkingPointsWeights();
           
        }

        private async void GetEntities()
        {
            Employees = await _employeeDataService.GetAll();
            Subjects = await _subjectDataService.GetAll();
            Groups = await _groupDataService.GetAll();
            WorkingLabels = await _workingLabelDataService.GetAll();
            WorkingPointsWeightsXML = await _workingPointsWeight.GetAll();
            GroupSubjects = await _groupSubjectDataService.GetAll();


            EntitiesWrapper.Subjects = Subjects;
            EntitiesWrapper.Employees = Employees;
            EntitiesWrapper.Groups = Groups;
            EntitiesWrapper.GroupSubjects = GroupSubjects;
            EntitiesWrapper.WorkingLabels = WorkingLabels;
            EntitiesWrapper.WorkingPointsWeights = WorkingPointsWeightsXML;
        }

        private async void GetWorkingPointsWeights()
        {
            var points = await _workingPointsWeight.GetAll();
            WorkingPointsWeights = points.ToList();

            LectureType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Lecture).Value;
            PractiseType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Practise).Value;
            SeminareType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Seminare).Value;
            LectureTypeEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.LectureEng).Value;
            PractiseTypeEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.PractiseEng).Value;
            SeminareTypeEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.SeminareEng).Value;
            CreditType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Credit).Value;
            ClassifiedCreditType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ClassifiedCredit).Value;
            ExamType = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Exam).Value;
            CreditTypeEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.CreditEng).Value;
            ClassifiedCreditTypeEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ClassifiedCreditEng).Value;
            ExamTypeEng = WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ExamEng).Value;
        }

        private void GenerateXml(object obj)
        {
            DataContractSerializer xs = new DataContractSerializer(typeof(EntitiesWrapper));
            TextWriter filestream = new StreamWriter(@"C:\School\haha.xml");

            using (var xmlWriter = XmlWriter.Create(filestream, new XmlWriterSettings { Indent = true }))
            {
                xs.WriteObject(xmlWriter, EntitiesWrapper);
                
            }

            filestream.Close();

            Notifier.ShowSuccess("Database model was successfuly exported to XML");
        }

        private void ImportFromXml(object obj)
        {
            DataContractSerializer xs = new DataContractSerializer(typeof(EntitiesWrapper));
            TextReader filestream2 = new StreamReader(@"C:\School\haha.xml");
            using (var xmlReader = XmlReader.Create(filestream2))
            {
                var test = xs.ReadObject(xmlReader, true);

            }

            filestream2.Close();
        }

        private void UpdatePointsHandler(object obj)
        {
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Lecture).Value = LectureType;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Practise).Value = PractiseType;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Seminare).Value = SeminareType;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.LectureEng).Value = LectureTypeEng;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.PractiseEng).Value = PractiseTypeEng;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.SeminareEng).Value = SeminareTypeEng;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Credit).Value = CreditType;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ClassifiedCredit).Value = ClassifiedCreditType;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Exam).Value = ExamType;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Credit).Value = CreditTypeEng;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.ClassifiedCredit).Value = ClassifiedCreditTypeEng;
            WorkingPointsWeights.ElementAt((int)WorkingWeightTypes.Exam).Value = ExamTypeEng;

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
        private double _practiseType;
        public double PractiseType
        {
            get { return _practiseType; }
            set
            {
                _practiseType = value;
                OnPropertyChanged(nameof(PractiseType));
            }
        }
        private double _seminareType;
        public double SeminareType
        {
            get { return _seminareType; }
            set
            {
                _seminareType = value;
                OnPropertyChanged(nameof(SeminareType));
            }
        }
        private double _lectureTypeEng;
        public double LectureTypeEng
        {
            get { return _lectureTypeEng; }
            set
            {
                _lectureTypeEng = value;
                OnPropertyChanged(nameof(LectureTypeEng));
            }
        }
        private double _practiseTypeEng;
        public double PractiseTypeEng
        {
            get { return _practiseTypeEng; }
            set
            {
                _practiseTypeEng = value;
                OnPropertyChanged(nameof(PractiseTypeEng));
            }
        }
        private double _seminareTypeEng;
        public double SeminareTypeEng
        {
            get { return _seminareTypeEng; }
            set
            {
                _seminareTypeEng = value;
                OnPropertyChanged(nameof(SeminareTypeEng));
            }
        }
        private double _creditType;
        public double CreditType
        {
            get { return _creditType; }
            set
            {
                _creditType = value;
                OnPropertyChanged(nameof(CreditType));
            }
        }
        private double _classifiedCreditType;
        public double ClassifiedCreditType
        {
            get { return _classifiedCreditType; }
            set
            {
                _classifiedCreditType = value;
                OnPropertyChanged(nameof(ClassifiedCreditType));
            }
        }
        private double _examType;
        public double ExamType
        {
            get { return _examType; }
            set
            {
                _examType = value;
                OnPropertyChanged(nameof(ExamType));
            }
        }
        private double _creditTypeEng;
        public double CreditTypeEng
        {
            get { return _creditTypeEng; }
            set
            {
                _creditTypeEng = value;
                OnPropertyChanged(nameof(CreditTypeEng));
            }
        }
        private double _classifiedCreditTypeEng;
        public double ClassifiedCreditTypeEng
        {
            get { return _classifiedCreditTypeEng; }
            set
            {
                _classifiedCreditTypeEng = value;
                OnPropertyChanged(nameof(ClassifiedCreditTypeEng));
            }
        }
        private double _examTypeEng;
        public double ExamTypeEng
        {
            get { return _examTypeEng; }
            set
            {
                _examTypeEng = value;
                OnPropertyChanged(nameof(ExamTypeEng));
            }
        }

    }
}
