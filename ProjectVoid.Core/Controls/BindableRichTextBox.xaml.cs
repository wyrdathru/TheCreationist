using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectVoid.Core.Controls
{
    public class BindableRichTextBox : RichTextBox
    {
        #region Properties

        public FlowDocument FlowDocument
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("FlowDocument", typeof(FlowDocument), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDocumentChanged)));

        public DateTime LastChanged
        {
            get { return (DateTime)GetValue(LastChangedProperty); }
            set { SetValue(LastChangedProperty, value); }
        }

        public static readonly DependencyProperty LastChangedProperty =
            DependencyProperty.Register("LastChanged", typeof(DateTime), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(null, null));

        public Color Forecolor
        {
            get { return (Color)GetValue(ForecolorProperty); }
            set { SetValue(ForecolorProperty, value); }
        }

        public static readonly DependencyProperty ForecolorProperty =
            DependencyProperty.Register("Forecolor", typeof(Color), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(Colors.Transparent, null));

        public Color Backcolor
        {
            get { return (Color)GetValue(BackcolorProperty); }
            set { SetValue(BackcolorProperty, value); }
        }

        public static readonly DependencyProperty BackcolorProperty =
            DependencyProperty.Register("Backcolor", typeof(Color), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(Colors.Transparent, null));

        #endregion Properties

        #region Public Methods

        public static void OnDocumentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue == null)
            {
                return;
            }

            BindableRichTextBox rtb = (BindableRichTextBox)obj;

            rtb.Document = (FlowDocument)args.NewValue;
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            EditingCommands.SelectLeftByCharacter.Execute(null, this);

            this.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush(Forecolor));
            this.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, new SolidColorBrush(Backcolor));

            EditingCommands.MoveRightByCharacter.Execute(null, this);

            e.Handled = true;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (e.Changes.Count > 0)
            {
                LastChanged = DateTime.Now;
            }

            e.Handled = true;
        }

        #endregion Protected Methods
    }
}