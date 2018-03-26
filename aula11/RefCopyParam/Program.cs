using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefCopyParam
{
    class Program
    {
        public static void ByCopy(int a, String b)
        {

        }
        public static void ByRef(ref int x, ref String b)
        {
            x = 20;
            b = "ave";
        }
        public static bool ByCopy_Ref_Out(out int x, ref String b, int i)
        {
            x = 10;
            b = "ave";
            return true;
        }

        static void Main(string[] args)
        {
            String x1 = "poo";
            int a1=10;
            ByCopy(a1, x1);

            String x2 = "poo";
            int a2 = 10;
            ByRef(ref a2, ref x2);

            String x3 = "poo";
            int a3;
            ByCopy_Ref_Out(out a3, ref x3, a1);
        }
    }
}
