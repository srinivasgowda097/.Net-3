using System;
using System.ServiceModel;

using DummyService.Contracts;

namespace DummyService.Service
{
    /// <summary>
    /// The test service.
    /// </summary>
    public class TestService : ITestService
    {
        /// <summary>
        /// The say hello.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SayHello()
        {
            return "Hello, I'm serving you via in-memory. I'll be gone once the business finished.";
        }

        /// <summary>
        /// The throws an exception.
        /// </summary>
        /// <exception cref="NotImplementedException"> Throws a custome exception
        /// </exception>
        public void MethodThatThrowsAnException()
        {
            throw new FaultException<System.NotImplementedException>(new NotImplementedException());
        }
    }
}
