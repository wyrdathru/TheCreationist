using System.Configuration;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

/* The MIT License (MIT)

Copyright (c) 2013 Kyle Wernham

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

namespace ProjectVoid.TheCreationist
{
    public class MyRichTextBox : RichTextBox
    {
        public Brush myForeColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
        public Brush myBackColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));

        public MyRichTextBox()
            : base()
        {
            //
        }

        /* This event colours each character that the user inputs is using the selected colours. These can change from their configured defaults during runtime.  */
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            EditingCommands.SelectLeftByCharacter.Execute(null, this);
            this.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, myForeColour);
            this.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, myBackColour);
            EditingCommands.MoveRightByCharacter.Execute(null, this);
            e.Handled = true;
        }

        public void SelectCharByX(int x, string direction)
        {
            for (int i = 0; i < x; i++)
            {
                if (direction.ToLower() == "right")
                {
                    EditingCommands.SelectRightByCharacter.Execute(null, this);
                }
                else if (direction.ToLower() == "left")
                {
                    EditingCommands.SelectLeftByCharacter.Execute(null, this);
                }
                else
                {
                    //
                }
            }
        }

        public void Deselect()
        {
            this.Selection.Select(this.Document.ContentStart, this.Document.ContentStart);
        }
    }
}