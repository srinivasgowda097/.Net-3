using System;

using Castle.DynamicProxy;

namespace Nagarro.ProxyWrapperGenerator.Interceptors.Abstract
{
    /// <summary>
    /// Implements the abstract base interceptor.
    /// </summary>
    public abstract class BaseInterceptor : IInterceptor
    {
        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            this.BeforeInvocation(invocation);

            try
            {
                this.InnerInvocation(invocation);
            }
            catch (Exception e)
            {
                this.OnException(invocation, e);
            }

            this.AfterInvocation(invocation);
        }

        /// <summary>
        /// Implements the logic that will be executed before the invocation proceeds.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected virtual void BeforeInvocation(IInvocation invocation)
        {
            // Do nothing here.
        }

        /// <summary>
        /// Implements the logic that will be executed after the invocation proceeded successfully.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected virtual void AfterInvocation(IInvocation invocation)
        {
            // Do nothing here.
        }

        /// <summary>
        /// Implements the logic that will be executed if an 
        /// exception is raised during the invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="exception">The exception.</param>
        /// <exception cref="Exception">
        /// The base method will always throw the exception again.
        /// </exception>
        protected virtual void OnException(IInvocation invocation, Exception exception)
        {
               throw exception;
        }

        /// <summary>
        /// Implements the logic that will be executed.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected virtual void InnerInvocation(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
