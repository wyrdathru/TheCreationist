using log4net;
using ProjectVoid.TheCreationist.Properties;
using System;
using System.Reflection;
using System.Windows;

namespace ProjectVoid.TheCreationist
{
    public class Logger
    {
        public static ILog Log { get; private set; }

        public static void Create()
        {
            if (Log != null)
            {
                return;
            }

            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }

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
