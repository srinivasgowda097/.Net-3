using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using DummyService.Service;

namespace DummyService
{
    /// <summary>
    /// Implements the console host for the services.
    /// </summary>
    internal class ConsoleHost
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            ServiceHost testService = null;

            try
            {
                testService = new ServiceHost(typeof(TestService));
                testService.Faulted += (sender, eventArgs) =>
                {
                    Console.WriteLine(sender);
                    testService.Close();
                    testService.Open();
                };

                testService.Open();
                
                Console.WriteLine("TestService successfully opened!!!!");
                Console.WriteLine(testService.State);
                Console.WriteLine(testService.OpenTimeout);
                Console.WriteLine(testService);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception);
                testService.Abort();
            }

            Console.WriteLine("Press any key to terminate the host.");
            Console.ReadLine();
        }
    }
}
