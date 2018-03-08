using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesDemo
{
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

        public int this[int idx]
        {
            get
            {
                return idx * 2;
            }
        }

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

            Console.WriteLine("Result of using the index property - {0} -", p1[5]);

            p1.Now = DateTime.Now.AddDays(1);
        }
    }
}
