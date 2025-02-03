using Eventplanner.UI.Startup;
using Eventplanner.UI.View;
using System;
using System.Windows;
using Autofac;

namespace Eventplanner.UI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        //private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        //{

        //    MessageBox.Show("Ein unvorhergesehener Fehler ist aufgetreten. Bitte Informieren Sie den Administrator."
        //+ Environment.NewLine + e.Exception.Message, "Unexpected error");

        //    e.Handled = true;
        //    return;
        //}
    }
}
