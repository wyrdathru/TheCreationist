using GalaSoft.MvvmLight;
//using ProjectVoid.Core.Utilities;
using System;
using System.Collections.ObjectModel;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class OptionsViewModel : ViewModelBase, IDisposable
    {
        public OptionsViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            Initialize();
        }

        public MainViewModel MainViewModel { get; private set; }

        public ObservableCollection<int> FontSizes { get; private set; }

        public bool IsSpellCheckEnabled
        {
            get { return MainViewModel.ActiveProject.IsSpellCheckEnabled; }

            set
            {
                MainViewModel.ActiveProject.IsSpellCheckEnabled = value;
                RaisePropertyChanged("IsSpellCheckEnabled");
            }
        }

        private void Initialize()
        {
            GetFontSizes();
        }

        private void GetFontSizes()
        {
            FontSizes = new ObservableCollection<int>();

            for (int i = 8; i < 31; i++)
            {
                FontSizes.Add(i);
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