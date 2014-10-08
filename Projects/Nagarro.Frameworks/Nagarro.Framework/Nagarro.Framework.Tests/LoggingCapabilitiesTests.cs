using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Nagarro.Framework.Logging;

namespace Nagarro.Framework.Tests
{
    public class LoggingCapabilitiesTests
    {
        /// <summary>
        /// Stub for the logging handler
        /// </summary>
        private Action<string> loggingHandlerStub;

        /// <summary>
        /// string value updated by the handler for logging operation.
        /// </summary>
        private string writtenContent = string.Empty;

        /// <summary>
        /// Setup to run before running any test in the current fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void InitialzingTestFixture()
        {
            this.loggingHandlerStub = s => { this.writtenContent = s; };
        }

        /// <summary>
        /// Setup to run before running each test
        /// </summary>
        [SetUp]
        public void InitializeBeforeRunningEachTest()
        {
            LoggingCapabilities.RemoveAllLogHandler();
            this.writtenContent = string.Empty;

        }

        /// <summary>
        /// Clean up to perform after each test.
        /// </summary>
        [TearDown]
        public void CleanUpAfterRunningEachTest()
        {
            LoggingCapabilities.RemoveLogHandler(this.loggingHandlerStub);
        }

        /// <summary>
        /// The a_ execute_ register new logging handler_ success.
        /// </summary>
        [Test]
        public void A_Execute_RegisterNewLoggingHandler_Success()
        {
            // arrange
            LoggingCapabilities.AddLogHandler(this.loggingHandlerStub);
            int expected = 1;

            // act
            var result = LoggingCapabilities.GetLoggingHandlers().Count;

            // assert
            Assert.AreEqual(expected, result, "No LoggingHandler in the list!");
        }

        /// <summary>
        /// The b_ execute_ register new logging handler_ writes to the log file.
        /// </summary>
        [Test]
        public void B_Execute_RegisterNewLoggingHandler_WritesTotheLogFile()
        {
            // arrange
            LoggingCapabilities.AddLogHandler(this.loggingHandlerStub);
            var messageToLog = "HELLO WORLD!";

            // act
            LoggingCapabilities.LogSync(messageToLog);

            // assert
            Assert.AreEqual(messageToLog, this.writtenContent, "Content wasn't logged correctly.");
        }

        /// <summary>
        /// The c_ execute_ register new logging handler with async_ writes to the log file.
        /// </summary>
        [Test]
        public void C_Execute_RegisterNewLoggingHandlerWithAsync_WritesTotheLogFile()
        {
            // arrange
            LoggingCapabilities.AddLogHandler(this.loggingHandlerStub);
            LoggingCapabilities.AsyncResults.Clear();
            var messageToLog = "HELLO WORLD Async!";

            // act
            LoggingCapabilities.LogAsync(messageToLog);

            // Wait for the async operation
            while(LoggingCapabilities.AsyncResults.Any(x => !x.IsCompleted))
            {
                continue;
            }

            Assert.AreEqual(messageToLog, this.writtenContent, "Content wasn't logged correctly.");
        } 
    }
}
