using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;

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

            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>((e) => OnWindowClosing(e));
        }

        public MainViewModel MainViewModel { get; set; }

        public RelayCommand<CancelEventArgs> OnWindowClosingCommand { get; set; }

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

        private void OnWindowClosing(CancelEventArgs e)
        {
            if (MainViewModel.Libraries.Any<LibraryViewModel>(l => l.IsDirty))
            {
                var result = MessageBox.Show(String.Format("You have unsaved changes in your libraries. Are you sure you want to exit?"), String.Format("Exit"), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                for (int i = MainViewModel.Libraries.Count - 1; i > -1; i--)
                {
                    if (MainViewModel.Libraries[i].IsDirty)
                    {
                        MainViewModel.LibraryManager.DiscardChanges(MainViewModel.Libraries[i]);
                    }
                }
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