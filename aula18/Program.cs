using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBench
{
    class Program
    {
        public static int foo()
        {
            int r = 2;
            for (int i = 0; i < 100; ++i)
                r *= i;
            return r;
        }

        public static String bar()

        {

            String a = "ave ";
            for (int i = 0; i < 100; ++i)
                a += i;
            return a;
        }

        public static void TestFoo()
        {
            foo();
        }

        public static void TestBar()
        {
            bar();
        }

        public static void Main()
        {
            AVE1718v.NBench.Bench(new Action(TestFoo), "foo");
            AVE1718v.NBench.Bench(new Action(TestBar), "bar");
        }
    }
}
