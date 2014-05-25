using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
