using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Reflection;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class AboutViewModel : ViewModelBase, IDisposable
    {
        public AboutViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            Initialize();
        }

        public MainViewModel MainViewModel { get; private set; }

        public Assembly Assembly { get; private set; }
        public FileVersionInfo FileInfo { get; private set; }

        public string Version { get; set; }
        public string Product { get; set; }
        public string Author { get; set; }
        public string Copyright { get; set; }
        public string Description { get; set; }

        private void Initialize()
        {
            Logger.Log.Debug("Initializing");

            GetAssembly();

            GetFileInfo();

            Logger.Log.Debug("Initialized");
        }


        private void GetFileInfo()
        {
            Logger.Log.Debug("Getting FileInfo");

            FileInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);

            Version = FileInfo.FileVersion;
            Product = FileInfo.ProductName;
            Author = FileInfo.CompanyName;
            Copyright = FileInfo.LegalCopyright;
            Description = FileInfo.Comments;

            Logger.Log.DebugFormat("FileInfo Version[{0}] Product[{1}] Author[{2}] Copyright[{3}] Description[{4}]", Version, Product, Author, Copyright, Description);
        }

        private void GetAssembly()
        {
            Logger.Log.Debug("Getting Assembly");

            Assembly = Assembly.GetExecutingAssembly();

            Logger.Log.DebugFormat("Assembly Location[{0}]", Assembly.Location);
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            Assembly = null;
            FileInfo = null;

            Version = string.Empty;
            Product = string.Empty;
            Author = string.Empty;
            Copyright = string.Empty;
            Description = string.Empty;

            Logger.Log.Debug("Disposed");
        }
    }
}