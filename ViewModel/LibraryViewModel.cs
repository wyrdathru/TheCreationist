using GalaSoft.MvvmLight;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProjectVoid.TheCreationist.ViewModel
{
    public class LibraryViewModel : ViewModelBase, IDisposable
    {
        private Library _Library;

        private bool _IsDirty;

        private ObservableCollection<SwatchViewModel> _Swatches;

        public LibraryViewModel(MainViewModel mainViewModel)
            : this(mainViewModel, new Library())
        {

        }

        public LibraryViewModel(MainViewModel mainViewModel, Library library)
        {
            MainViewModel = mainViewModel;

            Library = library;
            Swatches = new ObservableCollection<SwatchViewModel>();
            IsDirty = false;

            Initialize();
        }

        public MainViewModel MainViewModel { get; set; }

        public Library Library
        {
            get { return _Library; }

            set
            {
                _Library = value;
                RaisePropertyChanged("Library");
            }
        }

        public ObservableCollection<SwatchViewModel> Swatches
        {
            get { return _Swatches; }

            set
            {
                _Swatches = value;
                RaisePropertyChanged("Swatches");
            }
        }

        public string Id
        {
            get { return Library.Id.ToString(); }
        }

        public string Name
        {
            get { return Library.Name; }

            set
            {
                Library.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return Library.Description; }

            set
            {
                Library.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string Tags
        {
            get { return Library.Tags; }

            set
            {
                Library.Tags = value;
                RaisePropertyChanged("Tags");
            }
        }

        public string Author
        {
            get { return Library.Author; }

            set
            {
                Library.Author = value;
                RaisePropertyChanged("Author");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get { return _IsDirty; }

            set
            {
                _IsDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

        private void Initialize()
        {
            Logger.Log.Debug("Initializing");

            LoadSwatches();

            Logger.Log.Debug("Initialized");
        }

        private void LoadSwatches()
        {
            Logger.Log.Debug("Loading");

            foreach (Swatch s in Library.Swatches)
            {
                Swatches.Add(new SwatchViewModel(MainViewModel, s));
            }

            Logger.Log.DebugFormat("Loaded Swatches[{0}]", Swatches.Count);
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            Library = null;

            Swatches.Clear();
            Swatches = null;

            MainViewModel = null;

            Library = null;

            Logger.Log.Debug("Disposed");
        }
    }
}
