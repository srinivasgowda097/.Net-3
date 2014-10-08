using System.Reflection;

namespace Nagarro.ProxyWrapperGenerator.InterceptorsHooks
{
    /// <summary>
    /// Implements the default interceptor hook.
    /// </summary>
    public sealed class DefaultInterceptorHook : InterceptorHookBase
    {
        /// <summary>
        /// Default interceptor allows all the methods to be intercepted.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>
        /// True if the method should should be
        /// intercepted, otherwise false.
        /// </returns>
        protected override bool InterceptMethod(MethodInfo methodInfo)
        {
            return true;
        }
    }
}
