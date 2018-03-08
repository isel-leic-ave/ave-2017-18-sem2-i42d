using System;
using System.Reflection;
using PropertiesDemo;

namespace Logger
{
    class Logger
    {

        public static void Log(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = 
                t.GetProperties();
            // <=>
            properties = t.GetProperties(
                BindingFlags.Public | 
                BindingFlags.Instance |
                BindingFlags.Static);
            foreach(PropertyInfo p in properties)
            {
                Console.WriteLine(p.Name);
            }
        }

        static void Main(string[] args)
        {
            Properties p = new Properties();
            Logger.Log(p);
        }
    }
}
