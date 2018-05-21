using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arrayList = new List<string>();

            LinkedList<SortedSet<string>> linkedList = new LinkedList<SortedSet<string>>();

            Dictionary<int, SortedSet<string>> ht = new Dictionary<int, SortedSet<string>>();

            foreach(string v in arrayList)
            {
                // print v
            }
            //<=>
            IEnumerator<string> it = arrayList.GetEnumerator();
            while (it.MoveNext())
            {
                string v = it.Current;
                Console.WriteLine(v);
            }
            
            foreach(int i in MakeSequence(100))
            {
                Console.WriteLine(i);
            }

        }

        private static IEnumerable<int> MakeSequence(int limit)
        {
            return new NIEnumerable(limit);
        }
    }
       
    class NIEnumerable : IEnumerable<int>
    {
        int limit;
        public NIEnumerable(int limit)
        {
            this.limit = limit;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new NIEnumerator(limit);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NIEnumerator(limit);
        }
    }
    
    class NIEnumerator : IEnumerator<int>
    {
        int limit;
        int current = -1;
        public NIEnumerator(int limit)
        {
            this.limit = limit;
        }
        public int Current
        {
            get
            {
                return current;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return current;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            current++;
            return current <= limit;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
