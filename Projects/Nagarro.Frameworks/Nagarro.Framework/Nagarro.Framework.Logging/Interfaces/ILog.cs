using System;

namespace Nagarro.Framework.Logging.Interfaces
{
    /// <summary>
    /// Logs the errors, infos, debug statements including the support for Async logging
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs the error to the log file
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Error(string message, Exception exception);

        /// <summary>
        /// The debug format.
        /// </summary>
        /// <param name="stringValue">
        /// The string Value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        void DebugFormat(string stringValue, params string[] args);

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        void Debug(string stringValue);

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        void Info(string stringValue);

        /// <summary>
        /// The info format.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        void InfoFormat(string stringValue, params string[] args);

        /// <summary>
        /// Asynchrounously logs the errors
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void ErrorAsync(string message, Exception exception);

        /// <summary>
        /// Asynchrounously logs the information with string format support
        /// </summary>
        /// <param name="stringValue">
        /// The string Value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        void DebugFormatAsync(string stringValue, params string[] args);

        /// <summary>
        /// Asynchrounously logs the debug information
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        void DebugAsync(string stringValue);

        /// <summary>
        /// Asynchrounously logs the information
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        void InfoAsync(string stringValue);

        /// <summary>
        /// Asynchrounously logs the information with string format support
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        void InfoFormatAsync(string stringValue, params string[] args);
    }
}
