using GalaSoft.MvvmLight;
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

        private void Initialize()
        {
            for (int i = 0; i < 1; i++)
            {
                Projects.Add(new ProjectViewModel());
            }
        }

    }
}