using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Model;
using System;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class SwatchViewModel : ViewModelBase, IDisposable
    {
        private bool _IsSelected;

        public SwatchViewModel(MainViewModel mainViewModel, Swatch swatch)
        {
            MainViewModel = mainViewModel;

            IsSelected = false;

            Swatch = swatch;
        }

        public MainViewModel MainViewModel { get; set; }

        public Swatch Swatch { get; set; }

        public Color Color
        {
            get { return Swatch.Color; }

            set
            {
                Swatch.Color = value;
                RaisePropertyChanged("Color");
            }
        }

        public bool IsSelected
        {
            get { return _IsSelected; }

            set
            {
                _IsSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            Swatch = null;

            Logger.Log.Debug("Disposed");
        }
    }
}
