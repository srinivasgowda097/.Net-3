using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cshandler.ScriptEngine
{
    public class Report
    {
        // Internal state
        private readonly List<int> values = new List<int>();
        private int result;

        // Encapsulate the internal data structure
        public void AddValue(int value)
        {
            values.Add(value);
        }

        // Allow read-only enumeration of values
        public IEnumerable<int> Values { get { return values; } }

        // User code will provide the implementation for these methods
        public Action GetValues;
        public Func<int> CalculateResult;

        // This method may be called by both the host application and the user code
        public void PrintResult()
        {
            var get = GetValues;
            if (get != null)
            {
                get();
            }

            var calc = CalculateResult;
            if (calc != null)
            {
                result = calc();
            }

            Console.WriteLine("The result of the calculation is {0}", result);
        }
    }
}
