using GalaSoft.MvvmLight.Command;
using ProjectVoid.TheCreationist.View;
using ProjectVoid.TheCreationist.ViewModel;
using System.Windows;

namespace ProjectVoid.TheCreationist
{
    public class WindowManager
    {
        public WindowManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            OptionsViewModel = new OptionsViewModel(MainViewModel);
            AboutViewModel = new AboutViewModel(MainViewModel);

            DisplayOptionsCommand = new RelayCommand(
                () => DisplayOptions(),
                () => CanDisplayOptions());

            DisplayAboutCommand = new RelayCommand(
                () => DisplayAbout(),
                () => CanDisplayAbout());
        }

        public MainViewModel MainViewModel { get; private set; }

        public OptionsViewModel OptionsViewModel { get; private set; }

        public AboutViewModel AboutViewModel { get; private set; }

        public RelayCommand DisplayOptionsCommand { get; private set; }

        public RelayCommand DisplayAboutCommand { get; private set; }

        private void DisplayOptions()
        {
            OptionsView optionsView = new OptionsView();

            optionsView.DataContext = OptionsViewModel;
            optionsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            optionsView.Owner = Application.Current.MainWindow;

            optionsView.ShowDialog();
        }

        private bool CanDisplayOptions()
        {
            return true;
        }

        private void DisplayAbout()
        {
            AboutView aboutView = new AboutView();

            aboutView.DataContext = AboutViewModel;
            aboutView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutView.Owner = Application.Current.MainWindow;

            aboutView.ShowDialog();
        }

        private bool CanDisplayAbout()
        {
            return true;
        }
    }
}
