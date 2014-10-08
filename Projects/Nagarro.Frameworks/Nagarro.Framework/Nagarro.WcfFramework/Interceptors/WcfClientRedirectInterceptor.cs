using System;
using System.Globalization;
using System.Reflection;
using System.ServiceModel;

using Castle.DynamicProxy;

using Nagarro.ProxyWrapperGenerator.Interceptors.Abstract;

namespace Nagarro.WcfClientFramework.Interceptors
{
    /// <summary>
    /// Implements the WcfClientRedirectInterceptor that is used to perform WCF channel operations.
    /// </summary>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    public class WcfClientRedirectInterceptor<TInterface> : BaseInterceptor
    {
        /// <summary>
        /// Holds the end point address.
        /// </summary>
        private string endPointAddress;

        /// <summary>
        /// Holds the host service name.
        /// </summary>
        private string hostServiceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfClientRedirectInterceptor{TInterface}"/> class.
        /// </summary>
        /// <param name="hostServiceName">Name of the host service.</param>
        public WcfClientRedirectInterceptor(string hostServiceName)
        {
            if (hostServiceName == null || hostServiceName.Trim().Length == 0)
            {
                throw new Exception("The name of the corresponding service must not be null or empty!");
            }

            // set the name of the corresponding hostService
            this.hostServiceName = hostServiceName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfClientRedirectInterceptor{TInterface}"/> class.
        /// </summary>
        /// <param name="hostServiceName">Name of the host service.</param>
        /// <param name="endPointAddress">The end point address.</param>
        public WcfClientRedirectInterceptor(string hostServiceName, string endPointAddress)
            : this(hostServiceName)
        {
            // set the optional endPointAddress
            this.endPointAddress = endPointAddress == null || endPointAddress.Trim().Length == 0 ? null : endPointAddress;
        }

        /// <summary>
        /// Implements the logic that will be executed.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected override void InnerInvocation(IInvocation invocation)
        {
            // Create an instance
            ChannelFactory<TInterface> channelFactory = null;

            try
            {
                if (this.endPointAddress == null)
                {
                    // create client with channelfactory
                    channelFactory = new ChannelFactory<TInterface>(this.hostServiceName);
                }
                else
                {
                    EndpointAddress address = new EndpointAddress(this.endPointAddress);

                    // create client with channelfactory
                    channelFactory = new ChannelFactory<TInterface>(this.hostServiceName, address);
                }

                TInterface channel = channelFactory.CreateChannel();

                object retVal = this.ServiceCall(invocation, channel);

                invocation.ReturnValue = retVal;

                channelFactory.Close();
            }
            finally
            {
                if (channelFactory != null)
                {
                    ((IDisposable)channelFactory).Dispose();
                }
            }
        }

        /// <summary>
        /// Calls the service method.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="channelObject">The channel object.</param>
        /// <returns>
        /// An instance of <see cref="object"/> representing the
        /// execution result.
        /// </returns>
        protected virtual object ServiceCall(IInvocation invocation, object channelObject)
        {
            Type type = channelObject.GetType();

            MethodInfo methodInfo = type.GetMethod(invocation.Method.Name);

            if (methodInfo == null)
            {
                foreach (var typeInterface in type.GetInterfaces())
                {
                    methodInfo = typeInterface.GetMethod(invocation.Method.Name);
                    break;
                }
            }

            if (methodInfo == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The method {0} could not be found.",
                        invocation.Method.Name));
            }

            if (invocation.Method.IsGenericMethod)
            {
                methodInfo = methodInfo.MakeGenericMethod(invocation.Method.GetGenericArguments());
            }

            return methodInfo.Invoke(
                channelObject,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                null,
                invocation.Arguments,
                CultureInfo.InvariantCulture);
        }
    }
}