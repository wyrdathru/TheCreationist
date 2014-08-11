using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class LibraryManagementViewModel : ViewModelBase, IDisposable
    {
        private Color _NewSwatch;

        private LibraryViewModel _ActiveLibrary;

        public LibraryManagementViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _NewSwatch = Settings.Default.Foreground;

            RecentColors = new List<Color>();

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

        public Color NewSwatch
        {
            get { return _NewSwatch; }

            set
            {
                _NewSwatch = value;
                RaisePropertyChanged("NewSwatch");
            }
        }

        public List<Color> RecentColors { get; set; }
        
        private void OnWindowClosing(CancelEventArgs e)
        {
            for (int i = MainViewModel.Libraries.Count - 1; i > -1; i--)
            {
                Logger.Log.Debug("Closing");

                if (MainViewModel.Libraries[i].IsDirty == true)
                {
                    var result = MessageBox.Show(String.Format("{0} has unsaved changes, do you want to save your changes?", MainViewModel.Libraries[i].Name), String.Format("Save {0}?", MainViewModel.Libraries[i].Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.No)
                    {
                        MainViewModel.LibraryManager.DiscardChanges(MainViewModel.Libraries[i]);
                        Logger.Log.Debug("Aborted UnsavedChangesDetected");
                        continue;
                    }

                    MainViewModel.LibraryManager.SaveChanges(MainViewModel.Libraries[i]);
                }

                Logger.Log.DebugFormat("Closed ID[{0}] Name[{1}]", MainViewModel.Libraries[i].Id, MainViewModel.Libraries[i].Name);
            }
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            ActiveLibrary = null;

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}