using System;

namespace Nagarro.Framework.Logging
{
    /// <summary>
    /// Logs the errors, infos, debug statements. Any client that ask for logging support with proxy client must 
    /// implement this interface with a wrapper of any logging provider.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Error(string format, Exception exception);

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
    }
}
