using AP8POSecretary.Infrastructure.Configuration;
using AP8POSecretary.ViewModels;
using Ninject;
using System.Windows;

namespace AP8POSecretary
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var test = new StandardKernel();
            var builder = new DependencyBuilder(test);
            builder.Build();


            var appViewModel = builder.Get<MainViewModel>();
            var mainWindow = new MainWindow()
            {
                DataContext = appViewModel
            };

  
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
  
    }

}

/*MainWindow Get() => test.Get<MainWindow>();
  Current.MainWindow = Get();

  Current.MainWindow.Title = "DI with Ninject";*/
