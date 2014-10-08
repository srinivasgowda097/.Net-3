using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

using SampleLib;

namespace SampleTestAssembly
{
    [TestFixture]
    public class SampleClassTest
    {
        [Description("A valid given two input values 1 and 2 must return 3")]
        [Test]
        public void Sum_ValidNumber_ReturnsSum()
        {
            Assert.AreEqual(3, new SampleClass().Sum(1, 2), "Sum operation returned value which was not expected.");
        }
    }
}
