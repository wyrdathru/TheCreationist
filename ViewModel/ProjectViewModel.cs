using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Model;
using System;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private DateTime _LastChanged;

        public ProjectViewModel()
        {
            Project = new Project();

            LastChanged = DateTime.Now;
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
            }
        }
    }
}