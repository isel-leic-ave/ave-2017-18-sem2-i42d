using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using LoggerLib;

namespace PropertiesDemo
{
    public class MyFormatter : IFormatter
    {
        public void WriteLog(string memberName, Type memberType, object memberValue)
        {
            Console.WriteLine("** Name={0}, Type={1}, Value={2} **",
                memberName, memberType, memberValue);
        }
    }

    [Output(typeof(MyFormatter))]
    public class Properties
    {
        private int number;

        public void Print()
        {
            // leitura da propriedade
            Console.WriteLine(Number); // call get_number...
            // leitura do campo
            Console.WriteLine(number); // ldfld number...
        }

        [Ignore]
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                Console.WriteLine("Trying to write {0}", value);
            }
        }

        public int Year { get; set; }

        /*
        public int this[int idx]
        {
            get
            {
                return idx * 2;
            }
        }
        */

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("value must be >= 0");
                number = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Properties p1 = new Properties();
            p1.Number = 50;
            //       <=>
            Properties p2 = new Properties
            {
                Number = 50
            };

            // lança excepção
            // p1.Number = -1;

            //Console.WriteLine("Result of using the index property - {0} -", p1[5]);

            p1.Now = DateTime.Now.AddDays(1);

            Logger logger = Logger.Build(p1.GetType());
            logger.Log(p1);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Type t = p1.GetType();
            PropertyInfo[] props = t.GetProperties();
            Attribute attr = Attribute.GetCustomAttribute(
                t, 
                typeof(OutputAttribute)
            );
            watch.Stop();
            Console.WriteLine(watch.ElapsedTicks);

            foreach (PropertyInfo p in props)
            {
                watch.Restart();
                object v = p.GetValue(p1);
                Console.WriteLine(watch.ElapsedTicks);
            }
        }
    }
}
