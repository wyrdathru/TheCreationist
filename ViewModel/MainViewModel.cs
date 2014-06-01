using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Managers;
using ProjectVoid.TheCreationist.Model;
using ProjectVoid.TheCreationist.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xaml;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private LibraryViewModel _ActiveLibrary;

        private ProjectViewModel _ActiveProject;

        private ObservableCollection<LibraryViewModel> _Libraries;

        private ObservableCollection<ProjectViewModel> _Projects;

        public MainViewModel()
        {
            Libraries = new ObservableCollection<LibraryViewModel>();
            Projects = new ObservableCollection<ProjectViewModel>();

            CommandManager = new CommandManager(this);
            WindowManager = new WindowManager(this);
            LibraryManager = new LibraryManager(this);

            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>((e) => OnWindowClosing(e));

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

        public CommandManager CommandManager { get; set; }

        public WindowManager WindowManager { get; set; }

        public LibraryManager LibraryManager { get; set; }

        public RelayCommand<CancelEventArgs> OnWindowClosingCommand { get; set; }

        private void Initialize()
        {
            Logger.Log.Debug("Initializing");

            LoadLibraries();

            LoadProjects();

            Logger.Log.Debug("Initialized");
        }

        private void LoadLibraries()
        {
            Logger.Log.Debug("Loading");

            var libraries = GetLibraries();

            foreach (FileInfo file in libraries)
            {
                var library = DeserializeFromFile(file.Name.Replace(".xml", string.Empty));

                Libraries.Add(new LibraryViewModel(this, library));
            }

            ActiveLibrary = Libraries[0];
            WindowManager.LibraryManagementViewModel.ActiveLibrary = Libraries[0];

            Logger.Log.DebugFormat("Loaded Libraries[{0}]", Libraries.Count);
        }

        private List<FileInfo> GetLibraries()
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directory = new DirectoryInfo(Settings.Default.Libraries);

            foreach (FileInfo file in directory.GetFiles("*.xml"))
            {
                files.Add(file);

                Logger.Log.DebugFormat("Found File[{0}]", file.Name);
            }

            return files;
        }

        public void SerializeToFile(Library library)
        {
            Logger.Log.Debug("Serializing");

            try
            {
                var path = Settings.Default.Libraries + "\\" + library.Name + ".xml";

                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    XamlServices.Save(fileStream, library);

                    Logger.Log.DebugFormat("Serialized ID[{0}] Name[{1}] Path[{2}]", library.Id, library.Name, path);

                    fileStream.Close();
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Invalid file name, please do not use special characters.");

                Logger.Log.Error("Serialize DirectoryNotFoundException", ex);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Serialize Exception", ex);
            }
        }

        public Library DeserializeFromFile(string name)
        {
            Library library;

            var path = Settings.Default.Libraries + "\\" + name + ".xml";

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                library = XamlServices.Load(fileStream) as Library;

                Logger.Log.DebugFormat("Deseralized ID[{0}] Name[{1}] Path[{2}]", library.Id, library.Name, path);

                fileStream.Close();
            }

            return library;
        }

        private void LoadProjects()
        {
            Logger.Log.Debug("Loading");


            for (int i = 0; i < 1; i++)
            {
                Projects.Add(new ProjectViewModel(this));
            }

            ActiveProject = Projects[0];

            Logger.Log.DebugFormat("Loaded Projects[{0}]", Projects.Count);
        }

        private void OnWindowClosing(CancelEventArgs e)
        {
            if (Projects.Any<ProjectViewModel>(p => !p.State.CanClose()))
            {
                var result = MessageBox.Show(String.Format("If you have unsaved work in your project(s) and may lose work. Are you sure you want to exit?"), String.Format("Exit"), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            ActiveLibrary = null;
            Libraries.Clear();
            Libraries = null;

            ActiveProject = null;
            Projects.Clear();
            Projects = null;

            CommandManager.Dispose();
            CommandManager = null;

            LibraryManager.Dispose();
            LibraryManager = null;

            WindowManager.Dispose();
            WindowManager = null;

            Logger.Log.Debug("Disposed");
        }
    }
}