using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variance
{
    class A { }

    class B : A{ }

    class C : B { }

    delegate void DoSomething(C a);

    delegate A DoSomethingRet(C a);

    class Program
    {
        static void M(C a) { }

        static void W(A a) { }

        static B T(C c) { return new B(); }

        static void Main(string[] args)
        {
            DoSomething d1 = new DoSomething(W);

            d1(new C());

            DoSomethingRet d2 = new DoSomethingRet(T);

            A a = d2(new C());
        }
    }
}
