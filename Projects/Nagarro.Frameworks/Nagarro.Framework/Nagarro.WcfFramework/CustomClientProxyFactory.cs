using System;

using Castle.DynamicProxy;

using Nagarro.Framework.Logging;
using Nagarro.Framework.Logging.Interfaces;
using Nagarro.ProxyWrapperGenerator;
using Nagarro.ProxyWrapperGenerator.Interceptors;
using Nagarro.WcfClientFramework.Interceptors;

namespace Nagarro.WcfClientFramework
{
    /// <summary>
    /// Factory for the specialized proxies of wcf client.
    /// </summary>
    public static class CustomClientProxyFactory
    {
        /// <summary>
        /// Gets the default WCF client proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="hostServiceName">Name of the host service.</param>
        /// <returns>The proxified interface.</returns>
        public static TInterface GetDefaultWcfClientInterfaceProxy<TInterface>(string hostServiceName)
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new NotSupportedException("The type to proxy has to be an interface.");
            }

            return ProxyFactory.GetInterfaceProxy<TInterface>(new WcfClientRedirectInterceptor<TInterface>(hostServiceName));
        }


        /// <summary>
        /// Gets the default error logging interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="log">The log to use.</param>
        /// <returns>
        /// An instance of the <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetDefaultErrorLoggingInterfaceProxy<TInterface>(TInterface target, ILog log)
        {
            return ProxyFactory.GetInterfaceProxy(target, new DefaultExceptionLoggingInterceptor(log));
        }

        /// <summary>
        /// Gets the default error logging interface proxy provided with custom hooks.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="log">The log to use.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <returns>
        /// An instance of the <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetDefaultErrorLoggingInterfaceProxy<TInterface>(TInterface target, ILog log, IProxyGenerationHook proxyGenerationHook)
        {
            return ProxyFactory.GetInterfaceProxy(target, proxyGenerationHook, new DefaultExceptionLoggingInterceptor(log));
        }

        /// <summary>
        /// Gets the default error logging class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="log">The log to use.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <returns>
        /// An instance of the <see cref="TClass"/>.
        /// </returns>
        public static TClass GetDefaultErrorLoggingClassProxy<TClass>(TClass target, ILog log, IProxyGenerationHook proxyGenerationHook) where TClass : class
        {
            return ProxyFactory.GetClassProxy(target, proxyGenerationHook, new DefaultExceptionLoggingInterceptor(log));
        }

        /// <summary>
        /// Gets a proxy that logs the traffic to a specified service.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service.</typeparam>
        /// <param name="targetInterface">The instance that is intedned ot be proxied.</param>
        /// <param name="trafficLogger">The logger instance that is used for logging.</param>
        /// <returns>A proxied instance of the service.</returns>
        public static TInterface GetTrafficLoggingInterfaceProxy<TInterface>(TInterface targetInterface, ILog trafficLogger)
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new NotSupportedException("The type to proxy has to be an interface.");
            }

            if (trafficLogger == null)
            {
                throw new ArgumentNullException("trafficLogger");
            }

            return ProxyFactory.GetInterfaceProxy(targetInterface, new LogTrafficInterceptor(trafficLogger));
        }
    }
}