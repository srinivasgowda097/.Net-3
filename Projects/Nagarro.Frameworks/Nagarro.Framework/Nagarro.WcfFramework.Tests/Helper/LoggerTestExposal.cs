using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nagarro.Framework.Logging.Interfaces;

namespace Nagarro.WcfClientFramework.Tests.Helper
{
    /// <summary>
    /// The logger test exposal.
    /// </summary>
    internal class LoggerTestExposal : ILog
    {
        /// <summary>
        /// Gets or sets the logged message. Used to fetch the last log
        /// </summary>
        public string LoggedMessage { get; set; }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void Error(string message, Exception exception)
        {
            this.LoggedMessage = string.Format("{0} - {1}", message, exception);
        }

        /// <summary>
        /// The debug format.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public void DebugFormat(string stringValue, params string[] args)
        {
            this.LoggedMessage = string.Format(stringValue, args);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        public void Debug(string stringValue)
        {
            this.LoggedMessage = string.Format(stringValue);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        public void Info(string stringValue)
        {
            this.LoggedMessage = string.Format(stringValue);
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
        public void InfoFormat(string stringValue, params string[] args)
        {
            this.LoggedMessage = string.Format(stringValue, args);
        }

        /// <summary>
        /// The error async.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <exception cref="NotImplementedException">The NotImplementedException
        /// </exception>
        public void ErrorAsync(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The debug format async.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <exception cref="NotImplementedException">The NotImplementedException
        /// </exception>
        public void DebugFormatAsync(string stringValue, params string[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The debug async.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <exception cref="NotImplementedException">The NotImplementedException
        /// </exception>
        public void DebugAsync(string stringValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The info async.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <exception cref="NotImplementedException">The NotImplementedException
        /// </exception> 
        public void InfoAsync(string stringValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The info format async.
        /// </summary>
        /// <param name="stringValue">
        /// The string value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <exception cref="NotImplementedException">The NotImplementedException
        /// </exception>
        public void InfoFormatAsync(string stringValue, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
