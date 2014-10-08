using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

using NUnit.Framework.Internal;

namespace Nagarro.Nunit.TestRunner
{
    public class Program
    {
        static void Main(string[] args)
        {
            CoreExtensions.Host.InstallBuiltins();

            Assembly testAssembly = Assembly.Load("SampleTestAssembly");

            new InMemoryNunitTestRunner().LoadTestAndRunningAssembly(testAssembly);

            Console.Read();
        }
    }
}
