using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCreationist.Core.Helpers;

namespace TheCreationist.Bootstrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] rgb = "555".ToCharArray();

            var r = (XtermHelper.R_BIT_VALUE * char.GetNumericValue(rgb[0]));
            var g = (XtermHelper.G_BIT_VALUE * char.GetNumericValue(rgb[1]));
            var b = (XtermHelper.B_BIT_VALUE * char.GetNumericValue(rgb[2]));

            var value = (XtermHelper.LOW_COLOR_COUNT + XtermHelper.HIGH_COLOR_COUNT) + r + g + b;

            var x = Math.Floor(value / 100);
            var y = Math.Floor((value % 100) / 10);
            var z = value % 10;

            Console.WriteLine(String.Format("{0} {1} {2}", x, y, z));

            Console.ReadLine();
        }
    }
}
