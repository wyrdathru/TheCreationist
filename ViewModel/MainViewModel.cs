using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Commands;
using ProjectVoid.TheCreationist.Model;
using ProjectVoid.TheCreationist.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xaml;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private LibraryViewModel _ActiveLibrary;

        private ProjectViewModel _ActiveProject;

        private ObservableCollection<LibraryViewModel> _Libraries;

        private ObservableCollection<ProjectViewModel> _Projects;

        public MainViewModel()
        {
            Libraries = new ObservableCollection<LibraryViewModel>();

            Projects = new ObservableCollection<ProjectViewModel>();

            Commands = new CommandManager(this);

            Initialize();
        }

        public LibraryViewModel ActiveLibrary
        {
            get { return _ActiveLibrary; }

            set
            {
                _ActiveLibrary = value;
                RaisePropertyChanged("ActiveLibrary");
            }
        }

        public ProjectViewModel ActiveProject
        {
            get { return _ActiveProject; }

            set
            {
                _ActiveProject = value;
                RaisePropertyChanged("ActiveProject");
            }
        }

        public ObservableCollection<LibraryViewModel> Libraries
        {
            get { return _Libraries; }

            set
            {
                _Libraries = value;
                RaisePropertyChanged("Libraries");
            }
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get { return _Projects; }

            set
            {
                _Projects = value;
                RaisePropertyChanged("Projects");
            }
        }

        public CommandManager Commands { get; set; }

        private void Initialize()
        {
            LoadLibraries();

            LoadProjects();
        }

        private void LoadLibraries()
        {
            foreach (FileInfo file in GetLibraries())
            {
                DeserializeLibrary(file.FullName);
            }

            ActiveLibrary = Libraries[0];
        }

        private List<FileInfo> GetLibraries()
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directory = new DirectoryInfo(Settings.Default.Libraries);

            foreach (FileInfo file in directory.GetFiles("*.xml"))
            {
                files.Add(file);
            }

            return files;
        }

        private void DeserializeLibrary(string path)
        {
            Library library;

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                library = XamlServices.Load(fileStream) as Library;

                fileStream.Close();
            }

            Libraries.Add(new LibraryViewModel(this, library));
        }

        private void LoadProjects()
        {
            for (int i = 0; i < 1; i++)
            {
                Projects.Add(new ProjectViewModel());
            }

            ActiveProject = Projects[0];
        }
    }
}