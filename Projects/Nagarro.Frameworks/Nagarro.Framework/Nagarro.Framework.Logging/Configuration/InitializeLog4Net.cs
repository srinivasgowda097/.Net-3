using System;
using System.Configuration;
using System.IO;

namespace Nagarro.Framework.Logging.Configuration
{
    /// <summary>
    /// Implements an initiali
    /// </summary>
    internal class InitializeLog4Net
    {
        /// <summary>
        /// Carries out the initilaiztion of the log4Net application.
        /// </summary>
        /// <returns>The report that is emitted by the initialization.</returns>
        internal bool Init()
        {
            // load path for log4NetConfiguration from AppSettings
            string path = this.GetLog4NetConfig(DefaultLog4netConfiguration.AppSettingsKeyForLog4NetConfigurationFile);

            // trigger configuration of log4net
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));

            return true;
        }

        /// <summary>
        /// Extracts an abitray string, that must not be null or empty, from the appsettings.
        /// </summary>
        /// <param name="appsettingsKey">Specifies the key of the appsettings.</param>
        /// <returns>a string configured in the appsettings.</returns>
        /// <exception cref="Exception">specified error message</exception>
        private string GetLog4NetConfig(string appsettingsKey)
        {
            // extract the value from the appsettings
            string log4NetPath = ConfigurationManager.AppSettings[appsettingsKey];

            // check whether the specified appsettings key exists
            if (log4NetPath == null)
            {
                throw new ConfigurationErrorsException(string.Format("No AppsettingsEntry with key {0} detected.", appsettingsKey));
            }

            // check whether the value of the appsettingskey is not empty
            if (log4NetPath.Trim().Length == 0)
            {
                throw new ConfigurationErrorsException(string.Format("No AppsettingsEntry with key {0} was empty.", appsettingsKey));
            }

            // check whether the value of the appsettingskey is not empty)
            if (!File.Exists(log4NetPath))
            {
                throw new ConfigurationErrorsException(string.Format("Configured file path {0}, identified by AppsettingsEntry with key {1} does not exist.", log4NetPath, appsettingsKey));
            }

            return log4NetPath;
        }
    }
}
