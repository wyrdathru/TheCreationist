using GalaSoft.MvvmLight.Command;
//using ProjectVoid.Core.Utilities;
using ProjectVoid.Core.Validation;
using ProjectVoid.TheCreationist.Properties;
using ProjectVoid.TheCreationist.View;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ProjectVoid.TheCreationist.Managers
{
    public class WindowManager : IDisposable
    {
        public WindowManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            OptionsViewModel = new OptionsViewModel(MainViewModel);
            AboutViewModel = new AboutViewModel(MainViewModel);
            LibraryManagementViewModel = new LibraryManagementViewModel(MainViewModel);
            LibraryEditorViewModel = new LibraryEditorViewModel(MainViewModel);

            OpenOptionsCommand = new RelayCommand(
                () => OpenOptions(),
                () => CanOpenOptions());

            OpenAboutCommand = new RelayCommand(
                () => OpenAbout(),
                () => CanOpenAbout());

            OpenLibraryManagementCommand = new RelayCommand(
                () => OpenLibraryManagement(),
                () => CanOpenLibraryManagement());

            OpenLibraryEditorCommand = new RelayCommand<LibraryViewModel>(
                (l) => OpenLibraryEditor(l),
                (l) => CanOpenLibraryEditor(l));

            CloseLibraryEditorCommand = new RelayCommand<CancelEventArgs>(
                (e) => CloseLibraryEditor(e),
                (e) => CanCloseLibraryEditor(e));
        }

        public MainViewModel MainViewModel { get; private set; }

        public OptionsViewModel OptionsViewModel { get; private set; }
        public AboutViewModel AboutViewModel { get; private set; }
        public LibraryManagementViewModel LibraryManagementViewModel { get; private set; }
        public LibraryEditorViewModel LibraryEditorViewModel { get; private set; }

        public RelayCommand OpenOptionsCommand { get; private set; }
        public RelayCommand OpenAboutCommand { get; private set; }
        public RelayCommand OpenLibraryManagementCommand { get; private set; }
        public RelayCommand<LibraryViewModel> OpenLibraryEditorCommand { get; private set; }
        public RelayCommand<CancelEventArgs> CloseLibraryEditorCommand { get; private set; }

        private void OpenOptions()
        {
            Logger.Log.Debug("Opened");

            OptionsView optionsView = new OptionsView();

            optionsView.DataContext = OptionsViewModel;
            optionsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            optionsView.Owner = Application.Current.MainWindow;

            optionsView.ShowDialog();

            Logger.Log.Debug("Closed");
        }

        private bool CanOpenOptions()
        {
            return true;
        }

        private void OpenAbout()
        {
            Logger.Log.Debug("Opened");

            AboutView aboutView = new AboutView();

            aboutView.DataContext = AboutViewModel;
            aboutView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutView.Owner = Application.Current.MainWindow;

            aboutView.ShowDialog();

            Logger.Log.Debug("Closed");
        }

        private bool CanOpenAbout()
        {
            return true;
        }

        private void OpenLibraryManagement()
        {
            Logger.Log.Debug("Opened");

            LibraryManagementView libraryManagementView = new LibraryManagementView();

            libraryManagementView.DataContext = LibraryManagementViewModel;
            libraryManagementView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            libraryManagementView.Owner = Application.Current.MainWindow;

            libraryManagementView.ShowDialog();

            Logger.Log.Debug("Closed");
        }

        private bool CanOpenLibraryManagement()
        {
            return true;
        }

        private void OpenLibraryEditor(LibraryViewModel libraryViewModel)
        {
            Logger.Log.Debug("Opened");

            LibraryEditorViewModel.LibraryViewModel = libraryViewModel;
            LibraryEditorViewModel.LastEditedLibrary = libraryViewModel.Name;
            libraryViewModel.IsDirty = true;

            LibraryEditorView libraryEditorView = new LibraryEditorView();

            libraryEditorView.DataContext = LibraryEditorViewModel;
            libraryEditorView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            libraryEditorView.Owner = Application.Current.MainWindow;

            libraryEditorView.ShowDialog();

            Logger.Log.Debug("Closed");
        }

        private bool CanOpenLibraryEditor(LibraryViewModel libraryViewModel)
        {
            return true;
        }

        private void CloseLibraryEditor(CancelEventArgs eventArgs)
        {
            MainViewModel.SerializeToFile(LibraryEditorViewModel.LibraryViewModel.Library);
            LibraryEditorViewModel.LibraryViewModel.IsDirty = false;

            if (!LibraryEditorViewModel.LastEditedLibrary.Equals(LibraryEditorViewModel.LibraryViewModel.Name))
            {
                try
                {
                    File.Delete(Settings.Default.Libraries + "\\" + LibraryEditorViewModel.LastEditedLibrary + ".xml");
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Exception DeleteFailed", ex);
                }
            }
        }

        private bool CanCloseLibraryEditor(CancelEventArgs eventArgs)
        {
            return true;
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            OptionsViewModel = null;

            AboutViewModel = null;

            LibraryManagementViewModel = null;

            LibraryEditorViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}
