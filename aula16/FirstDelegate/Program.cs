using System;
using System.Reflection;

namespace FirstDelegate
{
    public delegate int Code(String a);

    public class Program
    {
        static void ShowInConsole(Code f, String s)
        {
            Console.WriteLine( f(s) );
            //<=>
            Console.WriteLine(f.Invoke(s));
        }
        static int StrToInt(String s)
        {
            return Int32.Parse(s);
        }

        static void Main(string[] args)
        {
            ShowInConsole(new Code(StrToInt), "123");
            //<=>
            ShowInConsole(StrToInt, "123");

            Program obj = new Program();
            ShowInConsole(new Code(obj.InstanceStrToInt), "123");
            //<=>
            ShowInConsole(obj.InstanceStrToInt, "123");

            Code code = null;
            code = new Code(obj.InstanceStrToInt);
            ShowInConsole(code, "1123");
            ShowInConsole(code, "2123");
            ShowInConsole(code, "3123");

            object target = code.Target;
            MethodInfo mtd = code.Method;

            ShowInConsole(
                delegate (String s) { return s.Length; }, 
                "123"
            );

            ShowInConsole(
                s => { return s.Length; },
                "123"
            );

            ShowInConsole(
                s => s.Length,
                "123"
            );

        }

        public int LengthToInt(String x)
        {
            return x.Length;
        }

        public int InstanceStrToInt(String s)
        {
            return Int32.Parse(s);
        }
    }
}
