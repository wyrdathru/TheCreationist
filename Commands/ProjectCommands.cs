using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVoid.TheCreationist.Commands
{
    public class ProjectCommands
    {
        public ProjectCommands()
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
        }

        public RelayCommand CreateProjectCommand { get; private set; }
        public RelayCommand OpenProjectCommand { get; private set; }
        public RelayCommand SaveProjectCommand { get; private set; }
        public RelayCommand CloseProjectCommand { get; private set; }
        public RelayCommand ConvertProjectCommand { get; private set; }
        public RelayCommand CompileProjectCommand { get; private set; }

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
    }
}
