using AP8POSecretary.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using System.Windows;

namespace AP8POSecretary.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        public Notifier Notifier { get; set; }


        public BaseViewModel()
        {
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
        }
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
