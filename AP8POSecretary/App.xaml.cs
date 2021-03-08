using AP8POSecretary.Infrastructure.Configuration;
using AP8POSecretary.State.Navigators;
using AP8POSecretary.ViewModels;
using AP8POSecretary.ViewModels.Factories;
using Ninject;
//using System;
using System.Windows;

namespace AP8POSecretary
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
             
            var builder = new DependencyBuilder(CreateServices());
            builder.Build();
          
            var appViewModel = builder.Get<MainViewModel>();
            var mainWindow = new MainWindow()
            {
                DataContext = appViewModel
            };

  
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }

        private StandardKernel CreateServices()
        {
            var kernel = new StandardKernel();
            kernel.Bind<INavigator>().To<Navigator>();
            kernel.Bind<ISecretaryViewModelAbstractFactory>().To<SecretaryViewModelAbstractFactory>();
            kernel.Bind<ISecretaryViewModelFactory<GroupsViewModel>>().To<GroupsViewModelFactory>();
            kernel.Bind<ISecretaryViewModelFactory<SubjectsViewModel>>().To<SubjectsViewModelFactory>();

            return kernel;
        }
       
  
    }

}
