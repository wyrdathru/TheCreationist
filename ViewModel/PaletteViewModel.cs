using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.ViewModel;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class PaletteViewModel : ViewModelBase
    {
        public PaletteViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; set; }
    }
}