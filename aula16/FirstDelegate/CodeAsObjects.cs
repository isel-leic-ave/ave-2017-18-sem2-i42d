using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDelegate
{
    public interface ICode
    {
        int run();
    }

    public class X : ICode
    {
        public int run()
        {
            return 1;
        }
    }

    class CodeAsObject
    {
        static void ShowInConsole(ICode func)
        {
            Console.WriteLine(func.run());
        }

        public static void foo()
        {
            ShowInConsole(new X()); // objecto função
        }
    }
}
