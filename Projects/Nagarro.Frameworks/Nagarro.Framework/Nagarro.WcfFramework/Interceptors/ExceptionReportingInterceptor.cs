using System;

using Castle.DynamicProxy;

namespace Nagarro.WcfFramework.Interceptors
{
    /// <summary>
    /// Implements the exception
    /// reporting interceptor.
    /// </summary>
    public class ExceptionReportingInterceptor : BaseInterceptor
    {
        /// <summary>
        /// Holds the error reporting action
        /// </summary>
        private Action<IInvocation, Exception> exceptionReportingAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportingInterceptor"/> class.
        /// </summary>
        /// <param name="errorReportingAction">The error reporting action.</param>
        public ExceptionReportingInterceptor(Action<Exception> errorReportingAction) : this((a, b) => errorReportingAction(b))
        {
            if (errorReportingAction == null)
            {
                throw new ArgumentNullException("errorReportingAction");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportingInterceptor"/> class.
        /// </summary>
        /// <param name="exceptionReportingAction">The error reporting action.</param>
        public ExceptionReportingInterceptor(Action<IInvocation, Exception> exceptionReportingAction)
        {
            if (exceptionReportingAction == null)
            {
                throw new ArgumentNullException("exceptionReportingAction");
            }

            this.exceptionReportingAction = exceptionReportingAction;
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
            this.exceptionReportingAction(invocation, exception);

            base.OnException(invocation, exception);
        }
    }
}
