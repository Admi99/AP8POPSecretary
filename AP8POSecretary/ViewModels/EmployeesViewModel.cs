using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private readonly IDataService<Employee> _dataService;
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        public RelayCommand AddButtonCommand { get; private set; }
        public RelayCommand ModifySubjectsCommand { get; private set; }
        public RelayCommand DeleteSubjectsCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public EmployeesViewModel(IDataService<Employee> dataService)
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
                await _dataService.Delete((obj as Employee).Id);
                Employees.Remove(obj as Employee);
                IsDeleted = false;
            }
        }

        private async void DeleteAllData(object obj)
        {
            IsDeleted = true;
            foreach (var item in Employees)
            {
                await _dataService.Delete(item.Id);
            }
            Employees.Clear();
            IsDeleted = false;
        }

        private async void ModifyAllData(object obj)
        {
            IsSaved = true;
            foreach (var item in Employees)
            {
                await _dataService.Update(item.Id, item);
            }
            IsSaved = false;
        }

        public bool CheckDataBeforeAdding(object obj = null)
            => !String.IsNullOrEmpty(FirstName) &&
            !String.IsNullOrEmpty(LastName) &&
            !String.IsNullOrEmpty(WholeName) &&
            !String.IsNullOrEmpty(Email) &&
            !String.IsNullOrEmpty(PersonalEmail) &&
            !String.IsNullOrEmpty(PhoneNumber);


        private async void AddData(object obj = null)
        {

            Employee newEmployee = new Employee()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                WholeName = this.WholeName,
                Email = this.Email,
                PersonalEmail = this.PersonalEmail,
                PhoneNumber = this.PhoneNumber,
                isDoctorant = this.IsDoctorant,
                CommitmentRate = this.CommitmentRate
            };

            Employees.Add(newEmployee);
            await _dataService.Create(newEmployee);
        }

        private async void InitAsync()
        {
            var employees = await _dataService.GetAll();
            Employees = new ObservableCollection<Employee>(employees);
        }

        private string _firstName;
        public string FirstName {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _wholeName;
        public string WholeName
        {
            get { return _wholeName; }
            set
            {
                _wholeName = value;
                OnPropertyChanged(nameof(WholeName));
            }
        }

        private string _email;
        public string Email {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _personalEmail;
        public string PersonalEmail
        {
            get { return _personalEmail; }
            set
            {
                _personalEmail = value;
                OnPropertyChanged(nameof(PersonalEmail));
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        private bool _isDoctorant;
        public bool IsDoctorant
        {
            get { return _isDoctorant; }
            set
            {
                _isDoctorant = value;
                OnPropertyChanged(nameof(IsDoctorant));
            }
        }
        private double _commitmentRate = 1;
        public double CommitmentRate
        {
            get { return _commitmentRate; }
            set
            {
                _commitmentRate = value;
                OnPropertyChanged(nameof(CommitmentRate));
            }
        }
    }
}
