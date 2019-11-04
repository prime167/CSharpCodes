using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.BinaryTree;

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

            var find = 5;
            Console.WriteLine("\n二分查找:");
            Console.WriteLine("find {0} in {1}", find, dd.Concat());

            var q = Search.binary_search_first_position1(dd.ToArray(), dd.Count, find);
            Console.WriteLine("index: {0}", q);

            Console.WriteLine("\n");
            a.Shuffle();
            Console.WriteLine(a.Concat());
            Console.WriteLine(Search.FindSecond(a));

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

            Console.WriteLine("\n tree:");
            // tree
            Console.WriteLine("=============tree==========");
            var root = new Node(0);
            var p1 = new Node(1);
            var p2 = new Node(2);
            var p3 = new Node(3);
            var p4 = new Node(4);
            var p5 = new Node(5);
            var p6 = new Node(6);
            var p7 = new Node(7);
            var p8 = new Node(8);
            var p9 = new Node(9);
            var p10 = new Node(10);
            var p11 = new Node(11);
            var p12 = new Node(12);
            var p13 = new Node(13);
            var p14 = new Node(14);
            var p15 = new Node(15);
            var p16 = new Node(16);

            root.lc = p1;
            root.rc = p2;
            p1.lc = p3;
            p1.rc = p4;
            p2.lc = p5;
            p2.rc = p6;
            p3.lc = p7;
            p3.rc = p8;
            p4.lc = p9;
            p4.rc = p10;
            p5.lc = p11;
            p5.rc = p12;
            p6.lc = p13;
            p6.rc = p14;
            p14.rc = p15;

            Tree.WideFirstTraverse(root);
            Console.WriteLine("==========depth first========");
            Tree.DepthFirstTraverse(root);
            Console.WriteLine("==========pre: root left right(equal to depthFirst)===========");
            Tree.PreOrder(root);
            Console.WriteLine("==========in: left root right===========");
            Tree.Inorder(root);
            Console.WriteLine("==========post: left right root===========");
            Tree.PostOrder(root);

            Console.WriteLine();
            Console.WriteLine("tree depth");
            int dep = Tree.TreeDepth(root);
            Console.WriteLine(dep.ToString());

            Console.WriteLine();
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
            Console.ReadLine();
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
