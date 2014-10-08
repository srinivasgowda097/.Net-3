using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Framework.Logging.Configuration
{
    /// <summary>
    /// Implements the default message sink service configuration.
    /// </summary>
    internal static class DefaultLog4netConfiguration
    {
        /// <summary>
        /// Holds the appsettingsentry for the log4net configuration file.
        /// </summary>
        private const string DefaultLog4NetConfigFile = "Framework.Log4netLoggerXmlFile";

        /// <summary>
        /// Holds the appsettingsentry for the initialisation configuration file.
        /// </summary>
        private const string InitialisationLog = "InitializationLogFileName";

        /// <summary>
        /// Gets the value of the appsettings key for log4net configurations file.
        /// </summary>
        public static string AppSettingsKeyForLog4NetConfigurationFile
        {
            get
            {
                return DefaultLog4NetConfigFile;
            }
        }

        /// <summary>
        /// Gets the value of the appsettings key for the initialisation configuration file.
        /// </summary>
        public static string AppSettingsKeyInitialisationLog
        {
            get
            {
                return InitialisationLog;
            }
        }
    }
}
