using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.WcfClientFramework.Tests.Helper
{
    /// <summary>
    /// Invokes the process that host the test service required for testing wcf client.
    /// </summary>
    internal class ServiceHostProcessInvoker
    {
        /// <summary>
        /// Invokes the host process for test service
        /// </summary>
        public static void InvokeDummyService()
        {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(Environment.CurrentDirectory, "DummyService.exe"));

            info.UseShellExecute = true;
            info.WorkingDirectory = Environment.CurrentDirectory;

            Process.Start(info);
        }

        /// <summary>
        /// Kills the process of service host
        /// </summary>
        public static void KillDummyService()
        {
            Process.GetProcessesByName("DummyService").ToList().ForEach(x => x.Kill());
        }
    }
}
