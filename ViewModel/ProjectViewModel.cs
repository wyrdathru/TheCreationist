using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Composite;
using ProjectVoid.TheCreationist.Model;
using System;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class ProjectViewModel : ViewModelBase, IDisposable
    {
        private DateTime _LastChanged;
        private ProjectState _State;
        private TextSelection _Selection;
        private bool _IsSpellCheckEnabled;

        public ProjectViewModel(MainViewModel mainViewModel, Project project)
        {
            MainViewModel = mainViewModel;
            Project = project;

            State = new ProjectState() { IsDirty = false, IsSaved = false };
            _LastChanged = DateTime.Now;
            _IsSpellCheckEnabled = false;
        }

        [PreferredConstructor]
        public ProjectViewModel(MainViewModel mainViewModel) : this(mainViewModel, new Project())
        {

        }

        public MainViewModel MainViewModel { get; set; }

        public Project Project { get; set; }

        public string Id { get { return Project.Id.ToString(); } }

        public string Name
        {
            get { return Project.Name; }

            set
            {
                Project.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public FlowDocument Document
        {
            get { return Project.Document; }

            set
            {
                Project.Document = value;
                RaisePropertyChanged("Document");
            }
        }

        public Color Foreground
        {
            get { return Project.Foreground; }

            set
            {
                Project.Foreground = value;
                RaisePropertyChanged("Foreground");
            }
        }

        public Color Background
        {
            get { return Project.Background; }

            set
            {
                Project.Background = value;
                RaisePropertyChanged("Background");
            }
        }

        public Color Backdrop
        {
            get { return Project.Backdrop; }

            set
            {
                Project.Backdrop = value;
                RaisePropertyChanged("Backdrop");
            }
        }

        public DateTime LastChanged
        {
            get { return _LastChanged; }

            set
            {
                _LastChanged = value;
                RaisePropertyChanged("LastChanged");

                State.IsDirty = true;
            }
        }

        public ProjectState State
        {
            get { return _State; }

            set
            {
                _State = value;
                _State.PropertyChanged += FireStatePropertyChanged;
                RaisePropertyChanged("State");
            }
        }

        public TextSelection Selection
        {
            get { return _Selection; }

            set
            {
                _Selection = value;
                RaisePropertyChanged("Selection");
            }
        }

        public bool IsSpellCheckEnabled
        {
            get { return _IsSpellCheckEnabled; }

            set
            {
                _IsSpellCheckEnabled = value;
                RaisePropertyChanged("IsSpellCheckEnabled");
            }
        }

        private void FireStatePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("State");
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            State = null;

            Selection = null;

            MainViewModel = null;

            Project = null;

            Logger.Log.Debug("Disposed");
        }
    }
}