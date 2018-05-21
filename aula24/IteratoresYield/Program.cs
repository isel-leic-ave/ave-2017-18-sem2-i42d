using System;
using System.Collections.Generic;
using System.Linq;

namespace IteratoresYield
{
    class Program
    {

        static IEnumerable<int> Three()
        {
            yield return 1;
            Console.WriteLine("Step 1");
            yield return 2;
            Console.WriteLine("Step 2");
            yield return 3;
        }

        static void Main(string[] args)
        {
            foreach(int v in Three())
            {
                Console.WriteLine("Seq value = {0}", v);
            }
        }
    }
}
