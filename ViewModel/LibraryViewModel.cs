using GalaSoft.MvvmLight;
using ProjectVoid.TheCreationist.Model;
using ProjectVoid.TheCreationist.Properties;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xaml;

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
            //
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
            get
            {
                return _Library;
            }

            set
            {
                _Library = value;
                RaisePropertyChanged("Library");
            }
        }


        public ObservableCollection<SwatchViewModel> Swatches
        {
            get
            {
                return _Swatches;
            }

            set
            {
                _Swatches = value;
                RaisePropertyChanged("Swatches");
            }
        }

        public string Name
        {
            get
            {
                return Library.Name;
            }

            set
            {
                Library.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return Library.Description;
            }

            set
            {
                Library.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }

            set
            {
                _IsDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

        public void Initialize()
        {
            foreach (Swatch s in Library.Swatches)
            {
                Swatches.Add(new SwatchViewModel(MainViewModel, s));
            }
        }

        public void Dispose()
        {
            MainViewModel = null;

            Library = null;
        }

        public void SerializeToFile()
        {
            Library library = Library;

            using (FileStream fileStream = new FileStream(Settings.Default.Libraries + "\\" + library.Name + ".xml", FileMode.Create))
            {
                XamlServices.Save(fileStream, library);

                fileStream.Close();
            }
        }

        public Library DeserializeFromFile()
        {
            Library library;

            using (FileStream fileStream = new FileStream(Settings.Default.Libraries + "\\" + Name + ".xml", FileMode.Open))
            {
                library = XamlServices.Load(fileStream) as Library;

                fileStream.Close();
            }

            return library;
        }
    }
}
