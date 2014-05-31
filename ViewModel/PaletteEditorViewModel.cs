using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.ViewModel;
using System;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class PaletteEditorViewModel : ViewModelBase, IDisposable
    {
        public PaletteEditorViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; set; }

        public LibraryViewModel LibraryViewModel { get; set; }

        public string LastEditedLibrary { get; set; }

        public void Dispose()
        {
            //TODO: Implement Dispose
        }
    }
}