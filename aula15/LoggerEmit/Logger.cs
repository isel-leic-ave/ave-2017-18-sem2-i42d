using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LoggerUtils
{

    /*------------------------------------
     * ex: MyClass
     *  
     */
    public class MyClass
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
    }
    class LoggerMyClass : ILogger
    {
        public void Log(object o)
        {
            MyClass x = (MyClass)o;
            Console.WriteLine(x.P1); //x.get_P1()
            Console.WriteLine(x.P2);
            Console.WriteLine(x.P3);
        }
    }
    /*------------------------------------*/

    public interface ILogger
    {
        void Log(object o);
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class IgnoreAttribute : Attribute
    {

    }

    public class Logger : ILogger
    {

        public static ILogger BuildWithEmit(Type t)
        {

            /**
             * TODO: Check if a Logger for the type represented by 't' was already emited.
             */

            AssemblyName aName = new AssemblyName("AssemblyLogger"+t.Name);
            AssemblyBuilder ab =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder mb =
                ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

            TypeBuilder tb = mb.DefineType(
                "Logger" + t.Name,
                 TypeAttributes.Public);

            tb.AddInterfaceImplementation(typeof(ILogger));

            MethodBuilder mtb = tb.DefineMethod(
                "Log",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(void),
                new Type[] { typeof(object) });

            ILGenerator gen = mtb.GetILGenerator();

            // declare local variable of type represeted by 't'
            gen.DeclareLocal(t); // loc_0

            // put on stack first argument and make cast
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Castclass, t);
            gen.Emit(OpCodes.Stloc_0);

            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props)
            {
                // load local variable
                gen.Emit(OpCodes.Ldloc_0);

                // read property
                gen.Emit(OpCodes.Call, p.GetGetMethod());

               // write to console
               gen.Emit(OpCodes.Call, 
                typeof(Console).GetMethod(
                        "WriteLine",
                        new Type[] { p.PropertyType }));
            }
            gen.Emit(OpCodes.Ret);

            Type newLoggerType = tb.CreateType();

            ab.Save(aName.Name + ".dll");

            return (ILogger) Activator.CreateInstance(newLoggerType);
        }


        public static ILogger BuildWithReflect(Type t)
        {
            return new Logger(t);
        }

        private Type type;
        private PropertyInfo[] filteredProperties;

        private Logger(Type t)
        {
            type = t;

            Type ignoreType = typeof(IgnoreAttribute);

            PropertyInfo[] props = t.GetProperties();
            List<PropertyInfo> relevantProps = new List<PropertyInfo>();
            foreach (PropertyInfo p in props)
            {
                if (!Attribute.IsDefined(p, ignoreType))
                {
                    relevantProps.Add(p);
                }
            }
            filteredProperties = relevantProps.ToArray();
        }

        public void Log(object obj)
        {
            if (obj == null || obj.GetType() != type) return;
            foreach (PropertyInfo p in filteredProperties)
            {
                object value = p.GetValue(obj);
                Console.WriteLine("{0}", value);
                // p.Name, p.PropertyType, 
            }
        }

    }
}
