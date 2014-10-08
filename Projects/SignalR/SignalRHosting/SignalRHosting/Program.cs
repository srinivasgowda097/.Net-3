using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SignalRHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start("http://localhost:5000"))
            {
                Console.WriteLine("Server running at http://localhost:5000/");
                Console.ReadLine();
            }
        }
    }
}
