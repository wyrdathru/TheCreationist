using GalaSoft.MvvmLight.Command;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Model;
using ProjectVoid.TheCreationist.Properties;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.Managers
{
    public class LibraryManager
    {
        public LibraryManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            SetActiveLibraryCommand = new RelayCommand<LibraryViewModel>(
                (l) => SetActiveLibrary(l),
                (l) => CanSetActiveLibrary(l));

            SaveChangesCommand = new RelayCommand<LibraryViewModel>(
                (l) => SaveChanges(l),
                (l) => CanSaveChanges(l));

            DiscardChangesCommand = new RelayCommand<LibraryViewModel>(
                (l) => DiscardChanges(l),
                (l) => CanDiscardChanges(l));

            AddSwatchCommand = new RelayCommand<LibraryViewModel>(
                (l) => AddSwatch(l),
                (l) => CanAddSwatch(l));

            RemoveSwatchCommand = new RelayCommand<LibraryViewModel>(
                (l) => RemoveSwatch(l),
                (l) => CanRemoveSwatch(l));

            CreateLibraryCommand = new RelayCommand(
                () => CreateLibrary(),
                () => CanCreateLibrary());

            DeleteLibraryCommand = new RelayCommand<LibraryViewModel>(
                (l) => DeleteLibrary(l),
                (l) => CanDeleteLibrary(l));
        }

        public MainViewModel MainViewModel { get; private set; }

        public RelayCommand<LibraryViewModel> SetActiveLibraryCommand { get; set; }

        public RelayCommand<LibraryViewModel> SaveChangesCommand { get; set; }

        public RelayCommand<LibraryViewModel> DiscardChangesCommand { get; set; }

        public RelayCommand<LibraryViewModel> AddSwatchCommand { get; set; }

        public RelayCommand<LibraryViewModel> RemoveSwatchCommand { get; set; }

        public RelayCommand CreateLibraryCommand { get; set; }

        public RelayCommand<LibraryViewModel> DeleteLibraryCommand { get; set; }

        public void SetActiveLibrary(LibraryViewModel libraryViewModel)
        {
            MainViewModel.ActiveLibrary = libraryViewModel;
        }

        private bool CanSetActiveLibrary(LibraryViewModel libraryViewModel)
        {
            if (libraryViewModel != null)
            {
                return true;
            }

            return false;
        }

        public void SaveChanges(LibraryViewModel libraryViewModel)
        {
            libraryViewModel.SerializeToFile();
            libraryViewModel.IsDirty = false;
        }

        private bool CanSaveChanges(LibraryViewModel libraryViewModel)
        {
            LibraryViewModel library = MainViewModel.ActiveLibrary;

            if (libraryViewModel != null)
            {
                library = libraryViewModel;
            }

            if (library.IsDirty)
            {
                return true;
            }

            return false;
        }

        public void DiscardChanges(LibraryViewModel libraryViewModel)
        {
            libraryViewModel.Library = libraryViewModel.DeserializeFromFile();

            libraryViewModel.Swatches.Clear();

            foreach (Swatch swatch in libraryViewModel.Library.Swatches)
            {
                libraryViewModel.Swatches.Add(new SwatchViewModel(MainViewModel, swatch));
            }

            libraryViewModel.IsDirty = false;
        }

        private bool CanDiscardChanges(LibraryViewModel libraryViewModel)
        {
            LibraryViewModel library = MainViewModel.ActiveLibrary;

            if (libraryViewModel != null)
            {
                library = libraryViewModel;
            }

            if (library.IsDirty)
            {
                return true;
            }

            return false;
        }

        public void AddSwatch(LibraryViewModel libraryViewModel)
        {
            Color color = ColorUtility.ConvertColorFromString(MainViewModel.WindowManager.PaletteViewModel.NewSwatchValue);

            if (libraryViewModel.Swatches.Where(s => s.Color == color).Any())
            {
                return;
            }

            libraryViewModel.Swatches.Add(new SwatchViewModel(MainViewModel, new Swatch(color)));

            libraryViewModel.IsDirty = true;
        }

        private bool CanAddSwatch(LibraryViewModel libraryViewModel)
        {
            return true;
        }

        public void RemoveSwatch(LibraryViewModel libraryViewModel)
        {
            for (int i = libraryViewModel.Swatches.Count - 1; i > -1; i--)
            {
                if (libraryViewModel.Swatches[i].IsSelected)
                {
                    libraryViewModel.Library.Swatches.Remove(libraryViewModel.Swatches[i].Swatch);

                    libraryViewModel.Swatches.Remove(libraryViewModel.Swatches[i]);
                }
            }

            libraryViewModel.IsDirty = true;
        }

        private bool CanRemoveSwatch(LibraryViewModel libraryViewModel)
        {
            LibraryViewModel library = MainViewModel.ActiveLibrary;

            if (libraryViewModel != null)
            {
                library = libraryViewModel;
            }

            if (library.Swatches.Where(s => s.IsSelected).Any())
            {
                return true;
            }

            return false;
        }

        private void CreateLibrary()
        {
            Library library = new Library();

            LibraryViewModel libraryViewModel = new LibraryViewModel(MainViewModel, library);

            MainViewModel.Libraries.Add(libraryViewModel);

            libraryViewModel.SerializeToFile();
        }

        private bool CanCreateLibrary()
        {
            return true;
        }

        private void DeleteLibrary(LibraryViewModel libraryViewModel)
        {
            MainViewModel.Libraries.Remove(libraryViewModel);

            try
            {
                File.Delete(Settings.Default.Libraries + "\\" + libraryViewModel.Name + ".xml");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Exception DeleteFailed", ex);
            }
        }

        private bool CanDeleteLibrary(LibraryViewModel libraryViewModel)
        {
            return true;
        }
    }
}
