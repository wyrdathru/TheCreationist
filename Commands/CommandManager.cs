using GalaSoft.MvvmLight.Command;
using System;

namespace ProjectVoid.TheCreationist.Commands
{
    public class CommandManager
    {
        public CommandManager()
        {
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
        }

        public RelayCommand CreateProjectCommand { get; private set; }
        public RelayCommand OpenProjectCommand { get; private set; }
        public RelayCommand SaveProjectCommand { get; private set; }
        public RelayCommand CloseProjectCommand { get; private set; }
        public RelayCommand ConvertProjectCommand { get; private set; }
        public RelayCommand CompileProjectCommand { get; private set; }
        public RelayCommand ExitApplicationCommand { get; private set; }
        public RelayCommand DisplayOptionsCommand { get; private set; }
        public RelayCommand DisplayAboutCommand { get; private set; }

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
    }
}
