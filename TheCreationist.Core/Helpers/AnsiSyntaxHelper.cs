using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCreationist.Core.Enum;

namespace TheCreationist.Core.Helpers
{
    public static class AnsiSyntaxHelper
    {
        public static string Generate(AnsiEngines AnsiEngine)
        {
            switch (AnsiEngine)
            {
                case AnsiEngines.XTERM:
                    break;

                case AnsiEngines.TINYMUX:
                    break;

                default:
                    break;
            }

            return string.Empty;
        }
    }
}
