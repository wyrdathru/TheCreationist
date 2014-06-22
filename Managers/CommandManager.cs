using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ProjectVoid.Core.Helpers;
using ProjectVoid.Core.Utilities;
using ProjectVoid.TheCreationist.Enum;
using ProjectVoid.TheCreationist.Model;
using ProjectVoid.TheCreationist.Properties;
using ProjectVoid.TheCreationist.View;
using ProjectVoid.TheCreationist.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.Managers
{
    public class CommandManager : IDisposable
    {
        public CommandManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            CreateProjectCommand = new RelayCommand(
                () => CreateProject(),
                () => CanCreateProject());

            OpenProjectCommand = new RelayCommand<MainViewModel>(
                (m) => OpenProject(m),
                (m) => CanOpenProject(m));

            SaveProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => SaveProject(p),
                (p) => CanSaveProject(p));

            CloseProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => CloseProject(p),
                (p) => CanCloseProject(p));

            CloseAllProjectsCommand = new RelayCommand(
                () => CloseAllProjects(),
                () => CanCloseAllProjects());

            CloseAllProjectsExceptCommand = new RelayCommand<ProjectViewModel>(
                (p) => CloseAllProjectsExcept(p),
                (p) => CanCloseAllProjectsExcept(p));

            ConvertProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => ConvertProject(p),
                (p) => CanConvertProject(p));

            CompileProjectCommand = new RelayCommand<ProjectViewModel>(
                (p) => CompileProject(p),
                (p) => CanCompileProject(p));

            CloseWindowCommand = new RelayCommand<Window>(
                (p) => CloseWindow(p),
                (p) => CanCloseWindow(p));

            SelectSwatchCommand = new RelayCommand<MouseButtonEventArgs>(
                (e) => SelectSwatch(e),
                (e) => CanSelectSwatch(e));

            OpenLogLocationCommand = new RelayCommand(
                () => OpenLogLocation(),
                () => CanOpenLogLocation());

            StripColorsCommand = new RelayCommand(
                () => StripColors(),
                () => CanStripColors());

            ProcessColorRuleCommand = new RelayCommand<ColorRulesViewModel>(
                (r) => ProcessColorRule(r),
                (r) => CanProcessColorRule(r));
        }

        public MainViewModel MainViewModel { get; private set; }

        public RelayCommand CreateProjectCommand { get; private set; }

        public RelayCommand<MainViewModel> OpenProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> SaveProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> CloseProjectCommand { get; private set; }

        public RelayCommand CloseAllProjectsCommand { get; private set; }

        public RelayCommand<ProjectViewModel> CloseAllProjectsExceptCommand { get; private set; }

        public RelayCommand<ProjectViewModel> ConvertProjectCommand { get; private set; }

        public RelayCommand<ProjectViewModel> CompileProjectCommand { get; private set; }

        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        public RelayCommand<MouseButtonEventArgs> SelectSwatchCommand { get; private set; }

        public RelayCommand OpenLogLocationCommand { get; private set; }

        public RelayCommand StripColorsCommand { get; private set; }

        public RelayCommand<ColorRulesViewModel> ProcessColorRuleCommand { get; private set; }

        private void CreateProject()
        {
            Logger.Log.Debug("Creating");

            ProjectViewModel project = new ProjectViewModel(MainViewModel);

            MainViewModel.Projects.Add(project);

            MainViewModel.ActiveProject = project;

            Logger.Log.DebugFormat("Created ID[{0}] Name[{1}]", project.Id, project.Name);
        }

        private bool CanCreateProject()
        {
            return true;
        }

        private void OpenProject(MainViewModel mainViewModel)
        {
            Logger.Log.Debug("Opening");

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();
            openFileDialog.Multiselect = false;
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "New Project Format|*.xml|Old Project Format|*.rtf|Text Files|*.txt";

            var result = openFileDialog.ShowDialog();

            if (result == false || result == null)
            {
                Logger.Log.Debug("Aborted");
                return;
            }

            var file = openFileDialog.FileName;

            switch (Path.GetExtension(file).ToLower())
            {
                case ".rtf":
                    OpenRtfProject(mainViewModel, file);
                    break;

                case ".txt":
                    OpenTxtProject(mainViewModel, file);
                    break;

                case ".xml":
                    OpenXmlProject(mainViewModel, file);
                    break;
            }
        }

        private void OpenRtfProject(MainViewModel mainViewModel, string file)
        {
            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                ProjectViewModel projectViewModel = new ProjectViewModel(mainViewModel);
                var range = new TextRange(projectViewModel.Document.ContentStart, projectViewModel.Document.ContentEnd);

                range.Load(fileStream, DataFormats.Rtf);

                range.ApplyPropertyValue(TextBlock.FontFamilyProperty, Settings.Default.FontFamily);
                range.ApplyPropertyValue(TextBlock.FontSizeProperty, Settings.Default.FontSize);

                if (mainViewModel.Projects.Any(p => p.Project.Name.Equals(fileName)))
                {
                    var match = mainViewModel.Projects.First(p => p.Project.Name.Equals(fileName));

                    mainViewModel.ActiveProject = match;

                    if (match.State.IsDirty)
                    {
                        var canReload = MessageBox.Show(String.Format("{0} has unsaved changes, are you sure you want to reload it?", match.Name), String.Format("Reload {0}?", match.Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (canReload == MessageBoxResult.No)
                        {
                            fileStream.Close();
                            Logger.Log.Debug("Aborted ProjectAlreadyExits");
                            return;
                        }

                        MainViewModel.Projects.Remove(match);
                    }
                    else
                    {
                        fileStream.Close();
                        Logger.Log.DebugFormat("Aborted ProjectAlreadyExits");
                        return;
                    }
                }

                projectViewModel.State.IsSaved = true;
                projectViewModel.State.IsDirty = false;
                projectViewModel.Name = fileName;

                mainViewModel.Projects.Add(projectViewModel);
                mainViewModel.ActiveProject = projectViewModel;

                fileStream.Close();

                Logger.Log.DebugFormat("Opened ID[{0}] Name[{1}]", projectViewModel.Id, projectViewModel.Name);
            }
        }

        private void OpenTxtProject(MainViewModel mainViewModel, string file)
        {
            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                ProjectViewModel projectViewModel = new ProjectViewModel(mainViewModel);
                var range = new TextRange(projectViewModel.Document.ContentStart, projectViewModel.Document.ContentEnd);

                range.Load(fileStream, DataFormats.Text);

                if (mainViewModel.Projects.Any(p => p.Project.Name.Equals(fileName)))
                {
                    var match = mainViewModel.Projects.First(p => p.Project.Name.Equals(fileName));

                    mainViewModel.ActiveProject = match;

                    if (match.State.IsDirty)
                    {
                        var canReload = MessageBox.Show(String.Format("{0} has unsaved changes, are you sure you want to reload it?", match.Name), String.Format("Reload {0}?", match.Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (canReload == MessageBoxResult.No)
                        {
                            fileStream.Close();
                            Logger.Log.Debug("Aborted ProjectAlreadyExits");
                            return;
                        }

                        MainViewModel.Projects.Remove(match);
                    }
                    else
                    {
                        fileStream.Close();
                        Logger.Log.DebugFormat("Aborted ProjectAlreadyExits");
                        return;
                    }
                }

                projectViewModel.State.IsSaved = true;
                projectViewModel.State.IsDirty = false;
                projectViewModel.Name = fileName;

                mainViewModel.Projects.Add(projectViewModel);
                mainViewModel.ActiveProject = projectViewModel;

                fileStream.Close();

                Logger.Log.DebugFormat("Opened ID[{0}] Name[{1}]", projectViewModel.Id, projectViewModel.Name);
            }
        }

        private void OpenXmlProject(MainViewModel mainViewModel, string file)
        {
            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                Project project = XamlReader.Load(fileStream) as Project;

                if (mainViewModel.Projects.Any(p => p.Project.Name.Equals(project.Name)))
                {
                    var match = mainViewModel.Projects.First(p => p.Project.Name.Equals(project.Name));

                    mainViewModel.ActiveProject = match;

                    if (match.State.IsDirty)
                    {
                        var canReload = MessageBox.Show(String.Format("{0} has unsaved changes, are you sure you want to reload it?", match.Name), String.Format("Reload {0}?", match.Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (canReload == MessageBoxResult.No)
                        {
                            fileStream.Close();
                            Logger.Log.Debug("Aborted ProjectAlreadyExits");
                            return;
                        }

                        MainViewModel.Projects.Remove(match);
                    }
                    else
                    {
                        fileStream.Close();
                        Logger.Log.DebugFormat("Aborted ProjectAlreadyExits");
                        return;
                    }
                }

                var projectViewModel = new ProjectViewModel(MainViewModel, project);

                projectViewModel.State.IsSaved = true;
                projectViewModel.State.IsDirty = false;

                mainViewModel.Projects.Add(projectViewModel);
                mainViewModel.ActiveProject = projectViewModel;

                fileStream.Close();

                Logger.Log.DebugFormat("Opened ID[{0}] Name[{1}]", project.Id, project.Name);
            }
        }

        private bool CanOpenProject(MainViewModel mainViewModel)
        {
            return true;
        }

        private void SaveProject(ProjectViewModel projectViewModel)
        {
            Logger.Log.Debug("Saving");

            var project = projectViewModel.Project;

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML|*.xml";

            var result = saveFileDialog.ShowDialog();

            if (result == false || result == null)
            {
                Logger.Log.Debug("Aborted");
                return;
            }

            var file = saveFileDialog.FileName;

            projectViewModel.Name = Path.GetFileNameWithoutExtension(file);

            using (FileStream fileStream = new FileStream(file, FileMode.Create))
            {
                XamlWriter.Save(project, fileStream);

                fileStream.Close();
                Logger.Log.DebugFormat("Saved Project ID[{0}] Name[{1}]", project.Id, project.Name);
            }

            projectViewModel.State.IsSaved = true;
            projectViewModel.State.IsDirty = false;
        }

        private bool CanSaveProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

            return true;
        }

        private void CloseProject(ProjectViewModel projectViewModel)
        {
            Logger.Log.Debug("Closing");

            if (projectViewModel.State.IsDirty == true)
            {
                var result = MessageBox.Show(String.Format("{0} has unsaved changes, are you sure you want to close it?", projectViewModel.Name), String.Format("Close {0}?", projectViewModel.Name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    Logger.Log.Debug("Aborted UnsavedChangesDetected");
                    return;
                }
            }

            MainViewModel.Projects.Remove(projectViewModel);

            Logger.Log.DebugFormat("Closed ID[{0}] Name[{1}]", projectViewModel.Id, projectViewModel.Name);
        }

        private bool CanCloseProject(ProjectViewModel projectViewModel)
        {
            if (MainViewModel.Projects.Count > 0)
            {
                return true;
            }

            return false;
        }

        private void CloseAllProjects()
        {
            Logger.Log.Debug("Closing");

            for (int i = MainViewModel.Projects.Count - 1; i > -1; i--)
            {
                CloseProject(MainViewModel.Projects[i]);
            }

            Logger.Log.Debug("Closed");
        }

        private bool CanCloseAllProjects()
        {
            if (MainViewModel.Projects.Count > 0)
            {
                return true;
            }

            return false;
        }

        private void CloseAllProjectsExcept(ProjectViewModel projectViewModel)
        {
            Logger.Log.Debug("Closing");

            for (int i = MainViewModel.Projects.Count - 1; i > -1; i--)
            {
                if (MainViewModel.Projects[i].Project.Id.Equals(projectViewModel.Project.Id))
                {
                    continue;
                }

                CloseProject(MainViewModel.Projects[i]);
            }

            Logger.Log.Debug("Closed");
        }

        private bool CanCloseAllProjectsExcept(ProjectViewModel projectViewModel)
        {
            if (MainViewModel.Projects.Count > 0)
            {
                return true;
            }

            return false;
        }

        private void ConvertProject(ProjectViewModel projectViewModel)
        {
            try
            {
                Logger.Log.Debug("Converting");

                var project = projectViewModel;

                if (project.Document.Blocks.Count < 1)
                {
                    Logger.Log.Debug("Compiling Cancelled[InsufficientBlockCount]");
                    return;
                }

                Brush _DefaultForeground = ColorUtility.ConvertBrushFromString(Settings.Default.Foreground.ToString());
                Brush _DefaultBackground = ColorUtility.ConvertBrushFromString(Settings.Default.Background.ToString());

                Brush _LastForeground;
                Brush _LastBackground;

                _LastForeground = _DefaultForeground;
                _LastBackground = _DefaultBackground;

                string result = ProcessBlocks(project.Document.Blocks, _DefaultForeground, _DefaultBackground, _LastForeground, _LastBackground);

                project.Project.Document.Blocks.Clear();

                project.Project.Document.Blocks.Add(new Paragraph(new Run(result) { Foreground = _DefaultForeground, Background = _DefaultBackground }));

                Logger.Log.DebugFormat("Converted ID[{0}] Name[{1}]", projectViewModel.Id, projectViewModel.Name);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Convert Exception", ex);
            }
        }

        private string ProcessBlocks(BlockCollection blocks, Brush defaultForeground, Brush defaultBackground, Brush lastForeground, Brush lastBackground)
        {
            StringBuilder processedBlocks = new StringBuilder();

            if (blocks != null)
            {
                foreach (Block block in blocks)
                {
                    Paragraph paragraph = block as Paragraph;

                    processedBlocks.Append("%r");

                    processedBlocks.Append(ProcessInlines(paragraph.Inlines, defaultForeground, defaultBackground, lastForeground, lastBackground));
                }
            }

            if (processedBlocks.ToString().Length >= 2)
            {
                return processedBlocks.ToString().Substring(2);
            }

            return null;
        }

        private string ProcessInlines(InlineCollection inlines, Brush defaultForeground, Brush defaultBackground, Brush lastForeground, Brush lastBackground)
        {
            StringBuilder processedInlines = new StringBuilder();

            foreach (Inline inline in inlines)
            {
                Char[] chars = null;

                Brush foreground = null;

                Brush background = null;

                if (inline.GetType().Equals(typeof(Span)))
                {
                    Span span = ((Span)inline);

                    chars = new TextRange(span.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward), span.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward)).Text.ToCharArray();

                    foreground = span.Foreground;
                    background = span.Background;
                }
                else if (inline.GetType().Equals(typeof(Run)))
                {
                    Run run = ((Run)inline);

                    chars = run.Text.ToCharArray();

                    foreground = run.Foreground;
                    background = run.Background;
                }
                else if (inline.GetType().Equals(typeof(LineBreak)))
                {
                    chars = Environment.NewLine.ToString().ToCharArray();
                }
                else
                {
                    MessageBox.Show(inline.GetType().ToString());
                }

                if (foreground == null)
                {
                    foreground = defaultForeground;
                }

                if (background == null)
                {
                    background = defaultBackground;
                }

                if (foreground.ToString() == lastForeground.ToString() && background.ToString() == lastBackground.ToString())
                {
                    //
                }

                if (foreground.ToString() == lastForeground.ToString() && background.ToString() != lastBackground.ToString())
                {
                    processedInlines.Append(String.Format("%X<#{0}>", background.ToString().ToUpper().Substring(3)));
                }

                if (foreground.ToString() != lastForeground.ToString() && background.ToString() == lastBackground.ToString())
                {
                    processedInlines.Append(String.Format("%x<#{0}>", foreground.ToString().ToUpper().Substring(3)));
                }

                if (foreground.ToString() != lastForeground.ToString() && background.ToString() != lastBackground.ToString())
                {
                    if (foreground.ToString() == defaultForeground.ToString() && background.ToString() == defaultBackground.ToString())
                    {
                        processedInlines.Append(String.Format("%xn"));
                    }
                    else
                    {
                        processedInlines.Append(String.Format("%x<#{0}>%X<#{1}>", foreground.ToString().ToUpper().Substring(3), background.ToString().ToUpper().Substring(3)));
                    }
                }

                lastForeground = foreground;
                lastBackground = background;

                for (int i = 0; i < chars.Length; i++)
                {
                    switch (chars[i].ToString())
                    {
                        case "\\":
                            processedInlines.Append("%\\");
                            break;

                        case "%":
                            processedInlines.Append("%%");
                            break;

                        case "{":
                            processedInlines.Append("%{");
                            break;

                        case "}":
                            processedInlines.Append("%}");
                            break;

                        case "[":
                            processedInlines.Append("%[");
                            break;

                        case "]":
                            processedInlines.Append("%]");
                            break;

                        case "\r":
                            processedInlines.Append("%r"); i++;
                            break;

                        case "\t":
                            processedInlines.Append("%t");
                            break;

                        case " ":
                            StringBuilder spaces = new StringBuilder();

                            spaces.Append(" ");

                            while ((i + 1) < chars.Length && chars[i + 1].Equals(' '))
                            {
                                spaces.Append(" ");
                                i++;
                            }

                            if (spaces.Length == 1) { processedInlines.Append(spaces.ToString()); }
                            else
                            {
                                processedInlines.Append(String.Format("[space({0})]", spaces.Length.ToString()));
                            }

                            break;

                        default:
                            processedInlines.Append(chars[i]);
                            break;
                    }
                }
            }

            return processedInlines.ToString();
        }

        private bool CanConvertProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

            return true;
        }

        private void CompileProject(ProjectViewModel projectViewModel)
        {
            try
            {
                Logger.Log.Debug("Compiling");

                var project = projectViewModel;

                if (project.Document.Blocks.Count < 1)
                {
                    Logger.Log.Debug("Compiling Cancelled[InsufficientBlockCount]");
                    return;
                }

                DocumentBuilder document = CreateDocument(project);

                project.Project.Document.Blocks.Clear();

                if (document == null)
                {
                    return;
                }

                project.Project.Document.Blocks.AddRange(document.Blocks);

                Logger.Log.DebugFormat("Compiled ID[{0}] Name[{1}]", projectViewModel.Id, projectViewModel.Name);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Compile Exception", ex);
            }
        }

        private DocumentBuilder CreateDocument(ProjectViewModel project)
        {
            string text = null;

            text = new TextRange(project.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward), project.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward)).Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            Brush defaultForeground = ColorUtility.ConvertBrushFromString(Settings.Default.Foreground.ToString());
            Brush defaultBackground = ColorUtility.ConvertBrushFromString(Settings.Default.Background.ToString());

            bool highlight = false;

            DocumentBuilder documentBuilder = new DocumentBuilder(defaultForeground, defaultBackground);

            StringBuilder stringBuilder = new StringBuilder();

            RegexHelper regexHelper = new RegexHelper();

            Match match = null;

            string subString = String.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i].ToString())
                {
                    case "[":
                        subString = text.Substring(i);

                        match = regexHelper.WhiteSpace.Match(subString, 0);

                        if (match.Success == true)
                        {
                            StringBuilder whiteSpace = new StringBuilder();

                            for (int j = 0; j < int.Parse(match.Groups[1].Value); j++)
                            {
                                whiteSpace.Append(" ");
                            }

                            stringBuilder.Append(whiteSpace);

                            i = i + (match.Length - 1);
                        }

                        break;

                    case "%": subString = text.Substring(i);
                        switch (subString[1].ToString())
                        {
                            case "{":
                            case "}":
                            case "[":
                            case "]":
                            case "%":
                            case "\\":
                                stringBuilder.Append(subString[1].ToString());
                                break;

                            case "r": stringBuilder.Append("\r\n");
                                break;

                            case "t": stringBuilder.Append("\t");
                                break;

                            case "b": stringBuilder.Append(" ");
                                break;

                            case "x":
                            case "X":
                            case "c":
                            case "C":
                                switch (subString[2].ToString())
                                {
                                    case "x":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#808080");

                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#000000");
                                        }
                                        break;

                                    case "X":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#000000");
                                        break;

                                    case "r":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#FF0000");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#800000");
                                        }
                                        break;

                                    case "R":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#FF0000");
                                        break;

                                    case "g":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#00FF00");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#008000");
                                        }
                                        break;

                                    case "G":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#00FF00");
                                        break;

                                    case "y":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#FFFF00");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#808000");
                                        }
                                        break;

                                    case "Y":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#FFFF00");
                                        break;

                                    case "b":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#0000FF");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#000080");
                                        }
                                        break;

                                    case "B":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#0000FF");
                                        break;

                                    case "m":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#FF00FF");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#800080");
                                        }
                                        break;

                                    case "M":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#FF00FF");
                                        break;

                                    case "c":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#00FFFF");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#008080");
                                        }
                                        break;

                                    case "C":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#00FFFF");
                                        break;

                                    case "w":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        if (highlight)
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#FFFFFF");
                                        }
                                        else
                                        {
                                            documentBuilder.ActiveForeground = ColorUtility.ConvertBrushFromString("#C0C0C0");
                                        }
                                        break;

                                    case "W":
                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveBackground = ColorUtility.ConvertBrushFromString("#FFFFFF");
                                        break;

                                    case "n":
                                        highlight = false;

                                        documentBuilder.AddRun(stringBuilder.ToString());
                                        stringBuilder.Clear();

                                        documentBuilder.ActiveForeground = defaultForeground;
                                        documentBuilder.ActiveBackground = defaultBackground;
                                        break;

                                    case "h":
                                        highlight = true;
                                        break;

                                    case "f":
                                        break;

                                    case "i":
                                        break;

                                    default:

                                        match = regexHelper.AnsiColor.Match(subString, 0, 11);

                                        if (match.Success == true)
                                        {
                                            documentBuilder.AddRun(stringBuilder.ToString());
                                            stringBuilder.Clear();

                                            if (match.Groups[1].Value.Equals("X"))
                                            {
                                                documentBuilder.ActiveBackground =
                                                    ColorUtility.ConvertBrushFromString(match.Groups[2].Value);
                                            }
                                            else
                                            {
                                                documentBuilder.ActiveForeground =
                                                ColorUtility.ConvertBrushFromString(match.Groups[2].Value);
                                            }

                                            i = i + (match.Length - 3);
                                        }
                                        break;
                                }

                                i++;
                                break;
                        }

                        i++;
                        break;

                    case "{":
                    case "}":
                    case "]":
                    case "\\":
                        break;

                    default:
                        stringBuilder.Append(text[i].ToString()); break;
                }
            }

            documentBuilder.AddRun(stringBuilder.ToString());

            return documentBuilder;
        }

        private bool CanCompileProject(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null)
            {
                return false;
            }

            return true;
        }

        private void CloseWindow(Window window)
        {
            Logger.Log.Debug("Closing");

            window.Close();

            Logger.Log.DebugFormat("Closed Title[{0}]", window.Title);
        }

        private bool CanCloseWindow(Window window)
        {
            return true;
        }

        private void SelectSwatch(MouseButtonEventArgs eventArgs)
        {
            try
            {
                SwatchViewModel swatch = ((SwatchView)eventArgs.Source).DataContext as SwatchViewModel;

                switch (eventArgs.ChangedButton)
                {
                    case MouseButton.Left:
                        MainViewModel.ActiveProject.Foreground = swatch.Color;

                        if (MainViewModel.ActiveProject.Selection != null && !MainViewModel.ActiveProject.Selection.IsEmpty)
                        {
                            MainViewModel.ActiveProject.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush(swatch.Color));
                        }
                        break;

                    case MouseButton.Right:
                        MainViewModel.ActiveProject.Background = swatch.Color;

                        if (MainViewModel.ActiveProject.Selection != null && !MainViewModel.ActiveProject.Selection.IsEmpty)
                        {
                            MainViewModel.ActiveProject.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, new SolidColorBrush(swatch.Color));
                        }
                        break;

                    case MouseButton.Middle:
                        MainViewModel.ActiveProject.Backdrop = swatch.Color;
                        break;

                    default:
                        return;
                }

                Logger.Log.DebugFormat("Selected Color[{0}] Click[{1}]", swatch.Color, eventArgs.ChangedButton);

                MainViewModel.ActiveProject.State.IsDirty = true;

                eventArgs.Handled = true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Select Exception", ex);
            }
        }

        private bool CanSelectSwatch(MouseButtonEventArgs eventArgs)
        {
            if (MainViewModel.ActiveProject == null)
            {
                return false;
            }

            return true;
        }

        private void OpenLogLocation()
        {
            Logger.Log.Debug("Opening");

            var path = MainViewModel.WindowManager.AboutViewModel.Assembly.Location;

            Process.Start(System.IO.Path.GetDirectoryName(path));

            Logger.Log.DebugFormat("Opened Path[{0}]", path);
        }

        private bool CanOpenLogLocation()
        {
            return true;
        }

        private void StripColors()
        {
            if (MainViewModel.ActiveProject.Selection != null && !MainViewModel.ActiveProject.Selection.IsEmpty)
            {
                MainViewModel.ActiveProject.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush(Settings.Default.Foreground));
                MainViewModel.ActiveProject.Selection.ApplyPropertyValue(TextBlock.BackgroundProperty, new SolidColorBrush(Settings.Default.Background));
            }

            MainViewModel.ActiveProject.Foreground = Settings.Default.Foreground;
            MainViewModel.ActiveProject.Background = Settings.Default.Background;

            MainViewModel.ActiveProject.State.IsDirty = true;
        }

        private bool CanStripColors()
        {
            if (MainViewModel.ActiveProject == null)
            {
                return false;
            }

            if (MainViewModel.ActiveProject.Selection != null)
            {
                return true;
            }

            return false;
        }

        private void ProcessColorRule(ColorRulesViewModel colorRulesViewModel)
        {
            if (colorRulesViewModel.Selection == null)
            {
                return;
            }

            switch (colorRulesViewModel.Type)
            {
                case RuleTypes.Alternating:
                    ProcessAlternatingRule(colorRulesViewModel);
                    break;

                //case RuleTypes.Scaling:
                //    ProcessScalingRule(colorRulesViewModel);
                //    break;

                //case RuleTypes.Random:
                //    ProcessRandomRule(colorRulesViewModel);
                //    break;
            }
        }

        private void ProcessAlternatingRule(ColorRulesViewModel colorRulesViewModel)
        {
            var selection = MaterializeSelection(colorRulesViewModel);

            if (selection.GetType() == typeof(Section))
            {
                //
            }
            else if (selection.GetType() == typeof(Span))
            {
                //
            }
            else
            {
                MessageBox.Show(selection.GetType().ToString());
            }

            StringBuilder stringBuilder = new StringBuilder();

            if (selection.GetType() == typeof(Section))
            {
                var section = selection as Section;

                foreach (Block block in section.Blocks)
                {
                    var paragraph = block as Paragraph;

                    foreach (Inline inline in paragraph.Inlines)
                    {
                        var run = inline as Run;

                        if (run.Text.Length <= 0)
                        {
                            //stringBuilder.Append(Environment.NewLine);
                        }

                        stringBuilder.Append(run.Text);

                        //stringBuilder.Append(Environment.NewLine);
                    }
                }

                List<String> array = StringUtilities.SplitIntoParts(stringBuilder.ToString(), colorRulesViewModel.Interval).ToList<string>();

                Section newSection = new Section();
                Paragraph newParagraph = new Paragraph();

                Queue<Brush> foregroundBrushes = new Queue<Brush>();
                Queue<Brush> backgroundBrushes = new Queue<Brush>();

                foreach (SwatchViewModel swatchViewModel in colorRulesViewModel.ForegroundColors)
                {
                    foregroundBrushes.Enqueue(new SolidColorBrush(swatchViewModel.Color));
                }

                foreach (SwatchViewModel swatchViewModel in colorRulesViewModel.BackgroundColors)
                {
                    backgroundBrushes.Enqueue(new SolidColorBrush(swatchViewModel.Color));
                }

                for (int i = 0; i < array.Count; i++)
                {
                    Run run = new Run();
                    run.Text = array[i];

                    var foregorundBrush = foregroundBrushes.Dequeue();
                    var backgroundBrush = backgroundBrushes.Dequeue();

                    if (colorRulesViewModel.Scope == RuleScopes.Foreground)
                    {
                        run.Foreground = foregorundBrush;
                    }
                    else if (colorRulesViewModel.Scope == RuleScopes.Background)
                    {
                        run.Background = backgroundBrush;
                    }
                    else if (colorRulesViewModel.Scope == RuleScopes.Both)
                    {
                        run.Foreground = foregorundBrush;
                        run.Background = backgroundBrush;
                    }

                    foregroundBrushes.Enqueue(foregorundBrush);
                    backgroundBrushes.Enqueue(backgroundBrush);

                    newParagraph.Inlines.Add(run);
                }

                newSection.Blocks.Add(newParagraph);

                using (StreamWriter writer = new StreamWriter("temp.xaml"))
                {
                    XamlWriter.Save(newSection, writer.BaseStream);

                    writer.Close();
                }

                using (StreamReader reader = new StreamReader("temp.xaml"))
                {
                    MainViewModel.ActiveProject.Selection.Load(reader.BaseStream, DataFormats.Xaml);

                    reader.Close();
                }
            }
            else
            {
                MessageBox.Show(selection.GetType().ToString());
            }
        }

        private object MaterializeSelection(ColorRulesViewModel colorRulesViewModel)
        {
            object selection = null;

            using (StreamWriter writer = new StreamWriter("temp.xaml"))
            {
                colorRulesViewModel.Selection.Save(writer.BaseStream, DataFormats.Xaml);

                writer.Close();
            }

            using (StreamReader reader = new StreamReader("temp.xaml"))
            {
                selection = XamlReader.Load(reader.BaseStream);

                reader.Close();
            }

            return selection;
        }

        private void ProcessScalingRule(ColorRulesViewModel colorRulesViewModel)
        {
            
        }

        private void ProcessRandomRule(ColorRulesViewModel colorRulesViewModel)
        {
            //throw new NotImplementedException();
        }

        private bool CanProcessColorRule(ColorRulesViewModel colorRulesViewModel)
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