using System;

namespace MethodDispatch
{
    interface ISomething
    {
        void M();
        void T();
    }

    class A : ISomething
    {
        public virtual void M()
        {
            Console.WriteLine("A.M");
        }

        public void T() { Console.WriteLine("A.T"); }

        public virtual void OtherM() { }

        public virtual void Operation() { }

    }

    sealed class B : A
    {
        public override void M()
        {
            Console.WriteLine("B.M");
        }

        public new void T() { Console.WriteLine("B.T"); }

        public void W() { }

        public override void Operation()
        {
            Console.WriteLine("@B");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ISomething i = new B();
            i.M();          // callvirt
            i.T();          // callvirt
            B b = new B();
            b.W();          // call
            Z z = new Z();
            z.M();
        }
    }

    class Z
    {
        public void M() { }
    }
}
