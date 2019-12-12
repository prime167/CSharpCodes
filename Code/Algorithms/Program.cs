using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<int> a = Enumerable.Range(1, 10).ToList();
            a.Shuffle();

            ShowSort("冒泡排序", a, Sort.BubbleSort);
            ShowSort("选择排序", a, Sort.SelectionSort);
            ShowSort("插入排序", a, Sort.InsertionSort);
            ShowSort("Shell排序", a, Sort.ShellSort);
            ShowSort("归并排序", a, Sort.MergeSort);
            List<int> dd = ShowSort("快速排序", a, Sort.QuickSort);

            Console.WriteLine("约瑟夫环问题");
            var e = new JosephusProblem();
            int[] aj = e.Jose(41, 2);
            Console.WriteLine("order:");
            Console.WriteLine(aj.Concat());

            Console.WriteLine("\nList:");
            var l = new List();
            l.AddLast(1);
            l.AddLast(2);
            l.AddFirst(0);
            l.ListNodes();
            l.Reverse();
            Console.WriteLine("\n reversed:");
            l.ListNodes();

            Console.WriteLine("\nHas loop?");
            var n1 = new List.Node { NodeContent = 1 };
            var n2 = new List.Node { NodeContent = 1 };
            var n3 = new List.Node { NodeContent = 1 };
            var n4 = new List.Node { NodeContent = 1 };
            var n5 = new List.Node { NodeContent = 1 };
            var n6 = new List.Node { NodeContent = 1 };
            var n7 = new List.Node { NodeContent = 1 };
            var n8 = new List.Node { NodeContent = 1 };

            n1.Next = n2;
            n2.Next = n3;
            n3.Next = n4;
            n4.Next = n5;
            n5.Next = n6;
            n6.Next = n7;
            n7.Next = n8;
            n8.Next = n2;

            bool hasLoop = List.HasLoop(n1);
            Console.WriteLine(hasLoop);

            var arrA = new[] { 1, 2, 5, 7, 8, 9, 23, 56 };
            var arrB = new[] { 2, 4, 5, 9, 23, 145 };
            Other.FindSame(arrA, arrB);
            Console.WriteLine(Other.HasSame(arrA, arrB));

            IEnumerable<int> x = arrA.Intersect(arrB);
            x.ForAll(Console.WriteLine);

            var s = "this is a test!";
            Console.WriteLine(Other.ReverseSentenceByWord(s));

            var ls = new decimal[] { -1, 2, -3, -5, 3, 4, -2, 3 };
            Console.WriteLine(ls.ToList().FindBestSubsequence(out int start, out int end));
            Console.WriteLine("start index:{0}, end index:{1}", start, end);
        }

        private static List<T> ShowSort<T>(string sortMethod, List<T> a, Func<List<T>, List<T>> S)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine();
            Console.WriteLine(sortMethod);

            List<T> copy = (from x in a
                        select x).ToList();
            string b = copy.Concat();
            Console.WriteLine("sorting:");
            List<T> sorted = S(copy);
            sw.Stop();
            string c = sorted.Concat();
            Console.WriteLine(c);
            return copy;
        }
    }
}
