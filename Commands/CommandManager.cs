using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.View;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.Commands
{
    public class CommandManager
    {
        public CommandManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            CreateProjectCommand = new RelayCommand(
                () => CreateProject(),
                () => CanCreateProject());

            OpenProjectCommand = new RelayCommand(
                () => OpenProject(),
                () => CanOpenProject());

            SaveProjectCommand = new RelayCommand(
                () => SaveProject(),
                () => CanSaveProject());

            CloseProjectCommand = new RelayCommand(
                () => CloseProject(),
                () => CanCloseProject());

            ConvertProjectCommand = new RelayCommand(
                () => ConvertProject(),
                () => CanConvertProject());

            CompileProjectCommand = new RelayCommand(
                () => CompileProject(),
                () => CanCompileProject());

            ExitApplicationCommand = new RelayCommand(
                () => ExitApplication(),
                () => CanExitApplication());

            DisplayOptionsCommand = new RelayCommand(
                () => DisplayOptions(),
                () => CanDisplayOptions());

            DisplayAboutCommand = new RelayCommand(
                () => DisplayAbout(),
                () => CanDisplayAbout());

            SelectSwatchCommand = new RelayCommand<MouseButtonEventArgs>(
                (e) => SelectSwatch(e),
                (e) => CanSelectSwatch(e));
        }

        public MainViewModel MainViewModel { get; private set; }
        
        public RelayCommand CreateProjectCommand { get; private set; }
        public RelayCommand OpenProjectCommand { get; private set; }
        public RelayCommand SaveProjectCommand { get; private set; }
        public RelayCommand CloseProjectCommand { get; private set; }
        public RelayCommand ConvertProjectCommand { get; private set; }
        public RelayCommand CompileProjectCommand { get; private set; }
        public RelayCommand ExitApplicationCommand { get; private set; }
        public RelayCommand DisplayOptionsCommand { get; private set; }
        public RelayCommand DisplayAboutCommand { get; private set; }
        public RelayCommand<MouseButtonEventArgs> SelectSwatchCommand { get; private set; }

        private void CreateProject()
        {
            Console.WriteLine("CreateProject");
        }

        private bool CanCreateProject()
        {
            return true;
        }

        private void OpenProject()
        {
            Console.WriteLine("OpenProject");
        }

        private bool CanOpenProject()
        {
            return true;
        }

        private void SaveProject()
        {
            Console.WriteLine("SaveProject");
        }

        private bool CanSaveProject()
        {
            return true;
        }

        private void CloseProject()
        {
            Console.WriteLine("CloseProject");
        }

        private bool CanCloseProject()
        {
            return true;
        }

        private void ConvertProject()
        {
            Console.WriteLine("ConvertProject");
        }

        private bool CanConvertProject()
        {
            return true;
        }

        private void CompileProject()
        {
            Console.WriteLine("CompileProject");
        }

        private bool CanCompileProject()
        {
            return true;
        }

        private void ExitApplication()
        {
            Console.WriteLine("ExitApplication");
        }

        private bool CanExitApplication()
        {
            return true;
        }

        private void DisplayOptions()
        {
            Console.WriteLine("DisplayOptions");
        }

        private bool CanDisplayOptions()
        {
            return true;
        }

        private void DisplayAbout()
        {
            Console.WriteLine("DisplayAbout");
        }

        private bool CanDisplayAbout()
        {
            return true;
        }

        private void SelectSwatch(MouseButtonEventArgs eventArgs)
        {
            SwatchViewModel swatch = ((SwatchView)eventArgs.Source).DataContext as SwatchViewModel;

            switch (eventArgs.ChangedButton)
            {
                case MouseButton.Left:
                    MainViewModel.ActiveProject.Foreground = swatch.Color;

                    if (MainViewModel.ActiveProject.Selection != null && !MainViewModel.ActiveProject.Selection.IsEmpty)
                    {
                        MainViewModel.ActiveProject.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush(swatch.Color));
                    }
                    break;

                case MouseButton.Right:
                    MainViewModel.ActiveProject.Background = swatch.Color;

                    if (MainViewModel.ActiveProject.Selection != null && !MainViewModel.ActiveProject.Selection.IsEmpty)
                    {
                        MainViewModel.ActiveProject.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, new SolidColorBrush(swatch.Color));
                    }
                    break;

                case MouseButton.Middle:
                    MainViewModel.ActiveProject.Backdrop = swatch.Color;
                    break;

                default:
                    return;
            }

            MainViewModel.ActiveProject.State.IsDirty = true;

            eventArgs.Handled = true;
        }

        private bool CanSelectSwatch(MouseButtonEventArgs eventArgs)
        {
            return true;
        }
    }
}
