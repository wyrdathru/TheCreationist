using GalaSoft.MvvmLight;
using System;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class LibraryManagementViewModel : ViewModelBase, IDisposable
    {
        private string _NewSwatchValue;

        private LibraryViewModel _ActiveLibrary;

        public LibraryManagementViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _NewSwatchValue = "";
        }

        public MainViewModel MainViewModel { get; set; }

        public LibraryViewModel ActiveLibrary
        {
            get { return _ActiveLibrary; }

            set
            {
                _ActiveLibrary = value;
                RaisePropertyChanged("ActiveLibrary");
            }
        }

        public string NewSwatchValue
        {
            get { return _NewSwatchValue; }

            set
            {
                _NewSwatchValue = value;
                RaisePropertyChanged("NewSwatchValue");
            }
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            NewSwatchValue = string.Empty;

            ActiveLibrary = null;

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}