using System;
using System.Reflection;

using Castle.DynamicProxy;

namespace Nagarro.ProxyWrapperGenerator.InterceptorsHooks
{
    /// <summary>
    /// Implements the abstrace base
    /// interceptor hook.
    /// </summary>
    public abstract class InterceptorHookBase : IProxyGenerationHook
    {
        /// <summary>
        /// Shoulds the intercept method.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>
        /// True if the method should should be
        /// intercepted, otherwise false.
        /// </returns>
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return this.InterceptMethod(methodInfo);
        }

        /// <summary>
        /// Nons the proxyable member notification.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <param name="memberInfo">The member info.</param>
        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            // Do nothing here.
        }

        /// <summary>
        /// Methods inspected(Not required).
        /// </summary>
        public void MethodsInspected()
        {
            // Do nothing here.
        }

        /// <summary>
        /// Implements the logic
        /// for deciding whether to
        /// intercept a method
        /// or not.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>
        /// True if the method should should be
        /// intercepted, otherwise false.
        /// </returns>
        protected abstract bool InterceptMethod(MethodInfo methodInfo);
    }
}
