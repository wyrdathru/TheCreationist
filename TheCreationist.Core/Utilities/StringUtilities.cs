using System;
using System.Collections.Generic;

namespace TheCreationist.Core.Utilities
{
    public static class StringUtilities
    {
        public static IEnumerable<string> SplitIntoParts(string str, int interval)
        {
            for (int i = 0; i < str.Length; i += interval)
            {
                yield return str.Substring(i, Math.Min(interval, str.Length - i));
            }
        }
    }
}
