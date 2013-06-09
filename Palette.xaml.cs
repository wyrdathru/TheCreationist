using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
    /// <summary>
    /// Interaction logic for Palette.xaml
    /// </summary>
    public partial class Palette : Window
    {
        //The swatch that called the creation of this Palette menu will be the target for the ChangeTargetColour method.
        public object _target;

        public Palette(object target)
        {
            InitializeComponent();

            _target = target;
        }

        #region Events
        private void eightBitColoursWrapPanel_Initialized(object sender, EventArgs e)
        {
            Create8BitSwatches();
        }

        private void fourBitColoursWrapPanel_Initialized(object sender, EventArgs e)
        {
            Create4BitSwatches();
        }

        private void customSwatchValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ChangeCustomSwatch();
            }
            catch
            {
                //Do nothing. Unless a hexadecimal value can be matched, it should remain as it was.
            }
        }

        private void swatch_Click(object sender, RoutedEventArgs e)
        {
            ChangeTargetColour((Swatch)sender);

            //Close the Palette menu as its purpose is complete.
            this.Close();
        }
        #endregion

        #region Functions
        private Swatch CreateSwatch(int width, int height, string AnsiColourValue)
        {
            Swatch swatch = new Swatch(width, height, AnsiColourValue);

            swatch.Click += new RoutedEventHandler(swatch_Click);

            return swatch;
        }

        private void Create4BitSwatches()
        {
            for (int i = 0; i < AnsiColours.fourBitColours.Length; i++)
            {
                fourBitColoursWrapPanel.Children.Add(CreateSwatch(50, 50, AnsiColours.fourBitColours[i].ToString()));
            }
        }

        private void Create8BitSwatches()
        {
            for (int i = 0; i < AnsiColours.eightBitColours.Length; i++)
            {
                eightBitColoursWrapPanel.Children.Add(CreateSwatch(50, 50, AnsiColours.eightBitColours[i].ToString()));
            }
        }

        private void ChangeCustomSwatch()
        {
            customSwatchButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(customSwatchValueTextBox.Text));
            customSwatchButton.ToolTip = customSwatchButton.Background.ToString().Substring(3, 6).Insert(0, "#");
        }

        private void ChangeTargetColour(Swatch sender)
        {
            if (_target is Swatch)
            {
                //Alter the properties of the swatch in the application's favourites.
                ((Swatch)_target).Background = sender.Background;
                ((Swatch)_target).ToolTip = sender.Background.ToString().Substring(3, 6).Insert(0, "#");
                
                //Amend and save app.config with the updated hexadecimal value.
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[((Swatch)_target).Name].Value = sender.Background.ToString().Substring(3, 6).Insert(0, "#");
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            else if (_target is Rectangle)
            {
                /*If the target is a rectangle, change the Fill property rather than the Background property.
                  This is for foregroundColourRectangle, backgroundColourRectangle and workspaceBackgroundRectangle. */
                ((Rectangle)_target).Fill = sender.Background;
            }
            else
            {
                //
            }
        }
        #endregion
    }
}
