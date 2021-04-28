using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.Domain.XmlWrapper;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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

            //SendEmailWithAttachment();

            GetEntities();
            GetWorkingPointsWeights();
           
        }

        private async void GetEntities()
        {

            try
            {
                Employees = await _employeeDataService.GetAll();
                Subjects = await _subjectDataService.GetAll();
                Groups = await _groupDataService.GetAll();
                WorkingLabels = await _workingLabelDataService.GetAll();
                WorkingPointsWeightsXML = await _workingPointsWeight.GetAll();
                GroupSubjects = await _groupSubjectDataService.GetAll();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }

            EntitiesWrapper.Subjects = Subjects;
            EntitiesWrapper.Employees = Employees;
            EntitiesWrapper.Groups = Groups;
            EntitiesWrapper.GroupSubjects = GroupSubjects;
            EntitiesWrapper.WorkingLabels = WorkingLabels;
            EntitiesWrapper.WorkingPointsWeights = WorkingPointsWeightsXML;
        }

        private async void GetWorkingPointsWeights()
        {
            try
            {
                var points = await _workingPointsWeight.GetAll();
                WorkingPointsWeights = points.ToList();
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to load a data from database with error: " + ex);
            }
           
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
            string filePath = string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
                filePath = saveFileDialog.FileName;

            bool wasSuccessful = false;

            if (filePath != "")
            {
                try
                {
                    DataContractSerializer xs = new DataContractSerializer(typeof(EntitiesWrapper));
                    TextWriter filestream = new StreamWriter(filePath);

                    using (var xmlWriter = XmlWriter.Create(filestream, new XmlWriterSettings { Indent = true }))
                    {
                        xs.WriteObject(xmlWriter, EntitiesWrapper);
                    }

                    filestream.Close();

                    Notifier.ShowSuccess("Database model was successfuly exported to XML");
                    wasSuccessful = true;
                }
                catch (Exception ex)
                {
                    Notifier.ShowError("Exporting to XML failed with exception: {0}, please concact your administrator " + ex);
                }

                if(SendEmail)
                {
                    try
                    {
                        if (wasSuccessful && IsValidEmail(EmailToSend))
                        {
                            SendEmailWithAttachment(filePath, EmailToSend);
                            Notifier.ShowSuccess("Email was send succesfully");
                        }
                        else
                        {
                            Notifier.ShowError("There was a problem with sanding an email, check if email adress or file exists");
                        }
                    }
                    catch (Exception ex)
                    {
                        Notifier.ShowError("Sending email failed with error: " + ex);
                    }                    
                }               

            }
            
        }

        private void ImportFromXml(object obj)
        {
            string filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml file (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
                filePath = openFileDialog.FileName;
            if(filePath != "")
            {
                try
                {
                    DataContractSerializer xs = new DataContractSerializer(typeof(EntitiesWrapper));
                    TextReader fileStream = new StreamReader(filePath);
                    bool result;
                    using (var xmlReader = XmlReader.Create(fileStream))
                    {
                        var eWrapper = xs.ReadObject(xmlReader, true);
                        result = _employeeDataService.Import((EntitiesWrapper)eWrapper);
                    }

                    fileStream.Close();
                   
                    if(result)
                        Notifier.ShowSuccess("Database model was successfuly imported from XML");
                    else
                        Notifier.ShowWarning("Xml file is probably demaged, and it cannot be loaded !");
                }
                catch (Exception ex)
                {
                    Notifier.ShowError("Import from XML failed with exception: {0}, please concact your administrator " + ex);
                }
               
            }           
        }

        private async void SendEmailWithAttachment(string filePath, string to)
        {
            var basicCredential = new NetworkCredential("SecretarySender@outlook.cz", "NL={`s=<uLa,9:<E");
            var sender = new SmtpSender(() => new SmtpClient("outlook.office365.com") {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = basicCredential, 
            });

            Email.DefaultSender = sender;

            await Email
                .From("SecretarySender@outlook.cz")
                .To(to)
                .Subject("Exported xml file")
                .Body("It has been sent to you xml file in an attachment, from Secretary app")
                .AttachFromFilename(filePath)
                .SendAsync();
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

            try
            {
                _workingPointsWeight.Update(WorkingPointsWeights);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to update a data in database with error: " + ex);
            }

            Notifier.ShowSuccess("Data in database updated successfuly");
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

        private bool _sendEmail;
        public bool SendEmail
        {
            get { return _sendEmail; }
            set
            {
                _sendEmail = value;
                OnPropertyChanged(nameof(SendEmail));
            }
        }

        private string _emailToSend;
        public string EmailToSend {
            get
            {
                return _emailToSend;
            }
            set
            {
                _emailToSend = value;
                OnPropertyChanged(nameof(EmailToSend));
            }
        
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
