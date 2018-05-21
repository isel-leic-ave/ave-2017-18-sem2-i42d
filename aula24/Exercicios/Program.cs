using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exercicios
{
    static class IterableUtils
    {
        public static IEnumerable<T> 
            Collapse<T>(this IEnumerable<T> source)
        {
            IEnumerator<T> it = source.GetEnumerator();
            if (it.MoveNext())
            {
                T prev = it.Current;
                yield return prev;
                while (it.MoveNext())
                {
                    T curr = it.Current;
                    if (!curr.Equals(prev))
                    {
                        prev = curr;
                        yield return curr;
                    }
                }
            }
        }

        public static IEnumerable<Action<T>> MethodsWithName<T>(
               this IEnumerable<Action<T>> source,
               String str)
        {
            foreach(Action<T> t in source)
            {
                MethodInfo mi = t.Method;
                String name = mi.Name;
                if (name.Contains(str))
                {
                    yield return t;
                }
            }
        }
    }


    /*
     * Acrescente à interface IEnumerable<T> suporte para a operação lazy Collapse, 
     * que retorna uma nova sequência que junta os elementos adjacentes iguais da 
     * sequência original (segundo o método Equals). 
     * 
     * Exemplo:  

            int[] a = { 7, 7, 9, 11, 11, 3, 3, 9, 9, 7 };
            foreach(int i in a.Collapse())  // escreve no standard output
                Console.Write(i + "; ");      // 7; 9; 11; 3; 9; 7;
    */


    /*
     *  Implemente o método de extensão MethodsWithName, 
     *  o qual produz uma sequência de saída lazy 
     *  com os elementos da sequência src cujo nome dos 
     *  métodos referidos contenha a string str.
     *  
     *  static IEnumerable<Action<T>> 
     *      MethodsWithName<T>(
     *          this IEnumerable<Action<T>> src, 
     *          String str)
     *  
     */


    class Program
    {

        static void Main(string[] args)
        {
            int[] a = { 3, 3, 3, 7, 7, 9, 3 };
            foreach(int v in a.Collapse())
            {
                Console.WriteLine(v);
            }

            List<Action<int>> actions = new List<Action<int>>();
            actions.Add(StaticMethod);
            Program p = new Program();
            actions.Add(p.InstanceMethod);

            foreach(Action<int> d 
                in actions.MethodsWithName("Method"))
            {
                Console.WriteLine("Target = {0}, Mtd = {1}",
                    d.Target,
                    d.Method.Name);
            }

            IEnumerable<Action<int>> seq = actions;

            IEnumerable<Action<int>> newSeq =
                seq.Where((d) => d.Method.Name.Contains("ABC"));

            // Language Integrated Query (LINQ)
            var other = from d in seq
                        where d.Method.Name.Contains("ABC")
                        select d;

        }

        public void InstanceMethod(int a) { }
        public static void StaticMethod(int b) { }
    }
}
