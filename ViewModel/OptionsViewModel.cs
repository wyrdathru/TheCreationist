using GalaSoft.MvvmLight;
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

        private void Initialize()
        {
            //
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}