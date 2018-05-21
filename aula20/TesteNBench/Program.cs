using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBench
{
    delegate int MyDelegate(string s, object o);

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

        public void InstanceMethod()
        {

        }

        public static void Main()
        {
            /*AVE1718v.NBench.Bench(new Action(TestFoo), "foo");
            AVE1718v.NBench.Bench(TestFoo, "foo");

            AVE1718v.NBench.Bench(
                () => foo(), 
                "foo");

            AVE1718v.NBench.Bench(new Action(TestBar), "bar");
            */

            Action a1 = () => Console.WriteLine("a1");
            Action a2 = () => Console.WriteLine("a2");
            Action a3 = () => Console.WriteLine("a3");

            Action all = a1 + a2;
            all += a3;

            Program p = new Program();
            all += new Action(p.InstanceMethod);

            int v = 10;

            Action print = () => Console.WriteLine(v);

            v = 20;

            print();
        }
    }
}
