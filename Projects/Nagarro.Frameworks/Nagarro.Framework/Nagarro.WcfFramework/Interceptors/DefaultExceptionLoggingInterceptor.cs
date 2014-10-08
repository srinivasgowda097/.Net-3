using System;

using Castle.DynamicProxy;

using Nagarro.Framework.Logging;

namespace Nagarro.WcfFramework.Interceptors
{
    /// <summary>
    /// Implements the default exception logging
    /// interceptor. The interceptor will
    /// log the exception as an error
    /// into the specified <see cref="ILog"/>
    /// and call the base interceptor (that
    /// will re-throw the exception.
    /// </summary>
    public class DefaultExceptionLoggingInterceptor : BaseInterceptor
    {
        /// <summary>
        /// Holds the log to use for
        /// logging exceptions.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultExceptionLoggingInterceptor"/> class.
        /// </summary>
        /// <param name="log">The log to use.</param>
        public DefaultExceptionLoggingInterceptor(ILog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }

            this.log = log;
        }

        /// <summary>
        /// Implements the logic that will be
        /// executed if an exception is raised
        /// during the invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="exception">The exception.</param>
        /// <exception cref="Exception">
        /// The base method will always throw the exception again.
        /// </exception>
        protected override void OnException(IInvocation invocation, Exception exception)
        {
            // Log the exception
            this.LogException(invocation, exception);

            // Direct the exeception to the base.
            base.OnException(invocation, exception);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="exception">The exception.</param>
        protected virtual void LogException(IInvocation invocation, Exception exception)
        {
            this.log.Error(
                string.Format(
                    "An exception was raised during the execution of {0}.",
                    invocation.MethodInvocationTarget.Name),
                exception);
        }
    }
}
