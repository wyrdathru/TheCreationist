using GalaSoft.MvvmLight.Command;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Model;
using ProjectVoid.TheCreationist.Properties;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.Managers
{
    public class LibraryManager : IDisposable
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
            Logger.Log.Debug("Setting");

            MainViewModel.ActiveLibrary = libraryViewModel;

            Logger.Log.Debug("Set");
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
            Logger.Log.Debug("Saving");

            MainViewModel.SerializeToFile(libraryViewModel.Library);
            libraryViewModel.IsDirty = false;

            Logger.Log.Debug("Saved");
        }

        private bool CanSaveChanges(LibraryViewModel libraryViewModel)
        {
            if (libraryViewModel == null)
            {
                return false;
            }

            LibraryViewModel library = MainViewModel.ActiveLibrary;

            library = libraryViewModel;

            if (library.IsDirty)
            {
                return true;
            }

            return false;
        }

        public void DiscardChanges(LibraryViewModel libraryViewModel)
        {
            Logger.Log.Debug("Discarding");

            libraryViewModel.Library = MainViewModel.DeserializeFromFile(libraryViewModel.Name);

            libraryViewModel.Swatches.Clear();

            foreach (Swatch swatch in libraryViewModel.Library.Swatches)
            {
                libraryViewModel.Swatches.Add(new SwatchViewModel(MainViewModel, swatch));
            }

            libraryViewModel.IsDirty = false;

            Logger.Log.Debug("Discarded");
        }

        private bool CanDiscardChanges(LibraryViewModel libraryViewModel)
        {
            if (libraryViewModel == null)
            {
                return false;
            }

            LibraryViewModel library = MainViewModel.ActiveLibrary;

            library = libraryViewModel;

            if (library.IsDirty)
            {
                return true;
            }

            return false;
        }

        public void AddSwatch(LibraryViewModel libraryViewModel)
        {
            Logger.Log.Debug("Adding");

            Color color = ColorUtility.ConvertColorFromString(MainViewModel.WindowManager.LibraryManagementViewModel.NewSwatchValue);

            if (libraryViewModel.Swatches.Where(s => s.Color == color).Any())
            {
                //MessageBox.Show(String.Format("{0} already exists in {1}", color.ToString().Substring(3).Insert(0, "#"), libraryViewModel.Name));
                return;
            }

            var swatch = new Swatch(color);
            var swatchViewModel = new SwatchViewModel(MainViewModel, swatch);

            libraryViewModel.Library.Swatches.Add(swatch);

            libraryViewModel.Swatches.Add(swatchViewModel);

            libraryViewModel.IsDirty = true;

            Logger.Log.DebugFormat("Added Color[{0}]", swatchViewModel.Color);
        }

        private bool CanAddSwatch(LibraryViewModel libraryViewModel)
        {
            return true;
        }

        public void AddSelectedSwatches(LibraryViewModel fromLibrary, LibraryViewModel toLibrary)
        {
            Logger.Log.Debug("Adding");

            for (int i = fromLibrary.Swatches.Count - 1; i > -1; i--)
            {
                if (fromLibrary.Swatches[i].IsSelected)
                {
                    if (toLibrary.Swatches.Where(s => s.Color == fromLibrary.Swatches[i].Color).Any())
                    {
                        //MessageBox.Show(String.Format("{0} already exists in {1}", fromLibrary.Swatches[i].Color.ToString().Substring(3).Insert(0, "#"), toLibrary.Name));
                        continue;
                    }

                    var swatch = new Swatch(fromLibrary.Swatches[i].Color);
                    var swatchViewModel = new SwatchViewModel(MainViewModel, swatch);

                    toLibrary.Library.Swatches.Add(swatch);

                    toLibrary.Swatches.Add(swatchViewModel);
                }
            }

            toLibrary.IsDirty = true;
        }


        public void RemoveSwatch(LibraryViewModel libraryViewModel)
        {
            Logger.Log.Debug("Removing");

            for (int i = libraryViewModel.Swatches.Count - 1; i > -1; i--)
            {
                if (libraryViewModel.Swatches[i].IsSelected)
                {
                    libraryViewModel.Library.Swatches.Remove(libraryViewModel.Swatches[i].Swatch);

                    libraryViewModel.Swatches.Remove(libraryViewModel.Swatches[i]);
                }
            }

            libraryViewModel.IsDirty = true;

            Logger.Log.DebugFormat("Removed ID[{0}] Name[{1}]", libraryViewModel.Id, libraryViewModel.Name);
        }

        private bool CanRemoveSwatch(LibraryViewModel libraryViewModel)
        {
            if (libraryViewModel == null)
            {
                return false;
            }

            LibraryViewModel library = MainViewModel.ActiveLibrary;

            library = libraryViewModel;

            if (library.Swatches.Where(s => s.IsSelected).Any())
            {
                return true;
            }

            return false;
        }

        private void CreateLibrary()
        {
            Logger.Log.Debug("Creating");

            Library library = new Library();

            LibraryViewModel libraryViewModel = new LibraryViewModel(MainViewModel, library);

            MainViewModel.Libraries.Add(libraryViewModel);
            MainViewModel.WindowManager.LibraryManagementViewModel.ActiveLibrary = libraryViewModel;

            MainViewModel.SerializeToFile(libraryViewModel.Library);

            Logger.Log.DebugFormat("Created ID[{0}] Name[{1}]", libraryViewModel.Id, libraryViewModel.Name);
        }

        private bool CanCreateLibrary()
        {
            return true;
        }

        private void DeleteLibrary(LibraryViewModel libraryViewModel)
        {
            Logger.Log.Debug("Deleting");

            MainViewModel.Libraries.Remove(libraryViewModel);

            if (MainViewModel.ActiveLibrary.Equals(libraryViewModel))
            {
                MainViewModel.ActiveLibrary = MainViewModel.Libraries.FirstOrDefault();
            }

            try
            {
                File.Delete(Settings.Default.Libraries.Replace("${USERPROFILE}", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) + "\\" + libraryViewModel.Name + ".xml");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Delete Exception", ex);
            }

            Logger.Log.DebugFormat("Deleted ID[{0}] Name[{1}]", libraryViewModel.Id, libraryViewModel.Name);
        }

        private bool CanDeleteLibrary(LibraryViewModel libraryViewModel)
        {
            return true;
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}
