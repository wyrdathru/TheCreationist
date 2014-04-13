using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ProjectViewModel _ActiveProject;

        private ObservableCollection<ProjectViewModel> _Projects;

        public MainViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();

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

            Initialize();
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

        public ObservableCollection<ProjectViewModel> Projects
        {
            get { return _Projects; }

            set
            {
                _Projects = value;
                RaisePropertyChanged("Projects");
            }
        }

        public RelayCommand CreateProjectCommand { get; private set; }
        public RelayCommand OpenProjectCommand { get; private set; }
        public RelayCommand SaveProjectCommand { get; private set; }
        public RelayCommand CloseProjectCommand { get; private set; }

        private void Initialize()
        {
            for (int i = 0; i < 1; i++)
            {
                Projects.Add(new ProjectViewModel());
            }
        }

        private void CreateProject()
        {
            throw new NotImplementedException();
        }

        private bool CanCreateProject()
        {
            throw new System.NotImplementedException();
        }

        private void OpenProject()
        {
            throw new System.NotImplementedException();
        }

        private bool CanOpenProject()
        {
            throw new System.NotImplementedException();
        }

        private void SaveProject()
        {
            throw new System.NotImplementedException();
        }

        private bool CanSaveProject()
        {
            throw new System.NotImplementedException();
        }

        private void CloseProject()
        {
            throw new System.NotImplementedException();
        }

        private bool CanCloseProject()
        {
            throw new System.NotImplementedException();
        }
    }
}