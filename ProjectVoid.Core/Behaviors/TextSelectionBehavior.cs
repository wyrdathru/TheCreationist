using ProjectVoid.Core.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace ProjectVoid.Core.Behaviors
{
    public class TextSelectionBehavior : Behavior<BindableRichTextBox>
    {
        #region Properties

        public TextSelection SelectedText
        {
            get { return (TextSelection)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register("SelectedText", typeof(TextSelection), typeof(TextSelectionBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextChanged));

        #endregion Properties

        #region Protected Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnRichTextBoxSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnRichTextBoxSelectionChanged;
        }

        #endregion Protected Methods

        #region Private Methods

        private void OnRichTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectedText = AssociatedObject.Selection;
        }

        private static void OnSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion Private Methods
    }
}