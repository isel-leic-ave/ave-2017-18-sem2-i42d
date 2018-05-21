using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteradores
{
    class Student
    {
        public string name;
        public int number;
    }

    class Aluno
    {
        public string nome;
        public int numero;
    }

    class FilterEnumerator<T> : IEnumerator<T>
    {
        private IEnumerator<T> it;
        private Predicate<T> p;

        public FilterEnumerator(IEnumerable<T> seq, Predicate<T> p)
        {
            it = seq.GetEnumerator();
            this.p = p;
        }

        public T Current
        {
            get
            {
                return it.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return it.Current;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            while (it.MoveNext())
            {
                if (p(it.Current))
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    class FilterEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> seq;
        readonly private Predicate<T> p;

        public FilterEnumerable(IEnumerable<T> seq, Predicate<T> p)
        {
            this.seq = seq;
            this.p = p;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new FilterEnumerator<T>(seq, p);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FilterEnumerator<T>(seq, p);
        }
    }

    // Extrension Methods
    static class IteratorUtils
    {
        public static IEnumerable<T> Filter<T>(
            this IEnumerable<T> seq,
            Predicate<T> p)
        {
            return new FilterEnumerable<T>(seq, p);
        }

        public static IEnumerable<W> Convert<T, W>(
            this IEnumerable<T> seq,
            Func<T, W> conv)
        {
            foreach (T t in seq)
            {
                yield return conv(t);
            }
        }
    }

    class Program
    {
        static IEnumerable<T> Filter<T>(
            IEnumerable<T> students, 
            Predicate<T> p)
        {
            return new FilterEnumerable<T>(students, p);
            /*List<T> list = new List<T>();
            foreach(T s in students)
            {
                if (p(s))
                {
                    list.Add(s);
                }
            }
            return list;*/
        }

        static List<W> Convert<T, W>(
            List<T> students, 
            Func<T, W> conv)
        {
            List<W> list = new List<W>();
            foreach (T s in students)
            {
                list.Add(conv(s));
            }
            return list;
        }

        static void Main(string[] args)
        {
            List<Student> lstStudent = new List<Student>();
            lstStudent.Add(new Student { number = 11, name = "abc"});
            lstStudent.Add(new Student { number = 20, name = "xpto" });
            lstStudent.Add(new Student { number = 33, name = "aaa" });

            IEnumerable<Student> filter = Filter(
                lstStudent,
                (student) => student.number % 2 != 0
                //new Predicate<Student>(OddFilter)
            );

            foreach(Student s in filter)
            {
                Console.WriteLine("nome={0} numero={1}",
                    s.name,
                    s.number);
            }

            var seq =
                lstStudent
                    .Filter((s) => s.number % 2 != 0)
                    .Convert((s) => new Aluno());

            foreach (var a in seq)
            {
                Console.WriteLine("nome={0} numero={1}",
                    a.nome,
                    a.numero);

            }

        }

        public static bool OddFilter(Student student)
        {
            return student.number % 2 != 0;
        }

    }
}
