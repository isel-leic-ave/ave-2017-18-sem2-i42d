using System;
using System.Collections.Generic;
using System.IO;

namespace Yield_and_Using
{
    class Program
    {
        static IEnumerable<int> M()
        {
            using (FileStream fs
                = new FileStream("./test.txt", FileMode.Create))
            {
                for (int i = 0; i < 3; ++i)
                {
                    fs.Write(new byte[] { (byte)i }, 0, 1);
                    yield return i;
                }
            }
        }

        static void Main(string[] args)
        {
            foreach(int i in M())
            {
                Console.WriteLine(i);
            }
        }
    }
}
