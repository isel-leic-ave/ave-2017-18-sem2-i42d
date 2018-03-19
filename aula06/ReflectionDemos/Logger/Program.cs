using System;
using System.Reflection;

namespace LoggerLib
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Method)]
    public class IgnoreAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class OutputAttribute : Attribute
    {
        public readonly Type Formatter;
        public OutputAttribute(Type formatter)
        {
            Formatter = formatter;
        }
    }

    public interface IFormatter
    {
        void WriteLog(
            String memberName, 
            Type memberType, 
            object memberValue);
    }

    class DefaultFormatter : IFormatter
    {
        public void WriteLog(String name, Type type, object value)
        {
            Console.WriteLine("{0}({2}) = {1}", name, value, type);
        }
    }

    public class Logger
    {

        public static void Log(object obj)
        {
            if (obj == null) return;
            Type t = obj.GetType();
            PropertyInfo[] properties = 
                t.GetProperties();
            // <=>
            properties = t.GetProperties(
                BindingFlags.Public | 
                BindingFlags.Instance |
                BindingFlags.Static);
            Type ignoreType = typeof(IgnoreAttribute);
            Type outputType = typeof(OutputAttribute);

            // variable to store a reference to a formatter
            IFormatter formatter = null;
            // check if formatter is used for 't'
            Attribute attr =
                    Attribute.GetCustomAttribute(t, outputType);
            if (attr != null)
            {
                OutputAttribute outAttr = (OutputAttribute)attr;
                Type formatterType = outAttr.Formatter;
                // check if formatter implements IFormatter
                if (typeof(IFormatter).
                    IsAssignableFrom(formatterType))
                {
                    formatter = (IFormatter)
                        Activator.CreateInstance(formatterType);
                }
            }

            // if Output attribute not used
            // or formatter has wrong type, use default formatter
            if (formatter == null)
            {
                formatter = new DefaultFormatter();
            }

            foreach (PropertyInfo p in properties)
            {
                if (Attribute.IsDefined(
                        p,
                        ignoreType
                     ))
                {
                    continue;
                }
                // <=>
                if (p.IsDefined(ignoreType))
                {
                    continue;
                }

                object value = p.GetValue(obj);
                formatter.WriteLog(
                    p.Name,
                    p.PropertyType,
                    value);
                //
                // MethodInfo getter = p.GetGetMethod();
            }

            MethodInfo[] methods = t.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static);
            foreach(MethodInfo m in methods)
            {
                if (m.ReturnType != typeof(void) && 
                    m.GetParameters().Length==0)
                {
                    Console.WriteLine("{0}({2}) = {1}", 
                        m.Name,
                        m.Invoke(obj, null),
                        m.ReturnType);
                }
            }

        }


    }
}
