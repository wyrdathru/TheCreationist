using TheCreationist.Core.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TheCreationist.Core.Converters
{
    [ValueConversion(typeof(Brush), typeof(string))]
    public class ColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;

            return color.ToString().Substring(3).Insert(0, "#");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value.ToString();

            return ColorUtility.ConvertColorFromString(s);
        }
    }
}