using System;

interface I { }

class Employee {
    protected String name;
    protected String department;

}

class Manager : Employee { }

class Ancestors {
    public static void Show(Type t) {
        Type ot = typeof(System.Object);
        Console.WriteLine(t);
        while (!Object.ReferenceEquals(t,ot)) {
            t = t.BaseType;
            Console.WriteLine(t);
        }
    }
}

class Program
{
    public void aMethod() { 
        // No cast needed since new returns an Employee object
        // and the Object is a base type of Employee.
        Object o = new Employee();
        // Cast required since Employee is derived from Object.
        // Other languages (such as Visual Basic) might not require.
        Employee e = (Employee) o;
        if (o is Employee) {
            e = (Employee) o;
            // Use e within the 'if' statement.
        }
        e = o as Employee;
        if (e != null) {
            /* Use e within the 'if' statement. */
        }
    }

    public static void Main()
    {
        Ancestors.Show(new Manager().GetType());

        Manager m = new Manager();
        Employee e = new Employee();

        Type t1 = typeof(Manager);
        Type t2 = typeof(Employee);

        RuntimeTypeHandle rtth = t1.TypeHandle;
        Type tt = Type.GetTypeFromHandle(rtth);

        Console.WriteLine(t1.IsSubclassOf(t2));
        Console.WriteLine(t2.IsSubclassOf(t1));
        Console.WriteLine(t2.IsAssignableFrom(t1));
    }
}