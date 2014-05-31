using System.ComponentModel;

namespace ProjectVoid.TheCreationist.Composite
{
    public class ProjectState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _IsSaved;
        private bool _IsDirty;

        public bool IsSaved
        {
            get { return _IsSaved; }

            set
            {
                _IsSaved = value;
                OnPropertyChanged("IsSaved");
            }
        }

        public bool IsDirty
        {
            get { return _IsDirty; }

            set
            {
                _IsDirty = value;
                OnPropertyChanged("IsDirty");

            }
        }

        public bool CanClose()
        {
            if (IsDirty == true)
            {
                return false;
            }

            return true;
        }

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
