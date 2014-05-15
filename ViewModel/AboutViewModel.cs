using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Reflection;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class AboutViewModel : ViewModelBase, IDisposable
    {
        private Assembly _Assembly;
        private FileVersionInfo _FileInfo;

        public AboutViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _Assembly = Assembly.GetExecutingAssembly();
            _FileInfo = FileVersionInfo.GetVersionInfo(_Assembly.Location);

            Version = _FileInfo.FileVersion;
            Product = _FileInfo.FileDescription;
            Author = _FileInfo.CompanyName;
            Copyright = _FileInfo.LegalCopyright;
            Description = _FileInfo.Comments;
        }

        public MainViewModel MainViewModel { get; private set; }

        public string Version { get; set; }
        public string Product { get; set; }
        public string Author { get; set; }
        public string Copyright { get; set; }
        public string Description { get; set; }

        public void Dispose()
        {
            _Assembly = null;
            _FileInfo = null;

            Version = string.Empty;
            Product = string.Empty;
            Author = string.Empty;
            Copyright = string.Empty;
            Description = string.Empty;
        }
    }
}