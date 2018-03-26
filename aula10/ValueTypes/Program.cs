using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueTypes
{
    struct SomeValue /* ValueType */
    {
        public int a;
        public String b;
        public SomeValue(int a, String b)
        {
            this.a = a;
            this.b = b;
        }
        public bool Equals(SomeValue v)
        {
            return a == v.a && b.Equals(v.b);
        }
    }

    class SomeClass
    {
        public int a;
        public String b;
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, this))
                return true;
            SomeClass other = obj as SomeClass;
            if (other == null)
                return false;
            return this.a == other.a && this.b.Equals(other.b);
        }
        public override int GetHashCode()
        {
            return a ^ b.GetHashCode();
        }
    }

    class Program
    {
        static void BoxTest()
        {
            int i = 10;
            object o = i;
            i = 20;


            SomeValue v = new SomeValue();
            object o2 = v;

            SomeValue sv = new SomeValue();
            Console.WriteLine(sv.ToString());

            int j = (int)o;
        }


        static void Main(string[] args)
        {
            SomeValue v1 = new SomeValue(10, "aaa");
            v1.a = 10;
            v1.b = new string(new char[] { 'a', 'b', 'b' });

            SomeValue v2 = new SomeValue();
            v2.a = 10;
            v2.b = new string(new char[] { 'a', 'b', 'b' });

            SomeClass c1 = new SomeClass();
            c1.a = 10;
            c1.b = new string(new char[] { 'a', 'b', 'b' });

            SomeClass c2 = new SomeClass();
            c2.a = 10;
            c2.b = new string(new char[] { 'a', 'b', 'b' });

            /*if (v1 == v2)
            {
                Console.WriteLine("c1 and c2 are ==");
            }*/

            if (v1.Equals(v2))
            {
                Console.WriteLine("v1 and v2 are Equal");
            }

            if (c1.Equals(c2) 
            /* se SomeClass não redefinir Equals(object o) <=> Object.ReferenceEquals(c1,c2) */)
            {
                Console.WriteLine("c1 and c2 are Equal");
            }

        }
    }
}
