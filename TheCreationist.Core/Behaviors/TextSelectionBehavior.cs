using TheCreationist.Core.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace TheCreationist.Core.Behaviors
{
    public class TextSelectionBehavior : Behavior<BindableRichTextBox>
    {
        public TextSelection SelectedText
        {
            get { return (TextSelection)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register("SelectedText", typeof(TextSelection), typeof(TextSelectionBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextChanged));

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

        private void OnRichTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectedText = AssociatedObject.Selection;
        }

        private static void OnSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }
    }
}