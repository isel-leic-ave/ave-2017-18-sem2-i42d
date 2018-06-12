using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagedheapDemo
{
    class X
    {
        int[] a1 = new int[32];
        int[] a2 = new int[32];
        int[] a3 = new int[32];
    }
    class Program
    {
        static X[] arr = new X[10];
        static void Main(string[] args)
        {
            int counter = 0;
            for (; ; )
            {
                Thread.Sleep(10);
                arr[counter % 10] = new X();
            }
        }
    }
}
