using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    /// <summary>
    /// 双端链表(注意和双向链表的区别)
    /// 每个节点指向下一个节点，有head指向第一个节点，tail指向最后一个节点
    /// </summary>
    public class DoubleEndedList
    {
        public Node Head { get; set; }

        public Node Tail { get; set; }

        public DoubleEndedList()
        {
            Head = null;
            Tail = null;
        }

        // 双端链表类

        public bool IsEmpty()
        {
            return Head == null;
        }

        // 表头插入
        public void InsertHead(int dd)
        {
            Node newNode = new Node(dd);
            if (IsEmpty())
            {
                Tail = newNode;
            }

            newNode.Next = Head;
            Head = newNode;
        }

        // 表尾插入
        public void InsertTail(int dd)
        {
            Node newNode = new Node(dd);
            if (IsEmpty())
            {
                Head = newNode;
            }
            else
            {
                Tail.Next = newNode;
            }

            Tail = newNode;
        }

        // 删除表头
        public void DeleteHead()
        {
            if (Head == null)
            {
                return;
            }

            Console.WriteLine($"delete {Head.Value}");
            Head = Head.Next;
            if (Head == null)
            {
                Tail = null;
            }
        }

        // 删除表尾
        public void DeleteTail()
        {
            if (Head == null)
            {
                return;
            }

            if (Head == Tail)
            {
                Console.WriteLine($"delete {Tail.Value}");
                Head = null;
                Tail = null;
            }
            else
            {
                Node before = null;
                Node current = Head;
                while (current != Tail)
                {
                    before = current;
                    current = current.Next;
                }

                Tail = before;
                Tail.Next = null;
                Console.WriteLine($"delete {current.Value}");
            }
        }

        public void DisplayList()
        {
            Console.WriteLine("List (first--->last)");
            Node current = Head;
            while (current != null)
            {
                current.DisplayNode();
                current = current.Next;
            }

            Console.WriteLine();
        }
    }

    public class Node
    {
        public Node Next { get; set; }

        public int Value { get; set; }

        public Node(int v)
        {
            Value = v;
        }

        internal void DisplayNode()
        {
            Console.WriteLine(Value);
        }
    }
}
