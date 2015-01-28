using TheCreationist.Core.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace TheCreationist.Core.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class ColorStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ColorUtility.IsValid(value as string))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}