using log4net;
using System.Reflection;

namespace ProjectVoid.Core.Utilities
{
    public class Logger
    {
        public static ILog Log { get; private set; }

        public static void Create()
        {
            if (Log != null)
            {
                return;
            }

            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
