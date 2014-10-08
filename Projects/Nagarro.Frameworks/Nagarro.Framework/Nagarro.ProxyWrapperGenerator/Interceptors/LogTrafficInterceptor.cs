using System;
using System.Text;

using Castle.DynamicProxy;

using Nagarro.Framework.Logging.Interfaces;
using Nagarro.ProxyWrapperGenerator.Interceptors.Abstract;

namespace Nagarro.ProxyWrapperGenerator.Interceptors
{
    /// <summary>
    /// Implements an interceptor that logs the traffic of
    /// a particualr service (specified by an interface).
    /// </summary>
    public class LogTrafficInterceptor : BaseInterceptor
    {
        /// <summary>
        /// Holds the trafficlogger instance.
        /// </summary>
        private ILog trafficLogger;

        /// <summary>
        /// Initializes a new instance of the LogTrafficInterceptor class.
        /// </summary>
        /// <param name="trafficLogger">Specifies the logger instance that is
        /// used for logging the traffic.</param>
        public LogTrafficInterceptor(ILog trafficLogger)
        {
            this.trafficLogger = trafficLogger;

            this.CheckInstance();
        }

        /// <summary>
        /// Itercepts the invokation of the wcf service call before
        /// it is forwarded to the service for logging purposes.
        /// </summary>
        /// <param name="invocation">The invocation that is intercepted.</param>
        protected override void BeforeInvocation(IInvocation invocation)
        {
            this.CheckInstance();

            this.trafficLogger.DebugFormat("Before invocation {0}.", this.GetStringOfInvocation(invocation));

            base.BeforeInvocation(invocation);
        }

        /// <summary>
        /// Itercepts the invokation of the wcf service call after
        /// it has been executed by the service for logging purposes.
        /// </summary>
        /// <param name="invocation">The invocation that is intercepted.</param>
        protected override void AfterInvocation(IInvocation invocation)
        {
            base.AfterInvocation(invocation);

            this.trafficLogger.DebugFormat("After invocation {0} with returnvalue {1}.", invocation.GetConcreteMethod().Name, invocation.ReturnValue != null ? invocation.ReturnValue.ToString() : "null or void");
        }

        /// <summary>
        /// Implements the invariante of the class.
        /// </summary>
        private void CheckInstance()
        {
            if (this.trafficLogger == null)
            {
                throw new InvalidOperationException("The field trafficLogger must not be null.");
            }
        }

        /// <summary>
        /// Gets a string representing a particular invocation.
        /// </summary>
        /// <param name="invocationToRepresent">Specirfies the invocation instance to represent.</param>
        /// <returns>A string that represnets the supplied invocation.</returns>
        private string GetStringOfInvocation(IInvocation invocationToRepresent)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("\r\nInvokation {0}.", invocationToRepresent.GetConcreteMethod().Name);

            builder.AppendLine("With arguments ");
            
            foreach (object argument in invocationToRepresent.Arguments)
            {
                builder.AppendFormat("Argument {0}.", argument);
            }

            return builder.ToString();
        }
    }
}
