using System;

using Castle.DynamicProxy;

using Nagarro.ProxyWrapperGenerator.Interceptors.Abstract;
using Nagarro.ProxyWrapperGenerator.InterceptorsHooks;

namespace Nagarro.ProxyWrapperGenerator
{
    /// <summary>
    /// Factory for the proxies.
    /// </summary>
    public static class ProxyFactory
    {
        /// <summary>
        /// Holds the proxy generator.
        /// </summary>
        private static ProxyGenerator proxyGenerator = new ProxyGenerator();

        /// <summary>
        /// Gets the interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="target">The target to generate to proxy for.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// An proxy from the interface with the type <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetInterfaceProxy<TInterface>(TInterface target, params BaseInterceptor[] interceptors)
        {
            return GetInterfaceProxy(target, (IInterceptor[])interceptors);
        }

        /// <summary>
        /// Gets the interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="target">The target to generate to proxy for.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// An proxy from the interface with the type <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetInterfaceProxy<TInterface>(TInterface target, params IInterceptor[] interceptors)
        {
            return GetInterfaceProxy(target, new DefaultInterceptorHook(), interceptors);
        }

        /// <summary>
        /// Gets the interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="target">The target to generate to proxy for.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// An proxy from the interface with the type <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetInterfaceProxy<TInterface>(TInterface target, IProxyGenerationHook proxyGenerationHook, params BaseInterceptor[] interceptors)
        {
            return GetInterfaceProxy(target, proxyGenerationHook, (IInterceptor[])interceptors);
        }

        /// <summary>
        /// Gets the interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="target">The target to generate to proxy for.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// An proxy from the interface with the type <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetInterfaceProxy<TInterface>(TInterface target, IProxyGenerationHook proxyGenerationHook, params IInterceptor[] interceptors)
        {
            return (TInterface)proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), target, new ProxyGenerationOptions(proxyGenerationHook), interceptors);
        }

        /// <summary>
        /// Gets the interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// An proxy from the interface with the type <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetInterfaceProxy<TInterface>(IProxyGenerationHook proxyGenerationHook, params IInterceptor[] interceptors)
        {
            return (TInterface)proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(TInterface), new ProxyGenerationOptions(proxyGenerationHook), interceptors);
        }

        /// <summary>
        /// Gets the interface proxy.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// An proxy from the interface with the type <see cref="TInterface"/>.
        /// </returns>
        public static TInterface GetInterfaceProxy<TInterface>(params IInterceptor[] interceptors)
        {
            return GetInterfaceProxy<TInterface>(new DefaultInterceptorHook(), interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(TClass target, params BaseInterceptor[] interceptors) where TClass : class
        {
            return GetClassProxy(target, (IInterceptor[]) interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(TClass target, params IInterceptor[] interceptors) where TClass : class
        {
            return GetClassProxy(target, new DefaultInterceptorHook(), interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(TClass target, IProxyGenerationHook proxyGenerationHook, params BaseInterceptor[] interceptors) where TClass : class
        {
            return GetClassProxy(target, proxyGenerationHook, (IInterceptor[]) interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(TClass target, IProxyGenerationHook proxyGenerationHook, params IInterceptor[] interceptors) where TClass : class
        {
            return (TClass)proxyGenerator.CreateClassProxyWithTarget(typeof(TClass), target, new ProxyGenerationOptions(proxyGenerationHook), interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="additionalInterfacesToProxy">The additional interfaces to proxy.</param>
        /// <param name="target">The target.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(Type[] additionalInterfacesToProxy, TClass target, params IInterceptor[] interceptors) where TClass : class
        {
            return (TClass)proxyGenerator.CreateClassProxyWithTarget(
                typeof(TClass),
                additionalInterfacesToProxy,
                target,
                interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="proxyGenerationHook">The proxy generation hook.</param>
        /// <param name="constructorParameters">The constructor parameters.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(TClass target, IProxyGenerationHook proxyGenerationHook, object[] constructorParameters, params IInterceptor[] interceptors) where TClass : class
        {
            return (TClass)proxyGenerator.CreateClassProxyWithTarget(typeof(TClass), target, new ProxyGenerationOptions(proxyGenerationHook), constructorParameters, interceptors);
        }

        /// <summary>
        /// Gets the class proxy.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="constructorParameters">The constructor parameters.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>
        /// A proxy from the class with the type <see cref="TClass"/>.
        /// </returns>
        public static TClass GetClassProxy<TClass>(TClass target, object[] constructorParameters, params IInterceptor[] interceptors) where TClass : class
        {
            return (TClass)proxyGenerator.CreateClassProxyWithTarget(typeof(TClass), target, constructorParameters, interceptors);
        }
    }
}
