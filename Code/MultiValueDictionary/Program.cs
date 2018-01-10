using System;
using System.Collections.Generic;

namespace MultiValueDictionary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var mvd = new MultiValueDictionary<double, double>();
            mvd.Add(13, 4);
            mvd.Add(3, 5);
            mvd.Add(3.2, 4);
            mvd.Add(3.2, 5);
            mvd.Add(3.3, 4);
            mvd.Add(3.2, 5);

            foreach (var kv in mvd)
            {
                Console.WriteLine($"{kv.Key} {kv.Value.Count}");
            }

            mvd.Remove(33);

            Console.ReadLine();
        }
    }

    public class MultiValueDictionary<T1, T2>
    {
        private readonly SortedDictionary<T1, List<T2>> _inner;

        public MultiValueDictionary()
        {
            _inner = new SortedDictionary<T1, List<T2>>();
        }

        public void Add(T1 key, T2 value)
        {
            if (_inner.ContainsKey(key))
            {
                _inner[key].Add(value);
            }
            else
            {
                _inner.Add(key, new List<T2> { value });
            }
        }

        public void Remove(T1 key)
        {
            _inner.Remove(key);
        }

        public bool ContainsKey(T1 key)
        {
            return _inner.ContainsKey(key);
        }

        public int Count()
        {
            return _inner.Count;
        }

        public IEnumerator<KeyValuePair<T1, List<T2>>> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }
}
