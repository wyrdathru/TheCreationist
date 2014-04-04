using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MainViewCommand = new RelayCommand(() => Console.WriteLine("MainViewCommand"));
        }

        public RelayCommand MainViewCommand { get; private set; }
    }
}