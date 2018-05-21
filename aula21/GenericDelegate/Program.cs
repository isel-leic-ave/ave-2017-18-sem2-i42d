using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegate
{

    delegate void Action(int a);

    delegate W SomeFunc<T, W>(T p);

    class Program
    {
        static void Main(string[] args)
        {
            SomeFunc<String, int> f1 =
                (x) => Int32.Parse(x);

            SomeFunc<int, bool> f2 =
                (y) => true;

            string[] input = { "123", "10", "7" };

            Converter<string, int> conv =
                (s) => Int32.Parse(s);

            int[] output =
                Array.ConvertAll(
                    input,
                    conv
                );
        }
    }
}
