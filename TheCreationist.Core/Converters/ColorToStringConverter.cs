using TheCreationist.Core.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TheCreationist.Core.Helpers;

namespace TheCreationist.Core.Converters
{
    [ValueConversion(typeof(Brush), typeof(string))]
    public class ColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            XtermHelper xtermHelper = new XtermHelper();

            var color = (Color)value;

            return xtermHelper.ConvertHexToRgb555(color.ToString().Substring(3));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value.ToString();

            return ColorUtility.ConvertColorFromString(s);
        }
    }
}