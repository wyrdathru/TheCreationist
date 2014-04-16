using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Model;
using System;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class SwatchViewModel : ViewModelBase, IDisposable
    {
        private Swatch _Swatch;

        public SwatchViewModel(MainViewModel mainViewModel, Swatch swatch)
        {
            MainViewModel = mainViewModel;

            Swatch = swatch;
        }

        public MainViewModel MainViewModel { get; set; }

        public Swatch Swatch { get; set; }

        public Color Color
        {
            get
            {
                return Swatch.Color;
            }
            set
            {
                Swatch.Color = value;
                RaisePropertyChanged("Color");
            }
        }

        public void Dispose()
        {
            //
        }
    }
}
