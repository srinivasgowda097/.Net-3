using NUnit.Framework;

namespace Nagarro.Nunit.TestRunner.TestConsole
{
    /// <summary>
    /// The sample test.
    /// </summary>
    [TestFixture]
    public class SampleTest
    {
        [Test]
        [Description("Test to check if the test is running.")]
        public void TestMethodWork_Nothing_JustRunIt()
        {
            Assert.AreEqual(1, 2, "The generated number was not equal to exepcted value.");
        }

        [Test]
        [Description("Test to check if the test succeeds.")]
        public void TestMethodWork_Nothing_JustPassIt()
        {
            // Assert.Pass("Test failed. Because of Booomm!!!");
            Assert.AreEqual(1, 1);
        }
    }
}
