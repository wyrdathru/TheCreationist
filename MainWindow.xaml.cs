using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events
        private void myMainWindow_Initialized(object sender, EventArgs e)
        {
            try
            {
                ReadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            workspaceMyRichTextBox.Focus();
        }

        private void myMainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("WARNING! Are you sure you wish to quit? You will lose unsaved work.", "Quit Project", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void favouritesWrapPanel_Initialized(object sender, EventArgs e)
        {
            PopulateFavourites();
        }

        private void newMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("WARNING! Are you sure you wish to start a new project? You will lose unsaved work.", "New Project", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                workspaceMyRichTextBox.Document.Blocks.Clear();
                myMainWindow.Title = "TheCreationist";
            }
        }

        private void openMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextRange range;
                range = new TextRange(workspaceMyRichTextBox.Document.ContentStart, workspaceMyRichTextBox.Document.ContentEnd);

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = ".rtf";
                dlg.Filter = "Rich Text Format|*.rtf";
                dlg.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];

                if (dlg.ShowDialog() == true)
                {
                    string filename = dlg.FileName;
                    FileStream fStream = new FileStream(filename, FileMode.Open);
                    range.Load(fStream, DataFormats.Rtf);
                    myMainWindow.Title = String.Format("TheCreationist - {0}", filename);
                    fStream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                TextRange range;
                range = new TextRange(workspaceMyRichTextBox.Document.ContentStart, workspaceMyRichTextBox.Document.ContentEnd);

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".rtf";
                dlg.Filter = "Rich Text Format|*.rtf";
                dlg.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];
                dlg.Title = "Save";

                if (dlg.ShowDialog() == true)
                {
                    string filename = dlg.FileName;
                    FileStream fStream = new FileStream(filename, FileMode.Create);
                    range.Save(fStream, DataFormats.Rtf);
                    myMainWindow.Title = String.Format("TheCreationist - {0}", filename);
                    fStream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void quitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("WARNING! Are you sure you wish to quit? You will lose unsaved work.", "Quit Project", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void convertMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Convert();
        }

        private void compileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Compile();
        }

        private void toggleSpellcheckMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (toggleSpellcheckMenuItem.IsChecked)
            {
                workspaceMyRichTextBox.SpellCheck.IsEnabled = true;
                config.AppSettings.Settings["SpellCheck"].Value = "true";
            }
            else
            {
                workspaceMyRichTextBox.SpellCheck.IsEnabled = false;
                config.AppSettings.Settings["SpellCheck"].Value = "false";
            }

            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void defaultRepositoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browser = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = browser.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["InitialDirectory"].Value = browser.SelectedPath;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void keepTextSelectedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (keepTextSelectedMenuItem.IsChecked)
            {
                keepTextSelectedMenuItem.IsChecked = true;
                config.AppSettings.Settings["KeepTextSelected"].Value = "true";
            }
            else
            {
                keepTextSelectedMenuItem.IsChecked = false;
                config.AppSettings.Settings["KeepTextSelected"].Value = "false";
            }

            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void ansiCodeXMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ansiCodeXMenuItem.IsChecked)
            {
                ansiCodeCMenuItem.IsChecked = false;
                ansiCodeXMenuItem.IsChecked = true;
                config.AppSettings.Settings["AnsiCode"].Value = "%x";
            }
            else
            {
                ansiCodeXMenuItem.IsChecked = true;
            }

            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void ansiCodeCMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ansiCodeCMenuItem.IsChecked)
            {
                ansiCodeXMenuItem.IsChecked = false;
                ansiCodeCMenuItem.IsChecked = true;
                config.AppSettings.Settings["AnsiCode"].Value = "%c";
            }
            else
            {
                ansiCodeCMenuItem.IsChecked = true;
            }

            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void restoreDefaultsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("WARNING! Are you sure you wish to restore defaults? This will reset favourites and change all colours. It will not clear current data, however.", "Reset Defaults", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                ResetDefaults();
            }
        }

        private void mnuStripColour_Click(object sender, RoutedEventArgs e)
        {
            StripColour();
        }

        private void aboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void configMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenConfigurationLocation();
        }

        private void btnDefaultColour_Click(object sender, RoutedEventArgs e)
        {
            StripColour();

            foregroundColourRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
            backgroundColourRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));

            workspaceMyRichTextBox.myForeColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
            workspaceMyRichTextBox.myBackColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));

            workspaceMyRichTextBox.Focus();
        }

        private void swatch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    CreatePalette(sender);
                }
                else
                {
                    ColourForeground(sender);
                    e.Handled = true;
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    CreatePalette(sender);
                }
                else
                {
                    ColourBackground(sender);
                    e.Handled = true;
                }
            }
            else if (e.ChangedButton == MouseButton.Middle)
            {
                CreatePalette(sender);
            }

            workspaceMyRichTextBox.Focus();
        }

        private void workspaceMyRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string myColor = workspaceMyRichTextBox.Selection.GetPropertyValue(TextBlock.ForegroundProperty).ToString();

                swatchAnalyzerRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(myColor));
                swatchAnalyzerLabel.Content = String.Format("#{0}", myColor.Substring(3, 6));
            }
            catch
            {
                //
            }
        }

        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string colour = "#BBBBAA";
            int count = Int32.Parse(ConfigurationManager.AppSettings["FavouriteSwatchQuantity"]);

            if ((e.ChangedButton == MouseButton.Left | e.ChangedButton == MouseButton.Right && Keyboard.IsKeyDown(Key.LeftCtrl) | Keyboard.IsKeyDown(Key.RightCtrl)) | e.ChangedButton == MouseButton.Middle)
            {
                ChangeColour(sender);

                if (sender == foregroundColourRectangle)
                {
                    workspaceMyRichTextBox.myForeColour = foregroundColourRectangle.Fill;
                    
                }
                else if (sender == backgroundColourRectangle)
                {
                    workspaceMyRichTextBox.myBackColour = backgroundColourRectangle.Fill;
                }
                else if (sender == workspaceBackgroundRectangle)
                {
                    workspaceMyRichTextBox.Background = workspaceBackgroundRectangle.Fill;
                    workspaceMyRichTextBox.myBackColour = workspaceBackgroundRectangle.Fill;
                    backgroundColourRectangle.Fill = workspaceBackgroundRectangle.Fill;

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["WorkspaceBackground"].Value = String.Format("#{0}", workspaceBackgroundRectangle.Fill.ToString().Substring(3, 6));
                    config.AppSettings.Settings["FontBackground"].Value = String.Format("#{0}", workspaceBackgroundRectangle.Fill.ToString().Substring(3, 6));
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }

                CreateNewSwatch(((Rectangle)sender).Fill.ToString());
                count++;

                Configuration config2 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config2.AppSettings.Settings["FavouriteSwatchQuantity"].Value = count.ToString();
                config2.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void fontFamilyComboBox_Initialized(object sender, EventArgs e)
        {
            List<string> systemFonts = new List<string>();

            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                systemFonts.Add(fontFamily.ToString());
            }

            systemFonts.Sort();

            foreach (string fontFamily in systemFonts)
            {
                ComboBoxItem font = new ComboBoxItem();
                font.Content = fontFamily.ToString();

                font.Selected += new RoutedEventHandler(fontFamilyComboBoxItem_Selected);

                myMainWindow.fontFamilyComboBox.Items.Add(font);
            }

            myMainWindow.fontFamilyComboBoxItem.Content = ConfigurationManager.AppSettings["FontFamily"].ToString();
        }

        private void fontFamilyComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            string newFont = ((ComboBoxItem)sender).Content.ToString();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["FontFamily"].Value = newFont;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            workspaceMyRichTextBox.FontFamily = (FontFamily)new FontFamilyConverter().ConvertFromString(newFont);
            workspaceMyRichTextBox.Focus();
        }

        private void fontSizeComboBox_Initialized(object sender, EventArgs e)
        {
            for (int i = 8; i < 31; i++)
            {
                ComboBoxItem size = new ComboBoxItem();
                size.Content = i.ToString();

                size.Selected += new RoutedEventHandler(fontSizeComboBoxItem_Selected);

                myMainWindow.fontSizeComboBox.Items.Add(size);
            }

            myMainWindow.fontSizeComboBoxItem.Content = ConfigurationManager.AppSettings["FontSize"].ToString();
        }

        private void fontSizeComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            int newSize = Int32.Parse(((ComboBoxItem)sender).Content.ToString());

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["FontSize"].Value = newSize.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            workspaceMyRichTextBox.FontSize = (double)newSize;
            workspaceMyRichTextBox.Focus();
        }

        private void stripButton_Initialized(object sender, EventArgs e)
        {
            Image stripper = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/TheCreationist;component/Images/Stripper.ico");
            bitmap.EndInit();
            stripper.Source = bitmap;

            if (ConfigurationManager.AppSettings["StripEnabled"].ToString() == "true")
            {
                stripButtonImage.Source = stripper.Source;
            }
            else
            {
                //
            }
        }

        private void statusBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (MessageBox.Show(String.Format("You are about to add the text '{0}' to your clipboard. Are you sure?", swatchAnalyzerLabel.Content.ToString()), "Clipboard", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    Clipboard.SetText(swatchAnalyzerLabel.Content.ToString());
                }
            }
            catch
            {
                //Do nothing!
            }
        }

        private void favouritesGroupBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("How many favourite swatches do you wish?", "Favourites");

            try
            {
                if (Int32.Parse(input) <= 0)
                {
                    MessageBox.Show("Please specify a positive integar that is greater than zero.");
                }
                else if (Int32.Parse(input) > 0 && Int32.Parse(input) <= 100)
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["FavouriteSwatchQuantity"].Value = input;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
                else if (Int32.Parse(input) > 100)
                {
                    if (MessageBox.Show("Are you sure you want to continue? This may take some time to load.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["FavouriteSwatchQuantity"].Value = input;
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                }
            }
            catch (FormatException)
            {
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            PopulateFavourites();
        }
        #endregion

        #region Functions
        private void ResetDefaults()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Clear();

            config.AppSettings.Settings.Add("WorkspaceBackground", "#010101");
            config.AppSettings.Settings.Add("FontForeground", "#BBBBBA");
            config.AppSettings.Settings.Add("FontBackground", "#010101");
            config.AppSettings.Settings.Add("FontSize", "12");
            config.AppSettings.Settings.Add("FontFamily", "Courier New");
            config.AppSettings.Settings.Add("SpellCheck", "true");
            config.AppSettings.Settings.Add("KeepTextSelected", "true");
            config.AppSettings.Settings.Add("AnsiCode", "%x");
            config.AppSettings.Settings.Add("StripEnabled", "false");
            config.AppSettings.Settings.Add("InitialDirectory", "");
            config.AppSettings.Settings.Add("FavouriteSwatchQuantity", "16");
            config.AppSettings.Settings.Add("Swatch0", "#FFFFFF");
            config.AppSettings.Settings.Add("Swatch1", "#BBBBBB");
            config.AppSettings.Settings.Add("Swatch2", "#FF0000");
            config.AppSettings.Settings.Add("Swatch3", "#BB0000");
            config.AppSettings.Settings.Add("Swatch4", "#FFFF55");
            config.AppSettings.Settings.Add("Swatch5", "#BBBB00");
            config.AppSettings.Settings.Add("Swatch6", "#55FF55");
            config.AppSettings.Settings.Add("Swatch7", "#00BB00");
            config.AppSettings.Settings.Add("Swatch8", "#55FFFF");
            config.AppSettings.Settings.Add("Swatch9", "#00BBBB");
            config.AppSettings.Settings.Add("Swatch10", "#5555FF");
            config.AppSettings.Settings.Add("Swatch11", "#0000BB");
            config.AppSettings.Settings.Add("Swatch12", "#FF55FF");
            config.AppSettings.Settings.Add("Swatch13", "#800080");
            config.AppSettings.Settings.Add("Swatch14", "#555555");
            config.AppSettings.Settings.Add("Swatch15", "#000000");

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            favouritesWrapPanel.Children.Clear();

            ResetColours();
            ResetPreferences();
            PopulateFavourites();
        }

        private void PopulateFavourites()
        {
            favouritesWrapPanel.Children.Clear();

            for (int i = 0; i < Int32.Parse(ConfigurationManager.AppSettings["FavouriteSwatchQuantity"]); i++)
            {
                Swatch swatch = new Swatch(50, 50, new Thickness(1,0,0,0));
                swatch.Name = String.Format("Swatch{0}", i);

                if (ConfigurationManager.AppSettings[String.Format("Swatch{0}", i)] == null)
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings.Add(String.Format("Swatch{0}", i), ConfigurationManager.AppSettings["FontForeground"]);
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }

                swatch.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings[String.Format("Swatch{0}", i)].ToString()));
                swatch.ToolTip = swatch.Background.ToString().Substring(3, 6).Insert(0, "#");

                swatch.PreviewMouseDown += new MouseButtonEventHandler(swatch_PreviewMouseDown);

                favouritesWrapPanel.Children.Add(swatch);
            }
        }

        private void CreatePalette(object sender)
        {
            Palette palette = new Palette(sender);
            palette.ShowDialog();
        }

        private void ColourForeground(object sender)
        {
            Brush colour = ((Swatch)sender).Background;
            workspaceMyRichTextBox.myForeColour = colour;
            foregroundColourRectangle.Fill = colour;

            if (!workspaceMyRichTextBox.Selection.IsEmpty)
            {
                workspaceMyRichTextBox.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, colour);
            }

            if (!keepTextSelectedMenuItem.IsChecked)
            {
                workspaceMyRichTextBox.Selection.Select(workspaceMyRichTextBox.Selection.End, workspaceMyRichTextBox.Selection.End);
            }

            workspaceMyRichTextBox.Focus();
        }

        private void ColourBackground(object sender)
        {
            Brush colour = ((Swatch)sender).Background;
            workspaceMyRichTextBox.myBackColour = colour;
            backgroundColourRectangle.Fill = colour;

            if (!workspaceMyRichTextBox.Selection.IsEmpty)
            {
                workspaceMyRichTextBox.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, colour);
            }

            if (!keepTextSelectedMenuItem.IsChecked)
            {
                workspaceMyRichTextBox.Selection.Select(workspaceMyRichTextBox.Selection.End, workspaceMyRichTextBox.Selection.End);
            }

            workspaceMyRichTextBox.Focus();
        }

        private void StripColour()
        {
            if (!workspaceMyRichTextBox.Selection.IsEmpty)
            {
                StringBuilder text = new StringBuilder();

                text.Append(workspaceMyRichTextBox.Selection.Text);

                workspaceMyRichTextBox.Selection.Text = text.ToString();

                workspaceMyRichTextBox.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"])));
                workspaceMyRichTextBox.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"])));
            }

            if (!keepTextSelectedMenuItem.IsChecked)
            {
                workspaceMyRichTextBox.Selection.Select(workspaceMyRichTextBox.Selection.End, workspaceMyRichTextBox.Selection.End);
            }

            workspaceMyRichTextBox.Focus();
        }

        private void ChangeColour(object sender)
        {
            CreatePalette(sender);
        }

        private void Convert()
        {
            TextRange workSpace = new TextRange(workspaceMyRichTextBox.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward), workspaceMyRichTextBox.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward));

            StringBuilder convertedContent = new StringBuilder();
            StringBuilder charCollection = new StringBuilder();

            object lastForegroundColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
            object lastBackgroundColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));

            workspaceMyRichTextBox.Deselect();

            for (int i = 0; i < workSpace.Text.Length; i++)
            {

                EditingCommands.SelectRightByCharacter.Execute(null, workspaceMyRichTextBox);

                object foregroundColour = workspaceMyRichTextBox.Selection.GetPropertyValue(TextBlock.ForegroundProperty);
                object backgroundColour = workspaceMyRichTextBox.Selection.GetPropertyValue(TextBlock.BackgroundProperty);

                if (backgroundColour == null)
                {
                    backgroundColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));
                }

                if (foregroundColour.ToString() == lastForegroundColour.ToString() && backgroundColour.ToString() == lastBackgroundColour.ToString())
                {
                    //
                }

                if (foregroundColour.ToString() == lastForegroundColour.ToString() && backgroundColour.ToString() != lastBackgroundColour.ToString())
                {
                    charCollection.Append(ConfigurationManager.AppSettings["AnsiCode"].ToString().ToUpper() + "<#" + backgroundColour.ToString().Substring(3, 6) + ">");
                }

                if (foregroundColour.ToString() != lastForegroundColour.ToString() && backgroundColour.ToString() == lastBackgroundColour.ToString())
                {
                    charCollection.Append(ConfigurationManager.AppSettings["AnsiCode"].ToString() + "<#" + foregroundColour.ToString().Substring(3, 6) + ">");
                }

                if (foregroundColour.ToString() != lastForegroundColour.ToString() && backgroundColour.ToString() != lastBackgroundColour.ToString())
                {
                    if (foregroundColour.ToString().Substring(3, 6) == "BBBBBA" || backgroundColour.ToString().Substring(3, 6) == "010101")
                    {
                        charCollection.Append(ConfigurationManager.AppSettings["AnsiCode"].ToString() + "n");
                    }
                    else
                    {
                        charCollection.Append(ConfigurationManager.AppSettings["AnsiCode"].ToString() + "<#" + foregroundColour.ToString().Substring(3, 6) + ">");
                        charCollection.Append(ConfigurationManager.AppSettings["AnsiCode"].ToString().ToUpper() + "<#" + backgroundColour.ToString().Substring(3, 6) + ">");
                    }
                }

                switch (workspaceMyRichTextBox.Selection.Text)
                {
                    case "\r\n":
                        charCollection.Append("%r");
                        i++;
                        break;

                    case "\\":
                        charCollection.Append("%\\");
                        break;

                    case "%":
                        charCollection.Append("%%");
                        break;

                    case "{":
                        charCollection.Append("%{");
                        break;

                    case "}":
                        charCollection.Append("%}");
                        break;

                    case "[":
                        charCollection.Append("%[");
                        break;

                    case "]":
                        charCollection.Append("%]");
                        break;

                    case "\t":
                        charCollection.Append("%t");
                        break;

                    case " ":
                        EditingCommands.SelectRightByCharacter.Execute(null, workspaceMyRichTextBox);

                        if (workspaceMyRichTextBox.Selection.Text == "  ")
                        {
                            while (workspaceMyRichTextBox.Selection.Text.EndsWith(" "))
                            {
                                i++;
                                EditingCommands.SelectRightByCharacter.Execute(null, workspaceMyRichTextBox);
                            }

                            EditingCommands.SelectLeftByCharacter.Execute(null, workspaceMyRichTextBox);
                            charCollection.Append("[space(" + workspaceMyRichTextBox.Selection.Text.Length + ")]");
                        }
                        else
                        {
                            EditingCommands.SelectLeftByCharacter.Execute(null, workspaceMyRichTextBox);
                            charCollection.Append(workspaceMyRichTextBox.Selection.Text);
                        }
                        break;

                    default:
                        charCollection.Append(workspaceMyRichTextBox.Selection.Text);
                        break;
                }

                EditingCommands.MoveRightByCharacter.Execute(null, workspaceMyRichTextBox);

                lastForegroundColour = foregroundColour;
                lastBackgroundColour = backgroundColour;
            }

            workSpace.Text = charCollection.ToString();
            workSpace.ClearAllProperties();
        }

        /*private void Compile()
        {
            TextRange workSpace = new TextRange(workspaceMyRichTextBox.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward), workspaceMyRichTextBox.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward));

            MyRichTextBox clone = new MyRichTextBox();
            TextRange clonedSpace = new TextRange(clone.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward), clone.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward));
            
            workspaceMyRichTextBox.Deselect();

            for (int i = 0; i < workSpace.Text.Length; i++)
            {
                EditingCommands.SelectRightByCharacter.Execute(null, workspaceMyRichTextBox);

                switch (workspaceMyRichTextBox.Selection.Text)
                {
                    case "%":
                        EditingCommands.SelectRightByCharacter.Execute(null, workspaceMyRichTextBox);

                        switch (workspaceMyRichTextBox.Selection.Text)
                        {
                            case "%r":
                                clone.AppendText(Environment.NewLine);
                                break;

                            case "%t":
                                clone.AppendText("\t");
                                break;

                            case "%\\":
                                clone.AppendText("\\");
                                break;

                            case "%%":
                                clone.AppendText("%");
                                break;

                            case "%{":
                                clone.AppendText("{");
                                break;

                            case "%}":
                                clone.AppendText("}");
                                break;

                            case "%[":
                                clone.AppendText("[");
                                break;

                            case "%]":
                                clone.AppendText("]");
                                break;

                            case "%b":
                                clone.AppendText(" ");
                                break;
                        }

                        i++;
                        break;

                    default:
                        clone.AppendText(workspaceMyRichTextBox.Selection.Text);
                        break;
                }

                EditingCommands.MoveRightByCharacter.Execute(null, workspaceMyRichTextBox);
            }

            workSpace.Text = clonedSpace.Text();
            workSpace.ClearAllProperties();
        }
         */

        private void ReadConfig()
        {
            //workspaceMyRichTextBox
            this.workspaceMyRichTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBBBBA"));
            this.workspaceMyRichTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["WorkspaceBackground"]));
            this.workspaceMyRichTextBox.myForeColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
            this.workspaceMyRichTextBox.myBackColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));
            this.workspaceMyRichTextBox.FontSize = (Double)new FontSizeConverter().ConvertFromString(ConfigurationManager.AppSettings["FontSize"]);
            this.workspaceMyRichTextBox.FontFamily = (FontFamily)new FontFamilyConverter().ConvertFromString(ConfigurationManager.AppSettings["FontFamily"]);
            this.workspaceMyRichTextBox.SpellCheck.IsEnabled = (Boolean)new BooleanConverter().ConvertFromString(ConfigurationManager.AppSettings["SpellCheck"]);

            //foregroundColourRectangle
            this.foregroundColourRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));

            //backgroundColourRectangle
            this.backgroundColourRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));

            //workspaceBackgroundRectangle
            this.workspaceBackgroundRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["WorkspaceBackground"]));

            //keepTextSelectedMenuItem
            this.keepTextSelectedMenuItem.IsChecked = (Boolean)new BooleanConverter().ConvertFromString(ConfigurationManager.AppSettings["KeepTextSelected"]);

            //toggleSpellcheckMenuItem
            this.toggleSpellcheckMenuItem.IsChecked = (Boolean)new BooleanConverter().ConvertFromString(ConfigurationManager.AppSettings["SpellCheck"]);

            //ansiCodeCMenuItem
            if (ConfigurationManager.AppSettings["AnsiCode"].Equals("%c"))
            {
                this.ansiCodeCMenuItem.IsChecked = true;
            }

            //ansiCodeXMenuItem
            if (ConfigurationManager.AppSettings["AnsiCode"].Equals("%x"))
            {
                this.ansiCodeXMenuItem.IsChecked = true;
            }
        }

        private void ResetColours()
        {
            //Reset the font forecolour, font backcolour and workspace background rectangles.
            foregroundColourRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
            backgroundColourRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));
            workspaceBackgroundRectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["WorkspaceBackground"]));

            //Reset the current properties of the RichTextBox.
            workspaceMyRichTextBox.myForeColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontForeground"]));
            workspaceMyRichTextBox.myBackColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["FontBackground"]));
            workspaceMyRichTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["WorkspaceBackground"]));
        }

        private void ResetPreferences()
        {
            workspaceMyRichTextBox.FontFamily = (FontFamily)new FontFamilyConverter().ConvertFromString("Courier New");
            workspaceMyRichTextBox.FontSize = 12;
            fontFamilyComboBoxItem.Content = "Courier New";
            fontSizeComboBoxItem.Content = "12";
            ansiCodeCMenuItem.IsChecked = false;
            ansiCodeXMenuItem.IsChecked = true;
            keepTextSelectedMenuItem.IsChecked = true;
            toggleSpellcheckMenuItem.IsChecked = true;
        }

        private void OpenConfigurationLocation()
        {
            Process.Start(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
        }

        private void CreateNewSwatch(string colour)
        {
            int i = 0;

            while (ConfigurationManager.AppSettings[String.Format("Swatch{0}", i)] != null)
            {
                i++;
            }

            Swatch swatch = new Swatch(50, 50, colour, new Thickness(1,0,0,0));
            swatch.Name = "Swatch" + i.ToString();
            swatch.Content = "";

            swatch.PreviewMouseDown += new MouseButtonEventHandler(swatch_PreviewMouseDown);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(swatch.Name, colour.Substring(3, 6).Insert(0, "#"));
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            favouritesWrapPanel.Children.Add(swatch);
        }
        #endregion
    }
}