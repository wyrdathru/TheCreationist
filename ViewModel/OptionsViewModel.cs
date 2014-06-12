using GalaSoft.MvvmLight;
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

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}