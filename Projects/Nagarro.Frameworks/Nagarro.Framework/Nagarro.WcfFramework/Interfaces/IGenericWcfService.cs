using System.Collections.Generic;
using System.ServiceModel;

namespace Nagarro.WcfFramework.Interfaces
{
    /// <summary>
    /// Interface definition for the
    /// generic wcf service.
    /// </summary>
    [ServiceContract]
    public interface IGenericWcfService
    {
        /// <summary>
        /// Inokes the generic method using the
        /// given parameters.
        /// </summary>
        /// <param name="genericMethodName">Name of the generic method.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// An instance of <see cref="object"/> representing the
        /// execution result
        /// </returns>
        [OperationContract]
        object HiddenMethod_InvokeGenericMethod(
            string genericMethodName,
            IList<object> genericTypes,
            IList<object> parameters);
    }
}
