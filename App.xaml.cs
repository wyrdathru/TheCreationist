using log4net;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Properties;
using System;
using System.Reflection;
using System.Windows;

namespace ProjectVoid.TheCreationist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Logger.Create();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);

            Logger.Log.Debug("Started");
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();

            Logger.Log.Debug("Exited");
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args) 
        {
            Logger.Log.Fatal("Exception", (Exception)args.ExceptionObject);
        }
    }
}
