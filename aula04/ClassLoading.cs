public class B
{
    static B()
    {
        System.Console.WriteLine("@B.cctor");
    }
}

public class C
{
    public int field = 10;
    static C()
    {
        System.Console.WriteLine("@C.cctor");
    }
}

public class D
{
    static D()
    {
        System.Console.WriteLine("@D.cctor");
    }
}

public class A
{
    static A()
    {
        System.Console.WriteLine("@A.cctor");
    }

    public static void M(C c, D d)
    {
        System.Console.WriteLine("@A.M");
        if (c!=null && c.field == 10)
        {
            System.Console.WriteLine("@A.M c != null");
        }
    }

    public static void foo() {
        System.Console.WriteLine("@foo");
    }

    private static void bar() {
        System.Console.WriteLine("@bar");
    }

    public static void Main(string[] args)
    {
        System.Console.WriteLine("@A.Main");
        A.foo();
        System.Console.ReadLine();
        A.M(null, null);
        System.Console.ReadLine();
    }
}