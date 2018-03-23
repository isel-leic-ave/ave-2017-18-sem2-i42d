using System;
using System.Collections.Generic;
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
        public static Logger Build(Type t)
        {
            return new Logger(t);
        }

        private IFormatter formatter;
        private PropertyInfo[] filteredProperties;
        private MethodInfo[] filteredMethods;

        private Logger(Type t)
        {
            //this.t = t;

            /* Phase 1: build formatter */
            Type ignoreType = typeof(IgnoreAttribute);
            Type outputType = typeof(OutputAttribute);

            // variable to store a reference to a formatter
            formatter = null;
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

            /* Phase 2: build relevant properties */
            PropertyInfo[] props = t.GetProperties();
            List<PropertyInfo> relevantProps = new List<PropertyInfo>();
            foreach(PropertyInfo p in props)
            {
                if (!Attribute.IsDefined(p, ignoreType))
                {
                    relevantProps.Add(p);
                }
            }
            filteredProperties = relevantProps.ToArray();

            /* Phase 3: build relevant methods */
            MethodInfo[] meths = t.GetMethods();
            List<MethodInfo> relevantMeth = new List<MethodInfo>();
            foreach (MethodInfo m in meths)
            {
                if (m.ReturnType != typeof(void) &&
                    m.GetParameters().Length == 0)
                {
                    relevantMeth.Add(m);
                }
            }
            filteredMethods = relevantMeth.ToArray();

        }

        public void Log(object obj)
        {
            if (obj == null) return;
            foreach (PropertyInfo p in filteredProperties)
            {
                object value = p.GetValue(obj);
                formatter.WriteLog(
                    p.Name,
                    p.PropertyType,
                    value);
                //
                // MethodInfo getter = p.GetGetMethod();
            }

            foreach(MethodInfo m in filteredMethods)
            {
                Console.WriteLine("{0}({2}) = {1}", 
                    m.Name,
                    m.Invoke(obj, null),
                    m.ReturnType);
            }

        }


    }
}
