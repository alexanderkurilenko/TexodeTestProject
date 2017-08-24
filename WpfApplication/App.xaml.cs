using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using Ninject.Modules;



namespace WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
           // IocKernel.Initialize( new IocConfig());
            //IocKernel.LoadModule(new DependencyResolver());
          
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureContainer()
        {
            
            this.container = new StandardKernel();
        
            
           
        }
        private void ComposeObjects()
        {
            Current.MainWindow = container.Get<MainWindow>();

        }
    }
}
