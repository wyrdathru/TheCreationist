using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.ViewModel;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class PaletteViewModel : ViewModelBase
    {
        private string _NewSwatchValue;

        private LibraryViewModel _ActiveLibrary;

        public PaletteViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _NewSwatchValue = "";
        }

        public MainViewModel MainViewModel { get; set; }

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
            get
            {
                return _NewSwatchValue;
            }

            set
            {
                _NewSwatchValue = value;
                RaisePropertyChanged("NewSwatchValue");
            }
        }
    }
}