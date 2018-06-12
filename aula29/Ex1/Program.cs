using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex1
{
    public interface IValidation { bool Validate(object obj); }

    public class Above18 : IValidation
    {
        public bool Validate(object obj)
        {
            return (int)obj > 18;
        }
    }

    public class ValidationException : Exception { }

    public class TypeMismatchException : Exception { }

    public class FuncWrapper<W> : IValidation
    {
        Func<W, bool> validator;
        public FuncWrapper(Func<W, bool> val)
        {
            validator = val;
        }
        public bool Validate(object obj)
        {
            return validator((W)obj);
        }
    }

    public class Validator<T>
    {
        // v1
        // private Dictionary<string, IValidation> validators;

        // v2
        private Dictionary<PropertyInfo, IValidation> validators;
        Type type;

        public Validator()
        {
            //validators = new Dictionary<string, IValidation>();
            validators = new Dictionary<PropertyInfo, IValidation>();
            type = typeof(T);
        }

        public Validator<T> AddValidation(string pName, IValidation rule)
        {
            // add rule to list
            //validators.Add(pName, rule);
            PropertyInfo pInfo = type.GetProperty(pName);
            validators.Add(pInfo, rule);
            return this;
        }

        public Validator<T>
            AddValidation<W>(string pName, Func<W, bool> validator)
        {
            Type typeW = typeof(W);
            PropertyInfo pInfo = type.GetProperty(pName);
            if (typeW != pInfo.PropertyType)
                throw new TypeMismatchException();
            IValidation rule = new FuncWrapper<W>(validator);
            // add rule to map
            validators.Add(pInfo, rule);
            return this;
        }

        public bool Validate(T obj)
        {
            // check all rules in list
            // Type type = typeof(T);
            foreach (KeyValuePair<PropertyInfo, IValidation> entry in validators)
            {
                //string pName = entry.Key;
                IValidation rule = entry.Value;
                PropertyInfo pInfo = entry.Key; //type.GetProperty(pName);
                object value = pInfo.GetValue(obj);
                if (!rule.Validate(value))
                    throw new ValidationException();
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
