using System.Text.RegularExpressions;

namespace TheCreationist.Core.Helpers
{
    public class RegexHelper
    {
        private Regex _AnsiColor = null;
        private Regex _WhiteSpace = null;

        public RegexHelper()
        {
            _AnsiColor = new Regex(@"{(\[)?(\d\d\d)");
            _WhiteSpace = new Regex(@"\[space\(([0-9]*)\)\]");
        }

        public Regex AnsiColor
        {
            get
            {
                return _AnsiColor;
            }
        }

        public Regex WhiteSpace
        {
            get
            {
                return _WhiteSpace;
            }
        }
    }
}