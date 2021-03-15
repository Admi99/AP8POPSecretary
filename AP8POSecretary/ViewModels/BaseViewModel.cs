using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected bool _isSaved;

        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                OnPropertyChanged(nameof(IsSaved));
            }
        }
        protected bool _isDeleted;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                OnPropertyChanged(nameof(IsDeleted));
            }
        }

    }
}
