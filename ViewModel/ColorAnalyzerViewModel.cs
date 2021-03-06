﻿using GalaSoft.MvvmLight;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TheCreationist.App.ViewModel
{
    public class ColorAnalyzerViewModel : ViewModelBase, IDisposable
    {
        public ColorAnalyzerViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; private set; }

        public TextSelection Selection { get; set; }

        public string SelectionForeground
        {
            get { return GetForeground(); }
        }

        public string SelectionBackground
        {
            get { return GetBackground(); }
        }

        private string GetForeground()
        {
            Brush brush = null;

            try
            {
                brush = ((Brush)Selection.GetPropertyValue(TextBlock.ForegroundProperty));
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Exception", ex);
                return "Invalid";
            }

            return brush.ToString().Substring(3).Insert(0, "#");
        }

        private string GetBackground()
        {
            Brush brush = null;

            try
            {
                brush = ((Brush)Selection.GetPropertyValue(TextBlock.BackgroundProperty));
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Exception", ex);
                return "Invalid";
            }

            return brush.ToString().Substring(3).Insert(0, "#");
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            Selection = null;

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}