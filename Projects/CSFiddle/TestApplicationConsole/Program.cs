using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cshandler.ScriptEngine;
using Cshandler.CompilationExecutionEngine;

namespace TestApplicationConsole
{
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /*
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            var host = new ScriptingHost();
            System.Diagnostics.Trace.Write("And it's started.");
            string codeLine;
            Console.Write(">");
            while ((codeLine = Console.ReadLine()) != "Exit();")
            {
                try
                {
                    // Execute the code 
                    var res = host.Execute(codeLine);

                    // Write the result back to console 
                    if (res != null)
                    {
                        Console.WriteLine(" = " + res.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(" !! " + e.Message);
                }

                Console.Write(">");
            }
        } */

        static void Main(string[] args)
        {
            string code = "public class Test{" + "public string SayHello() {" + "return \"Hi, WWW.\"; " + "}" + "}";
            string mainBlock = "Test test = new Test(); " + "Print(test.SayHello());";
                
            CompilerEngine compiler = new CompilerEngine(code, mainBlock);
            Console.WriteLine(compiler.EndToEndCompileAndRun());
            Console.Read();
        }
    }
}
