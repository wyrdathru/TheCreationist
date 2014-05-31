using GalaSoft.MvvmLight.Command;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Properties;
using ProjectVoid.TheCreationist.View;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace ProjectVoid.TheCreationist.Managers
{
    public class WindowManager
    {
        public WindowManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            OptionsViewModel = new OptionsViewModel(MainViewModel);
            AboutViewModel = new AboutViewModel(MainViewModel);
            PaletteViewModel = new PaletteViewModel(MainViewModel);
            PaletteEditorViewModel = new PaletteEditorViewModel(MainViewModel);

            DisplayOptionsCommand = new RelayCommand(
                () => DisplayOptions(),
                () => CanDisplayOptions());

            DisplayAboutCommand = new RelayCommand(
                () => DisplayAbout(),
                () => CanDisplayAbout());

            DisplayPaletteCommand = new RelayCommand(
                () => DisplayPalette(),
                () => CanDisplayPalette());

            DisplayPaletteEditorCommand = new RelayCommand<LibraryViewModel>(
                (l) => DisplayPaletteEditor(l),
                (l) => CanDisplayPaletteEditor(l));

            ClosePaletteEditorCommand = new RelayCommand<CancelEventArgs>(
                (e) => ClosePaletteEditor(e),
                (e) => CanClosePaletteEditor(e));
        }

        public MainViewModel MainViewModel { get; private set; }

        public OptionsViewModel OptionsViewModel { get; private set; }
        public AboutViewModel AboutViewModel { get; private set; }
        public PaletteViewModel PaletteViewModel { get; private set; }
        public PaletteEditorViewModel PaletteEditorViewModel { get; private set; }

        public RelayCommand DisplayOptionsCommand { get; private set; }
        public RelayCommand DisplayAboutCommand { get; private set; }
        public RelayCommand DisplayPaletteCommand { get; private set; }
        public RelayCommand<LibraryViewModel> DisplayPaletteEditorCommand { get; private set; }
        public RelayCommand<CancelEventArgs> ClosePaletteEditorCommand { get; private set; }

        private void DisplayOptions()
        {
            OptionsView optionsView = new OptionsView();

            optionsView.DataContext = OptionsViewModel;
            optionsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            optionsView.Owner = Application.Current.MainWindow;

            optionsView.ShowDialog();
        }

        private bool CanDisplayOptions()
        {
            return true;
        }

        private void DisplayAbout()
        {
            AboutView aboutView = new AboutView();

            aboutView.DataContext = AboutViewModel;
            aboutView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutView.Owner = Application.Current.MainWindow;

            aboutView.ShowDialog();
        }

        private bool CanDisplayAbout()
        {
            return true;
        }

        private void DisplayPalette()
        {
            PaletteView paletteView = new PaletteView();

            paletteView.DataContext = PaletteViewModel;
            paletteView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            paletteView.Owner = Application.Current.MainWindow;

            paletteView.ShowDialog();
        }

        private bool CanDisplayPalette()
        {
            return true;
        }

        private void DisplayPaletteEditor(LibraryViewModel libraryViewModel)
        {
            PaletteEditorViewModel.LibraryViewModel = libraryViewModel;
            PaletteEditorViewModel.LastEditedLibrary = libraryViewModel.Name;
            libraryViewModel.IsDirty = true;

            PaletteEditorView paletteEditorView = new PaletteEditorView();

            paletteEditorView.DataContext = PaletteEditorViewModel;
            paletteEditorView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            paletteEditorView.Owner = Application.Current.MainWindow;

            paletteEditorView.ShowDialog();
        }

        private bool CanDisplayPaletteEditor(LibraryViewModel libraryViewModel)
        {
            return true;
        }

        private void ClosePaletteEditor(CancelEventArgs eventArgs)
        {
            PaletteEditorViewModel.LibraryViewModel.SerializeToFile();
            PaletteEditorViewModel.LibraryViewModel.IsDirty = false;

            if (!PaletteEditorViewModel.LastEditedLibrary.Equals(PaletteEditorViewModel.LibraryViewModel.Name))
            {
                try
                {
                    File.Delete(Settings.Default.Libraries + "\\" + PaletteEditorViewModel.LastEditedLibrary + ".xml");
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Exception DeleteFailed", ex);
                }
            }
        }

        private bool CanClosePaletteEditor(CancelEventArgs eventArgs)
        {
            return true;
        }
    }
}
