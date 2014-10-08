using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Framework.Logging
{
    /// <summary>
    /// Provides the custom means for logging. You can set any target for logging e.g. Traces, file logging, database logging, event logs
    /// </summary>
    public static class LoggingCapabilities
    {
        /// <summary>
        /// The async result for testing purpose
        /// </summary>
        public static readonly List<IAsyncResult> AsyncResults = new List<IAsyncResult>();

        /// <summary>
        /// static logging handlers list
        /// </summary>
        private static readonly List<Action<string>> LoggingHandlers = new List<Action<string>>();

        /// <summary>
        /// Sets a logging handler for the logging event.
        /// The logging event is triggered by the Log operation.
        /// </summary>
        /// <param name="loggingHandler">The handler for logging event. Must not be null.
        /// </param>
        public static void AddLogHandler(Action<string> loggingHandler)
        {
            LoggingHandlers.Add(loggingHandler);
        }

        /// <summary>
        /// Removes a logging handler from the logging event.
        /// The logging event is triggered by the Log operation.
        /// </summary>
        /// <param name="loggingHandler">The handler for logging event. Must not be null.
        /// </param>
        public static void RemoveLogHandler(Action<string> loggingHandler)
        {
            if (!LoggingHandlers.Contains(loggingHandler))
            {
                LoggingHandlers.Remove(loggingHandler);
            }
        }

        /// <summary>
        /// it is going to remove all logging hadlers
        /// </summary>
        public static void RemoveAllLogHandler()
        {
            LoggingHandlers.Clear();
        }

        /// <summary>
        /// returns current registered logging handlers
        /// </summary>
        /// <returns>logging handlers</returns>
        public static List<Action<string>> GetLoggingHandlers()
        {
            return LoggingHandlers;
        }

        /// <summary>
        /// Generates a log entry with the supplied message.
        /// The concrete realization of Log operation depends on the afore specified logging function.
        /// </summary>
        /// <param name="message">Specifies the message to log. When null, empty or whitespace is supplied
        /// a proper message is logged. Method will not throw in case of invalid input argument.
        /// </param>
        public static void LogAsync(string message)
        {
            foreach (var lh in LoggingHandlers)
            {
                AsyncResults.Add(lh.BeginInvoke(message, null, null));
            }
        }

        /// <summary>
        /// Generates a log entry with the supplied message by SYNC
        /// The concrete realization of Log operation depends on the afore specified logging function.
        /// </summary>
        /// <param name="message">Specifies the message to log. When null, empty or whitespace is supplied
        /// a proper message is logged. Method will not throw in case of invalid input argument.
        /// </param>
        public static void LogSync(string message)
        {
            foreach (var lh in LoggingHandlers)
            {
                lh.Invoke(message);
            }
        }

    }
}
