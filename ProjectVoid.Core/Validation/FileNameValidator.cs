using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProjectVoid.Core.Validation
{
    public class FileNameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[\w\-. ]+$");

            if (regex.IsMatch(value as string))
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Please enter a valid file name.");
        }
    }
}
