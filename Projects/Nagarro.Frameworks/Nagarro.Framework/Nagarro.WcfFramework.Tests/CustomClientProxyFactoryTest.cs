using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DummyService.Contracts;
using Nagarro.WcfClientFramework.Tests.Helper;
using NUnit.Framework;

namespace Nagarro.WcfClientFramework.Tests
{
    /// <summary>
    /// Implements the tests for CustomClientProxyFactory class. The tests have falvour of integration
    /// as we're testing it on dummy service client.
    /// </summary>
    [TestFixture]
    public class CustomClientProxyFactoryTest
    {
        /// <summary>
        /// Setup required before the tests of the fixture will run.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            ServiceHostProcessInvoker.InvokeDummyService();
        }

        /// <summary>
        /// Tear down to perform clean when the execution is finished.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            ServiceHostProcessInvoker.KillDummyService();
        }

        /// <summary>
        /// Case when the method GetDefaultErrorLoggingInterfaceProxy was invoked to get the real 
        /// proxy of a test service, succeeds.
        /// </summary>
        [Test]
        public void GetDefaultWCFClientProxy_RequestingProxyClientGeneration_Succeeds()
        {
            var proxy = CustomClientProxyFactory.GetDefaultWcfClientInterfaceProxy<ITestService>("TestService");

            var result = proxy.SayHello();

            Assert.IsNotNullOrEmpty(result, "The result was null or empty.");
        }

        /// <summary>
        /// Case when the GetDefaultErrorLoggingClassProxy was invoked to generate a proxy client with 
        /// Error logging interceptors.
        /// </summary>
        [Test]
        public void GetDefaultErrorLoggingClassProxy_RequestProxyClientGenerationAndInvocation_Succeeds()
        {
            var loggerTeststub = new LoggerTestExposal();

            var proxy =
                CustomClientProxyFactory.GetDefaultErrorLoggingInterfaceProxy(
                    CustomClientProxyFactory.GetDefaultWcfClientInterfaceProxy<ITestService>("TestService"),
                    loggerTeststub);

            // Act
            Assert.Throws<TargetInvocationException>(proxy.MethodThatThrowsAnException);

            Assert.IsNotNullOrEmpty(loggerTeststub.LoggedMessage);
        }
    }
}
