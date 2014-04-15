using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.Commands;
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

            Commands = new ProjectCommands();

            ExitApplicationCommand = new RelayCommand(
                () => ExitApplication(),
                () => CanExitApplication());

            Initialize();
        }

        public RelayCommand ExitApplicationCommand { get; private set; }

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

        public ProjectCommands Commands { get; set; }

        private void Initialize()
        {
            for (int i = 0; i < 1; i++)
            {
                Projects.Add(new ProjectViewModel());
            }
        }

        private void ExitApplication()
        {
            Console.WriteLine("ExitApplication");
        }

        private bool CanExitApplication()
        {
            return true;
        }
    }
}