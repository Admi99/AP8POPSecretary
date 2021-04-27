using AP8POSecretary.Commands;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using ToastNotifications.Messages;

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
            Notifier.ShowWarning("Add button will be disabled until validation will match !");

            _dataService = dataService;

            AddButtonCommand = new RelayCommand(AddData, CheckDataBeforeAdding);
            ModifySubjectsCommand = new RelayCommand(ModifyAllData);
            DeleteSubjectsCommand = new RelayCommand(DeleteAllData);
            DeleteCommand = new RelayCommand(DeleteData);

            InitAsync();
        }

        private async void DeleteData(object obj)
        {
            try
            {
                if (obj != null)
                {
                    IsDeleted = true;
                    await _dataService.Delete((obj as Employee).Id);
                    Employees.Remove(obj as Employee);
                    IsDeleted = false;
                }
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to delete a data from database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were deleted successfuly ");

        }

        private async void DeleteAllData(object obj)
        {
            try
            {
                IsDeleted = true;
                foreach (var item in Employees)
                {
                    await _dataService.Delete(item.Id);
                }
                Employees.Clear();
                IsDeleted = false;
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to delete a data from database with error: " + ex);
            }
            Notifier.ShowSuccess("Data were deleted successfuly ");
        }

        private async void ModifyAllData(object obj)
        {
            try
            {
                IsSaved = true;
                foreach (var item in Employees)
                {
                    await _dataService.Update(item.Id, item);
                }
                IsSaved = false;
            }
            catch (Exception ex) { Notifier.ShowError("Failed to modify a data in database with error: " + ex); }
            Notifier.ShowSuccess("Data were updated successfuly ");
        }

        public bool CheckDataBeforeAdding(object obj = null)
            => !String.IsNullOrEmpty(FirstName) &&
            !String.IsNullOrEmpty(LastName) &&
            !String.IsNullOrEmpty(WholeName) &&
            !String.IsNullOrEmpty(Email) &&
            IsValidEmail(Email) &&
            !String.IsNullOrEmpty(PersonalEmail) &&
            IsValidEmail(PersonalEmail) &&
            !String.IsNullOrEmpty(PhoneNumber) &&
            PhoneNumber.Length == 9;
           
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

            try
            {
                await _dataService.Create(newEmployee);
            }
            catch(Exception ex)
            {
                Notifier.ShowError("Failed to add a data to database with error: " + ex);
            }

            Notifier.ShowSuccess("Data were added successfuly ");
        }

        private async void InitAsync()
        {
            try
            {
                var employees = await _dataService.GetAll();
                Employees = new ObservableCollection<Employee>(employees);
            }           
            catch (Exception ex) { Notifier.ShowError("Failed to load a data from database with error: " + ex); }
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
