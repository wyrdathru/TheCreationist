using GalaSoft.MvvmLight;
using ProjectVoid.Core.Utilities;
using System;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class OptionsViewModel : ViewModelBase, IDisposable
    {
        public OptionsViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; private set; }

        public bool IsSpellCheckEnabled
        {
            get { return MainViewModel.ActiveProject.IsSpellCheckEnabled; }

            set
            {
                MainViewModel.ActiveProject.IsSpellCheckEnabled = value;
                RaisePropertyChanged("IsSpellCheckEnabled");
            }
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}