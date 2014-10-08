using System;

using Castle.DynamicProxy;

using Nagarro.ProxyWrapperGenerator.Interceptors.Abstract;

namespace Nagarro.ProxyWrapperGenerator.Interceptors
{
    /// <summary>
    /// Implements the custom interceptor that can be used to get notified about invocations.
    /// </summary>
    public class CustomInterceptor : BaseInterceptor
    {
        /// <summary>
        /// Holds the action that will be called after an invocation.
        /// </summary>
        private readonly Action<IInvocation> afterInvocationAction;

        /// <summary>
        /// Holds the action that will be called before an invocation.
        /// </summary>
        private readonly Action<IInvocation> beforeInvocationAction;

        /// <summary>
        /// Holds the action that will be called if an exception was raised
        /// during the invocation.
        /// </summary>
        private readonly Action<IInvocation, Exception> onExceptionAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomInterceptor"/> class.
        /// </summary>
        /// <param name="onExceptionAction">The on exception action.</param>
        public CustomInterceptor(Action<Exception> onExceptionAction)
            : this((invocation, exception) =>
                   {
                       if (onExceptionAction != null)
                       {
                           onExceptionAction(exception);
                       }
                   }) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomInterceptor"/> class.
        /// </summary>
        /// <param name="onExceptionAction">The on exception action.</param>
        public CustomInterceptor(Action<IInvocation, Exception> onExceptionAction)
        {
            this.onExceptionAction = onExceptionAction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomInterceptor"/> class.
        /// </summary>
        /// <param name="beforeInvocationAction">The before invocation action.</param>
        /// <param name="afterInvocationAction">The after invocation action.</param>
        public CustomInterceptor(Action<IInvocation> beforeInvocationAction, Action<IInvocation> afterInvocationAction)
            : this(beforeInvocationAction, afterInvocationAction, (Action<Exception>)null) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomInterceptor"/> class.
        /// </summary>
        /// <param name="beforeInvocationAction">The before invocation action.</param>
        /// <param name="afterInvocationAction">The after invocation action.</param>
        /// <param name="onExceptionAction">The on exception action.</param>
        public CustomInterceptor(
            Action<IInvocation> beforeInvocationAction, 
            Action<IInvocation> afterInvocationAction,
            Action<Exception> onExceptionAction)
            : this(
            beforeInvocationAction,
                afterInvocationAction,
                (invocation, exception) =>
                   {
                       if (onExceptionAction != null)
                       {
                           onExceptionAction(exception);
                       }
                   }) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomInterceptor"/> class.
        /// </summary>
        /// <param name="beforeInvocationAction">The before invocation action.</param>
        /// <param name="afterInvocationAction">The after invocation action.</param>
        /// <param name="onExceptionAction">The on exception action.</param>
        public CustomInterceptor(
            Action<IInvocation> beforeInvocationAction, 
            Action<IInvocation> afterInvocationAction, 
            Action<IInvocation, Exception> onExceptionAction)
        {
            this.beforeInvocationAction = beforeInvocationAction;
            this.afterInvocationAction = afterInvocationAction;
            this.onExceptionAction = onExceptionAction;
        }

        /// <summary>
        /// Implements the logic that
        /// will be executed after the
        /// invocation proceeded successfully.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected override void AfterInvocation(IInvocation invocation)
        {
            if (this.afterInvocationAction != null)
            {
                this.afterInvocationAction(invocation);
            }

            base.AfterInvocation(invocation);
        }

        /// <summary>
        /// Implements the logic that
        /// will be executed before the
        /// invocation proceeds.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected override void BeforeInvocation(IInvocation invocation)
        {
            if (this.beforeInvocationAction != null)
            {
                this.beforeInvocationAction(invocation);
            }

            base.BeforeInvocation(invocation);
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
            if (this.onExceptionAction != null)
            {
                this.onExceptionAction(invocation, exception);
            }

            base.OnException(invocation, exception);
        }
    }
}