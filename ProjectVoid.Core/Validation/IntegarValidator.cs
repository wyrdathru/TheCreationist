using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProjectVoid.Core.Validation
{
    public class IntegarValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"\d+");

            if (regex.IsMatch(value as string))
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Please enter a valid integar.");
        }
    }
}
