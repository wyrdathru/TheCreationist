
using ProjectVoid.TheCreationist.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace ProjectVoid.TheCreationist.View
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

        private void SwatchView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var swatch = sender as SwatchView;
                DragDrop.DoDragDrop(swatch, swatch.DataContext, DragDropEffects.Move);
            }
        }
    }
}