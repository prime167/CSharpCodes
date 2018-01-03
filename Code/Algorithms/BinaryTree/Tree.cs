using System;
using System.Collections.Generic;

namespace Algorithms.BinaryTree
{
    public static class Tree
    {
        public static void WideFirstTraverse(Node root)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                Console.WriteLine(n.data);
                if (n.lc != null)
                {
                    queue.Enqueue(n.lc);
                }

                if (n.rc != null)
                {
                    queue.Enqueue(n.rc);
                }
            }
        }

        public static void DepthFirstTraverse(Node root)
        {
            var stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var n = stack.Pop();
                Console.WriteLine(n.data);
                if (n.rc != null)
                {
                    stack.Push(n.rc);
                }

                if (n.lc != null)
                {
                    stack.Push(n.lc);
                }
            }
        }

        public static void PreOrder(Node d)
        {
            Console.WriteLine(d.data);
            if (d.lc != null)
            {
                PreOrder(d.lc);
            }

            if (d.rc != null)
            {
                PreOrder(d.rc);
            }
        }

        public static void Inorder(Node d)
        {
            if (d.lc != null)
            {
                Inorder(d.lc);
            }

            Console.WriteLine(d.data);

            if (d.rc != null)
            {
                Inorder(d.rc);
            }
        }

        public static void PostOrder(Node d)
        {
            if (d.lc != null)
            {
                PostOrder(d.lc);
            }

            if (d.rc != null)
            {
                PostOrder(d.rc);
            }

            Console.WriteLine(d.data);
        }

        public static int TreeDepth(Node n)
        {
            if (n == null)
            {
                return 0;
            }

            Console.WriteLine(n.data);
            int ld = TreeDepth(n.lc);
            int rd = TreeDepth(n.rc);
            return 1 + (ld > rd ? ld : rd);
        }
    }
}
