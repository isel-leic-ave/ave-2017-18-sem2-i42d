//    using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Iteradores
//{
//    class Student
//    {
//        public string name;
//        public int number;
//    }

//    class Aluno
//    {
//        public string nome;
//        public int numero;
//    }

//    class Program
//    {
//        static List<T> Filter<T>(
//            List<T> students, 
//            Predicate<T> p)
//        {
//            List<T> list = new List<T>();
//            foreach(T s in students)
//            {
//                if (p(s))
//                {
//                    list.Add(s);
//                }
//            }
//            return list;
//        }

//        static List<W> Convert<T, W>(
//            List<T> students, 
//            Func<T, W> conv)
//        {
//            List<W> list = new List<W>();
//            foreach (T s in students)
//            {
//                list.Add(conv(s));
//                list.Add(conv(s));
//            }
//            return list;
//        }

//        static void Main(string[] args)
//        {
//            List<Student> lstStudent = new List<Student>();
//            lstStudent.Add(new Student { number = 11, name = "abc"});
//            lstStudent.Add(new Student { number = 20, name = "xpto" });
//            lstStudent.Add(new Student { number = 33, name = "aaa" });

//            List<Student> filter = Filter(
//                lstStudent,
//                (student) => student.number % 2 != 0
//                //new Predicate<Student>(OddFilter)
//            );

//            List<Aluno> convAluno = Convert(
//                filter,
//                (s) => new Aluno { nome = s.name, numero = s.number });

//            foreach(Aluno a in convAluno)
//            {
//                Console.WriteLine("nome={0} numero={1}",
//                    a.nome,
//                    a.numero);
//            }
//        }

//        public static bool OddFilter(Student student)
//        {
//            return student.number % 2 != 0;
//        }

//    }
//}
