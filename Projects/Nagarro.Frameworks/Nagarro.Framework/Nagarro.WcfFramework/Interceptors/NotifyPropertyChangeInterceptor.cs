using System;
using System.Collections.Generic;
using System.ComponentModel;

using Castle.DynamicProxy;

namespace Nagarro.WcfFramework.Interceptors
{
    /// <summary>
    /// Implements the NotifyPropertyChangeInterceptor that extends a class with 
    /// the <see cref="INotifyPropertyChanging"/> and <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class NotifyPropertyChangeInterceptor : BaseInterceptor
    {
        /// <summary>
        /// Holds the cache for property changed event arguments.
        /// </summary>
        private static Dictionary<string, PropertyChangedEventArgs> propertyChangedCache =
            new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// Holds the cache for property changing event arguments.
        /// </summary>
        private static Dictionary<string, PropertyChangingEventArgs> propertyChangingCache =
            new Dictionary<string, PropertyChangingEventArgs>();

        /// <summary>
        /// Holds the property changed handler.
        /// </summary>
        private PropertyChangedEventHandler propertyChangedHandler;

        /// <summary>
        /// Holds the property changing handler.
        /// </summary>
        private PropertyChangingEventHandler propertyChangingHandler;

        /// <summary>
        /// Gets the list of additional interfaces to extend a class with.
        /// </summary>
        public static Type[] AdditionalInterfaces
        {
            get { return new[] { typeof(INotifyPropertyChanged), typeof(INotifyPropertyChanging) }; }
        }

        /// <summary>
        /// Implements the logic that
        /// will be executed after the
        /// invocation proceeded successfully.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        protected override void InnerInvocation(IInvocation invocation)
        {
            switch (invocation.Method.Name)
            {
                case "add_PropertyChanging":
                    this.propertyChangingHandler =
                        (PropertyChangingEventHandler)
                        Delegate.Combine(this.propertyChangingHandler, (Delegate)invocation.Arguments[0]);

                    invocation.ReturnValue = this.propertyChangingHandler;
                    break;
                case "add_PropertyChanged":
                    this.propertyChangedHandler =
                        (PropertyChangedEventHandler)
                        Delegate.Combine(this.propertyChangedHandler, (Delegate)invocation.Arguments[0]);

                    invocation.ReturnValue = this.propertyChangedHandler;
                    break;
                case "remove_PropertyChanging":
                    this.propertyChangingHandler =
                        (PropertyChangingEventHandler)
                        Delegate.Remove(this.propertyChangingHandler, (Delegate)invocation.Arguments[0]);

                    invocation.ReturnValue = this.propertyChangingHandler;
                    break;
                case "remove_PropertyChanged":
                    this.propertyChangedHandler =
                        (PropertyChangedEventHandler)
                        Delegate.Remove(this.propertyChangedHandler, (Delegate)invocation.Arguments[0]);

                    invocation.ReturnValue = this.propertyChangedHandler;
                    break;
                default:
                    if (invocation.Method.Name.StartsWith("set_"))
                    {
                        // Launch the event before the execution
                        if (this.propertyChangingHandler != null)
                        {
                            PropertyChangingEventArgs arg = this.RetrievePropertyChangingArgument(invocation.Method.Name);
                            this.propertyChangingHandler(invocation.Proxy, arg);
                        }

                        // Do the setter execution
                        base.InnerInvocation(invocation);

                        // Launch the event after the execution
                        if (this.propertyChangedHandler != null)
                        {
                            PropertyChangedEventArgs arg = this.RetrievePropertyChangedArgument(invocation.Method.Name);
                            this.propertyChangedHandler(invocation.Proxy, arg);
                        }
                    }
                    else
                    {
                        base.InnerInvocation(invocation);
                    }

                    break;
            }
        }

        /// <summary>
        /// Retrieves the property changed argument.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The property changed argument.</returns>
        private PropertyChangedEventArgs RetrievePropertyChangedArgument(string methodName)
        {
            PropertyChangedEventArgs argument;
            propertyChangedCache.TryGetValue(methodName, out argument);

            if (argument == null)
            {
                argument = new PropertyChangedEventArgs(methodName.Substring(4));
                propertyChangedCache.Add(methodName, argument);
            }

            return argument;
        }

        /// <summary>
        /// Retrieves the property changing argument.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The property changing argument.</returns>
        private PropertyChangingEventArgs RetrievePropertyChangingArgument(string methodName)
        {
            PropertyChangingEventArgs argument;
            propertyChangingCache.TryGetValue(methodName, out argument);

            if (argument == null)
            {
                argument = new PropertyChangingEventArgs(methodName.Substring(4));
                propertyChangingCache.Add(methodName, argument);
            }

            return argument;
        }
    }
}