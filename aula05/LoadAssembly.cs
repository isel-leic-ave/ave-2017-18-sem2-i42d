using System;
using System.Reflection;

public class Loader
{

    public static void Main(String[] args)
    {
        Assembly assembly = Assembly.LoadFrom(args[0]);
        Console.WriteLine(assembly.GetName());
        Module[] modules = assembly.GetModules();
        for (int i=0; i<modules.Length; ++i)
        {
            Type[] types = modules[i].GetTypes();
            for (int j=0; j<types.Length; ++j)
            {
                Console.WriteLine(types[j].Name);
            }
        }
    }
}