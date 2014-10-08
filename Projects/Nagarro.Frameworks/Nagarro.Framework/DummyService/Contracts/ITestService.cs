using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DummyService.Contracts
{
    /// <summary>
    /// The TestService interface.
    /// </summary>
    [ServiceContract]
    public interface ITestService
    {
        /// <summary>
        /// The say hello.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [OperationContract]
        string SayHello();

        /// <summary>
        /// The throws an exception.
        /// </summary>
        [OperationContract]
        void MethodThatThrowsAnException();
    }
}
