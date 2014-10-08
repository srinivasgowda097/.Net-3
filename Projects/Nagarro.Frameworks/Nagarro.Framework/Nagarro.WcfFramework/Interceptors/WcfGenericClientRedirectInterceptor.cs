using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

using Castle.DynamicProxy;

namespace Nagarro.WcfClientFramework.Interceptors
{
    /// <summary>
    /// The wcf generic client redircect interceptor
    /// redirects calls to generic methods to the
    /// hidden invoke generic method.
    /// </summary>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    public class WcfGenericClientRedirectInterceptor<TInterface> : WcfClientRedirectInterceptor<TInterface>
    {
        /// <summary>
        /// Holds the method name for the invoke generic method.
        /// </summary>
        private const string HiddenMethodInvokeGenericMethodName = "HiddenMethod_InvokeGenericMethod";

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfGenericClientRedirectInterceptor&lt;TInterface&gt;"/> class.
        /// </summary>
        /// <param name="hostServiceName">Name of the host service.</param>
        public WcfGenericClientRedirectInterceptor(string hostServiceName) : base(hostServiceName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfGenericClientRedirectInterceptor&lt;TInterface&gt;"/> class.
        /// </summary>
        /// <param name="hostServiceName">Name of the host service.</param>
        /// <param name="endPointAddress">The end point address.</param>
        public WcfGenericClientRedirectInterceptor(string hostServiceName, string endPointAddress) : base(hostServiceName, endPointAddress)
        {
        }

        /// <summary>
        /// Calls the service.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="channelObject">The channel object.</param>
        /// <returns>
        /// An instance of <see cref="object"/> representing
        /// the execution result.
        /// </returns>
        protected override object ServiceCall(IInvocation invocation, object channelObject)
        {
            if (!invocation.Method.IsGenericMethod)
            {
                return base.ServiceCall(invocation, channelObject);
            }

            Type type = channelObject.GetType();

            MethodInfo hiddenMethod = type.GetMethod(HiddenMethodInvokeGenericMethodName);

            if (hiddenMethod == null)
            {
                foreach (var typInterface in type.GetInterfaces())
                {
                    hiddenMethod = typInterface.GetMethod(HiddenMethodInvokeGenericMethodName);

                    if (hiddenMethod != null)
                    {
                        break;
                    }
                }
            }

            if (hiddenMethod == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                    "The method {0} was not found. Make sure the interface inherits from IGenericWcfService",
                    HiddenMethodInvokeGenericMethodName));
            }

            IList<object> genericTypes = invocation.Method.GetGenericArguments().Select(FormatterServices.GetUninitializedObject).ToList();
            IList<object> parameters = invocation.Arguments;

            return hiddenMethod.Invoke(
                channelObject,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { invocation.Method.Name, genericTypes, parameters }, 
                CultureInfo.InvariantCulture);
        }
    }
}
