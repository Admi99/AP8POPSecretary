using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    { 
        private readonly IDataService<Group> _dataService;

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public GroupsViewModel(IDataService<Group> dataService)
        {
            _dataService = dataService;

            loadProperties();
            
        }

        private async void loadProperties()
        {
            var test = await _dataService.GetAll();
            Groups = new ObservableCollection<Group>(test);
        }

       
    }
}
