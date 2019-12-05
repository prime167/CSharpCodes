using System;
using System.Collections.Generic;

namespace Algorithms.BinaryTree
{
    public static class Tree
    {
        public static void WideFirstTraverse(TreeNode root)
        {
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                Console.WriteLine(n.data);
                if (n.Left != null)
                {
                    queue.Enqueue(n.Left);
                }

                if (n.Right != null)
                {
                    queue.Enqueue(n.Right);
                }
            }
        }

        public static void DepthFirstTraverse(TreeNode root)
        {
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var n = stack.Pop();
                Console.WriteLine(n.data);
                if (n.Right != null)
                {
                    stack.Push(n.Right);
                }

                if (n.Left != null)
                {
                    stack.Push(n.Left);
                }
            }
        }

        public static void PreOrder(TreeNode d)
        {
            Console.WriteLine(d.data);
            if (d.Left != null)
            {
                PreOrder(d.Left);
            }

            if (d.Right != null)
            {
                PreOrder(d.Right);
            }
        }

        public static void Inorder(TreeNode d)
        {
            if (d.Left != null)
            {
                Inorder(d.Left);
            }

            Console.WriteLine(d.data);

            if (d.Right != null)
            {
                Inorder(d.Right);
            }
        }

        public static void PostOrder(TreeNode d)
        {
            if (d.Left != null)
            {
                PostOrder(d.Left);
            }

            if (d.Right != null)
            {
                PostOrder(d.Right);
            }

            Console.WriteLine(d.data);
        }

        public static int TreeDepth(TreeNode n)
        {
            if (n == null)
            {
                return 0;
            }

            Console.WriteLine(n.data);
            int ld = TreeDepth(n.Left);
            int rd = TreeDepth(n.Right);
            return 1 + (ld > rd ? ld : rd);
        }
    }
}
