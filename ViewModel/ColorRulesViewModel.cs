using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Enum;
using ProjectVoid.TheCreationist.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class ColorRulesViewModel : ViewModelBase, IDisposable
    {
        private ObservableCollection<SwatchViewModel> _ChosenColors;

        public ColorRulesViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _ChosenColors = new ObservableCollection<SwatchViewModel>();

            _ChosenColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Red)));
            _ChosenColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Green)));
            _ChosenColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Blue)));
            //_ChosenColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Red)));
            //_ChosenColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Green)));
            //_ChosenColors.Add(new SwatchViewModel(MainViewModel, new Swatch(Colors.Blue)));
        }

        public MainViewModel MainViewModel { get; set; }

        public TextSelection Selection { get; set; }

        public RuleTypes Type { get; set; }

        public int Scope { get; set; }

        public int Interval { get; set; }

        public ObservableCollection<SwatchViewModel> ChosenColors
        {
            get
            {
                return _ChosenColors;
            }
            set
            {
                _ChosenColors = value;
                RaisePropertyChanged("ChosenColors");
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