using System;

interface Ia {

}

interface Ib {

}

class TypeOf : Object, Ia, Ib {

    public static void Main(String[] args) {
        Type t1 = typeof(TypeOf);
        Console.WriteLine(t1);

        Type t2 = new TypeOf().GetType();
        Console.WriteLine(t2);

        if (Object.ReferenceEquals(t1,t2)) {
            Console.WriteLine("The same type.");
        }

        Type t3 = t2.GetType();
        Console.WriteLine(t3);

    }
}