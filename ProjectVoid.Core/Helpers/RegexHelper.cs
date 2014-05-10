using System.Text.RegularExpressions;

namespace ProjectVoid.Core.Helpers
{
    public class RegexHelper
    {
        #region Fields

        private Regex _AnsiColor = null;
        private Regex _WhiteSpace = null;

        #endregion Fields

        #region Constructors

        public RegexHelper()
        {
            _AnsiColor = new Regex(@"%([x|X])<(#[0-9a-fA-F]{6})>");
            _WhiteSpace = new Regex(@"\[space\(([0-9]*)\)\]");
        }

        #endregion Constructors

        #region Properties

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

        #endregion Properties
    }
}