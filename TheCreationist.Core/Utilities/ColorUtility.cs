using System;
using System.Windows.Media;

namespace TheCreationist.Core.Utilities
{
    public static class ColorUtility
    {
        public static Brush ConvertBrushFromString(string s)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(s));
        }

        public static Color ConvertColorFromString(string s)
        {
            return (Color)ColorConverter.ConvertFromString(s);
        }

        public static bool IsValid(string s)
        {
            ColorConverter converter = new ColorConverter();

            if (String.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            if (s.Length != 7)
            {
                return false;
            }

            if (!s.StartsWith("#"))
            {
                return false;
            }

            if (!converter.IsValid(s))
            {
                return false;
            }

            return true;
        }
    }
}