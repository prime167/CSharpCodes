using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace Rx
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] foo = (from n in Enumerable.Range(0, 10) select n * n).ToArray();
            foo.ForAll(Console.WriteLine);
            
            // same as
            foo.ToList().ForEach(Console.WriteLine);

            List<int> its = Enumerable.Range(1, 15).ToList();
            its.Where(i => i % 2 == 0).ToList().ForEach(i => Console.Write("{0} ", i));

            Console.WriteLine();
            IObservable<int> input = Observable.Range(1, 15);
            input.Where(i => i % 2 == 0).Subscribe(x => Console.Write("{0} ", x));

            var txt = new TextBox();
            var frm = new Form{Controls = {txt}};

            var ts = Observable.FromEventPattern(txt, "TextChanged");
            var res = (from e in ts
                select ((TextBox) e.Sender).Text).DistinctUntilChanged();
            using (res.Subscribe(Console.WriteLine))
            {
                Application.Run(frm);
            }
        }
    }

    public static class Extensions
    {
        public static void ForAll<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T item in sequence)
            {
                action(item);
            }
        }
    }
}
