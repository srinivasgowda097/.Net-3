using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nagarro.WcfFramework.Hooks
{
    /// <summary>
    /// Implements the hook for the method filetering for interception. Help in logging only specific method.
    /// </summary>
    public class MethodFilterInterceptorHook : InterceptorHookBase
    {
        /// <summary>
        /// Holds the method to intercept.
        /// </summary>
        private List<string> methodsToIntercept;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodFilterInterceptorHook"/> class.
        /// </summary>
        /// <param name="methodsToIntercept">The methods to intercept.</param>
        public MethodFilterInterceptorHook(params MethodBase[] methodsToIntercept) : this(methodsToIntercept.Select(s => s.Name).ToArray()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodFilterInterceptorHook"/> class.
        /// </summary>
        /// <param name="methodsToIntercept">The methods to intercept.</param>
        public MethodFilterInterceptorHook(params string[] methodsToIntercept)
        {
            this.methodsToIntercept = new List<string>(methodsToIntercept);
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
        protected override bool InterceptMethod(MethodInfo methodInfo)
        {
            return this.methodsToIntercept.Contains(methodInfo.Name);
        }
    }
}
