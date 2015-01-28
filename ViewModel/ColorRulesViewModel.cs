using GalaSoft.MvvmLight;
using TheCreationist.App.Enum;
using TheCreationist.App.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace TheCreationist.App.ViewModel
{
    public class ColorRulesViewModel : ViewModelBase, IDisposable
    {
        private ObservableCollection<SwatchViewModel> _ForegroundColors;

        private ObservableCollection<SwatchViewModel> _BackgroundColors;

        public ColorRulesViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _ForegroundColors = new ObservableCollection<SwatchViewModel>();
            _BackgroundColors = new ObservableCollection<SwatchViewModel>();

            ForegroundColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Red)));
            ForegroundColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Green)));
            ForegroundColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Blue)));

            BackgroundColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Yellow)));
            BackgroundColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Purple)));
            BackgroundColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Silver)));

            Interval = 1;
        }

        public MainViewModel MainViewModel { get; set; }

        public TextSelection Selection { get; set; }

        public RuleTypes Type { get; set; }

        public RuleScopes Scope { get; set; }

        public int Interval { get; set; }

        public ObservableCollection<SwatchViewModel> ForegroundColors
        {
            get
            {
                return _ForegroundColors;
            }
            set
            {
                _ForegroundColors = value;
                RaisePropertyChanged("ForegroundColors");
            }
        }

        public ObservableCollection<SwatchViewModel> BackgroundColors
        {
            get
            {
                return _BackgroundColors;
            }
            set
            {
                _BackgroundColors = value;
                RaisePropertyChanged("BackgroundColors");
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