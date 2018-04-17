using System;
using LoggerUtils;
using System.Diagnostics;

namespace LoggerTest
{
    public class MyClass
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P11 { get; set; }
        public string P12 { get; set; }
        public string P13 { get; set; }
        public string P21 { get; set; }
        public string P22 { get; set; }
        public string P23 { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass c = new MyClass { P1="1", P2="isel", P3="1.0" };
            ILogger log = Logger.BuildWithReflect(typeof(MyClass));

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 100; ++i) {
                log.Log(c); log.Log(c);
                log.Log(c); log.Log(c);
                log.Log(c); log.Log(c);
                log.Log(c); log.Log(c);
            }
            watch.Stop();
            double opsReflect = watch.ElapsedTicks/100.0;

            ILogger logEmit = Logger.BuildWithEmit(typeof(MyClass));
            watch.Restart();
            for (int i = 0; i < 100; ++i)
            {
                logEmit.Log(c); logEmit.Log(c);
                logEmit.Log(c); logEmit.Log(c);
                logEmit.Log(c); logEmit.Log(c);
                logEmit.Log(c); logEmit.Log(c);
            }
            watch.Stop();
            double opsEmit = watch.ElapsedTicks / 100.0;

            Console.WriteLine("*** Reflect ops = {0}", opsReflect);
            Console.WriteLine("*** Emit ops = {0}", opsEmit);
        }
    }
}
