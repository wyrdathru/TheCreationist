
using TheCreationist.App.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace TheCreationist.App.View
{
    public partial class LibraryManagementView
    {
        public LibraryManagementView()
        {
            InitializeComponent();
        }

        private void TextBlock_Drop(object sender, System.Windows.DragEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var library = textBlock.DataContext as LibraryViewModel;

            library.MainViewModel.LibraryManager.AddSelectedSwatches(library.MainViewModel.WindowManager.LibraryManagementViewModel.ActiveLibrary, library);
        }

        private void SwatchView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && Keyboard.IsKeyDown(Key.LeftAlt))
            {
                var swatch = sender as SwatchView;
                var swatchViewModel = swatch.DataContext as SwatchViewModel;

                DragDrop.DoDragDrop(swatch, swatch.DataContext, DragDropEffects.Copy);

                e.Handled = true;
            }
        }
    }
}