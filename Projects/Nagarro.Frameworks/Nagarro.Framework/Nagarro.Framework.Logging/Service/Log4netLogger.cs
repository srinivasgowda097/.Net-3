using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nagarro.Framework.Logging.Configuration;
using Nagarro.Framework.Logging.Interfaces;
using ILog4net = log4net.ILog;

namespace Nagarro.Framework.Logging.Service
{
    /// <summary>
    /// The log 4 net logger.
    /// </summary>
    public class Log4netLogger : ILog
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private ILog4net logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4netLogger"/> class.
        /// </summary>
        /// <param name="loggerName">
        /// The logger Name.
        /// </param>
        /// <param name="startAsync">
        /// The start Async. Default value is false if passed true then initializatin will be done asynchronously.
        /// </param>
        public Log4netLogger(string loggerName, bool startAsync = false)
        {
            if (string.IsNullOrEmpty(loggerName))
            {
                throw new ArgumentNullException("loggerName parameter must not be null.");
            }

            this.InitializeLog4Net(loggerName, startAsync);
        }

        /// <summary>
        /// Logs the error to the log file
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void Error(string message, Exception exception)
        {
            this.logger.Error(message, exception);
        }

        /// <summary>
        /// The debug format.
        /// </summary>
        /// <param name="stringValue">
        /// The string Value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public void DebugFormat(string stringValue, params string[] args)
        {
            this.logger.DebugFormat(stringValue, args);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        public void Debug(string stringValue)
        {
            this.logger.Debug(stringValue);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        public void Info(string stringValue)
        {
            this.logger.Info(stringValue);
        }

        /// <summary>
        /// The info format.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public async void InfoFormat(string stringValue, params string[] args)
        {
            this.logger.InfoFormat(stringValue, args);
        }

        /// <summary>
        /// Asynchrounously logs the errors
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public async void ErrorAsync(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchrounously logs the information with string format support
        /// </summary>
        /// <param name="stringValue">
        /// The string Value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public async void DebugFormatAsync(string stringValue, params string[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchrounously logs the debug information
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        public async void DebugAsync(string stringValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchrounously logs the information
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        public async void InfoAsync(string stringValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchrounously logs the information with string format support
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public async void InfoFormatAsync(string stringValue, params string[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The initialize log 4 net.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        /// <param name="startAsync">
        /// The start async.
        /// </param>
        /// <exception cref="AggregateException"> Throws when the initialization task show faulted state
        /// </exception>
        private void InitializeLog4Net(string loggerName, bool startAsync)
        {
            Task initializeLoggerTask = new Task(() => new InitializeLog4Net().Init());

            // Actions for successful initialization
            initializeLoggerTask.ContinueWith(
                task => this.logger = log4net.LogManager.GetLogger(loggerName),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            // Actions when exception occurs during execution
            initializeLoggerTask.ContinueWith(
                s =>
                {
                    s.Exception.Flatten();
                    throw s.Exception;
                },
                TaskContinuationOptions.OnlyOnFaulted);

            if (startAsync)
            {
                initializeLoggerTask.Start();
            }

            initializeLoggerTask.RunSynchronously();
        }
    }
}
