using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ProjectVoid.TheCreationist.View;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Collections;

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

            OpenProjectCommand = new RelayCommand<MainViewModel>(
                (m) => OpenProject(m),
                (m) => CanOpenProject(m));

            SaveProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => SaveProject(p),
                (p) => CanSaveProject(p));

            CloseProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => CloseProject(p),
                (p) => CanCloseProject(p));

            ConvertProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => ConvertProject(p),
                (p) => CanConvertProject(p));

            CompileProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => CompileProject(p),
                (p) => CanCompileProject(p));

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

        public RelayCommand<MainViewModel> OpenProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> SaveProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> CloseProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> ConvertProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> CompileProjectCommand { get; private set; }

        public RelayCommand ExitApplicationCommand { get; private set; }

        public RelayCommand DisplayOptionsCommand { get; private set; }

        public RelayCommand DisplayAboutCommand { get; private set; }

        public RelayCommand<MouseButtonEventArgs> SelectSwatchCommand { get; private set; }

        private void CreateProject()
        {
            ProjectViewModel project = new ProjectViewModel();

            MainViewModel.Projects.Add(project);

            MainViewModel.ActiveProject = project;
        }

        private bool CanCreateProject()
        {
            return true;
        }

        private void OpenProject(MainViewModel mainViewModel)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();
            openFileDialog.Multiselect = false;
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "XML|*.xml";

            var result = openFileDialog.ShowDialog();

            if (result == false || result == null)
            {
                return;
            }

            var file = openFileDialog.FileName;

            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                ProjectViewModel project = XamlReader.Load(fileStream) as ProjectViewModel;

                if (mainViewModel.Projects.Any(p => p.Project.Id.Equals(project.Project.Id)))
                {
                    var match = mainViewModel.Projects.First(p => p.Project.Id.Equals(project.Project.Id));

                    mainViewModel.ActiveProject = match;

                    if (match.State.IsDirty)
                    {
                        var canReload = MessageBox.Show(String.Format("{0} has unsaved changes, are you sure you want to reload it?", match.Name), String.Format("Reload {0}?", match.Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (canReload == MessageBoxResult.No)
                        {
                            fileStream.Close();
                            return;
                        }

                        MainViewModel.Projects.Remove(match);
                    }
                    else
                    {
                        fileStream.Close();
                        return;
                    }
                }
                
                project.State.IsSaved = true;
                project.State.IsDirty = false;

                mainViewModel.Projects.Add(project);
                mainViewModel.ActiveProject = project;

                fileStream.Close();
            }
        }

        private bool CanOpenProject(MainViewModel mainViewModel)
        {
            return true;
        }

        private void SaveProject(ProjectViewModel projectViewModel)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML|*.xml";

            var result = saveFileDialog.ShowDialog();

            if (result == false || result == null)
            {
                return;
            }

            var file = saveFileDialog.FileName;

            projectViewModel.Name = Path.GetFileNameWithoutExtension(file);

            using (FileStream fileStream = new FileStream(file, FileMode.Create))
            {
                XamlWriter.Save(projectViewModel, fileStream);

                fileStream.Close();
            }

            projectViewModel.State.IsSaved = true;
            projectViewModel.State.IsDirty = false;
        }

        private bool CanSaveProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

            return true;
        }

        private void CloseProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel.State.IsDirty == true)
            {
                var result = MessageBox.Show(String.Format("{0} has unsaved changes, are you sure you want to close it?", projectViewModel.Name), String.Format("Close {0}?", projectViewModel.Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            MainViewModel.Projects.Remove(projectViewModel);
        }

        private bool CanCloseProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

            return true;
        }

        private void ConvertProject(ProjectViewModel projectViewModel)
        {
            Console.WriteLine("ConvertProject");
        }

        private bool CanConvertProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

            return true;
        }

        private void CompileProject(ProjectViewModel projectViewModel)
        {
            Console.WriteLine("CompileProject");
        }

        private bool CanCompileProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

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
