using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using NUnit.Engine;
using NUnit.Framework;
using NUnit.Framework.Internal;
using InternalTraceLevel = NUnit.Engine.InternalTraceLevel;
using TestListener = NUnit.Framework.Internal.TestListener;

namespace Nagarro.Nunit.TestRunner
{
    public class InMemoryNunitTestRunner
    {
        /// <summary>
        /// The schema file.
        /// </summary>
        private static readonly string SchemaFile = "NUnit2TestResult.xsd";

        /// <summary>
        /// The engine.
        /// </summary>
        private ITestEngine engine;

        /// <summary>
        /// The local directory.
        /// </summary>
        private string localDirectory;

        /// <summary>
        /// Gets or sets the engine result.
        /// </summary>
        private TestEngineResult engineResult;

        /// <summary>
        /// Gets the formatted result of last execution on the Runner instance.
        /// </summary>
        public string FormattedResult
        {
            get
            {
                if (this.engineResult.Xml != null)
                {
                    return this.SummaryTransformTest();
                }

                return "No tests results found.";
            }
        }

        /// <summary>
        /// Loads the assembly that contains tests and run the tests.
        /// </summary>
        /// <param name="testAssembly">
        /// Assembly that will be containing tests
        /// </param>
        public void LoadTestAndRunningAssembly(Assembly testAssembly)
        {
            Uri uri = new Uri(testAssembly.CodeBase);
            this.localDirectory = Path.GetDirectoryName(uri.LocalPath);
            this.engine = TestEngineActivator.CreateInstance(null, InternalTraceLevel.Off);

            var settings = new Dictionary<string, object>();

            var runner = new DefaultTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            Assert.True(runner.Load(testAssembly, settings), "Unable to load Executing Assembly.");

            // Convert our own framework XmlNode to a TestEngineResult
            var package = new TestPackage(GetLocalPath(uri.AbsolutePath));

            this.engineResult = TestEngineResult.MakeTestRunResult(
                package,
                DateTime.Now,
                new TestEngineResult(
                    runner.Run(TestListener.NULL, NUnit.Framework.Internal.TestFilter.Empty).ToXml(true).OuterXml));
        }

        /// <summary>
        /// Transforms the Engine result xml to a required readable format.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string SummaryTransformTest()
        {
            var transformPath = this.GetLocalPath("Summary.xslt");
            StringWriter writer = new StringWriter();
            new XmlTransformOutputWriter(transformPath).WriteResultFile(this.engineResult.Xml, writer);

            // Arrange test counts
            string summary = string.Format(
                "Tests Run: {0}, Passed: {1}, Failed: {2}",
                this.engineResult.Xml.Attributes["total"].Value,
                this.engineResult.Xml.Attributes["passed"].Value,
                this.engineResult.Xml.Attributes["failed"].Value);
            
            // Arrange failed tests summary details
            var xml = XDocument.Load(new XmlNodeReader(this.engineResult.Xml));
            string details = string.Empty;
            IEnumerable<XElement> textSegs = xml.Descendants("test-case")
                .Where(x => x.Attribute("result").Value.Equals("Failed", StringComparison.InvariantCultureIgnoreCase));

            if (textSegs.Any())
            {
                foreach (var seg in textSegs)
                {
                    var testCaseDescription = seg.Descendants("property")
                        .FirstOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == "Description");

                    details = string.Format(
                        "\n\n\n TestCase: {0} \n Failing message: {1}",
                        testCaseDescription != null
                            ? testCaseDescription.Attribute("value").Value
                            : seg.Attributes("name").First().Value,
                        seg.Descendants("message").First().Value);
                }
            }

            return summary + details;
        }

        /// <summary>
        /// The get local path.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetLocalPath(string fileName)
        {
            return Path.Combine(this.localDirectory, fileName);
        }
    }
}
