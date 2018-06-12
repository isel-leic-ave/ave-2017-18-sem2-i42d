using System;


namespace FinalizeDemo
{
    class A
    {
        int[] v = new int[1024];
        ~A() { }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            A[] v = new A[64];
            Console.WriteLine(GC.GetTotalMemory(false));
            for (int i = 0; i < 64; ++i)
                v[i] = new A();
            Console.WriteLine(GC.GetTotalMemory(false));
            v = null;
            GC.Collect();
            Console.WriteLine(GC.GetTotalMemory(false));
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine(GC.GetTotalMemory(false));
        }
    }
}
