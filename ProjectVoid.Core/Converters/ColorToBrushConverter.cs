using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjectVoid.Core.Converters
{
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = (Brush)value;

            return ColorConverter.ConvertFromString(brush.ToString());
        }
    }
}