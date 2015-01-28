using TheCreationist.Core.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TheCreationist.Core.Converters
{
    [ValueConversion(typeof(string), typeof(Brush))]
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ColorUtility.ConvertBrushFromString(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as Brush;

            return brush.ToString();
        }
    }
}