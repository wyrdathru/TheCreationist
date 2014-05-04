using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Enum;
using ProjectVoid.TheCreationist.Model;
using System;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private DateTime _LastChanged;
        private ProjectState _State;
        private TextSelection _Selection;

        public ProjectViewModel()
        {
            Project = new Project();

            State = new ProjectState() { IsDirty = false, IsSaved = false };
            _LastChanged = DateTime.Now;
        }

        public Project Project { get; set; }

        public string Name
        {
            get
            {
                return Project.Name;
            }

            set
            {
                Project.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public FlowDocument Document
        {
            get
            {
                return Project.Document;
            }

            set
            {
                Project.Document = value;
                RaisePropertyChanged("Document");
            }
        }

        public Color Foreground
        {
            get
            {
                return Project.Foreground;
            }

            set
            {
                Project.Foreground = value;
                RaisePropertyChanged("Foreground");
            }
        }

        public Color Background
        {
            get
            {
                return Project.Background;
            }

            set
            {
                Project.Background = value;
                RaisePropertyChanged("Background");
            }
        }

        public Color Backdrop
        {
            get
            {
                return Project.Backdrop;
            }

            set
            {
                Project.Backdrop = value;
                RaisePropertyChanged("Backdrop");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime LastChanged
        {
            get
            {
                return _LastChanged;
            }

            set
            {
                _LastChanged = value;
                RaisePropertyChanged("LastChanged");

                State.IsDirty = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProjectState State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = value;
                _State.PropertyChanged += FireStatePropertyChanged;
                RaisePropertyChanged("State");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextSelection Selection
        {
            get
            {
                return _Selection;
            }

            set
            {
                _Selection = value;
                RaisePropertyChanged("Selection");
            }
        }

        private void FireStatePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("State");
        }
    }
}