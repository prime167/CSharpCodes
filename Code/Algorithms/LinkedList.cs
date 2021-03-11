using System;

namespace Algorithms
{
    public class List
    {
        public class Node
        {
            public object NodeContent;
            public Node Next;
        }

        private int _size;
        public int Count
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// The head of the list.
        /// </summary>
        private Node _head;

        public Node Head { get; set; }

        /// <summary>
        /// The current node, used to avoid adding nodes before the head
        /// </summary>
        private Node _current;

        public List()
        {
            _size = 0;
            _head = null;
        }

        /// <summary>
        /// Add a new Node to the list.
        /// </summary>
        public void AddLast(object content)
        {
            _size++;

            var node = new Node()
            {
                NodeContent = content
            };

            if (_head == null)
            {
                _head = node;
            }
            else
            {
                _current.Next = node;
            }

            _current = node;
        }

        public void AddFirst(object content)
        {
            _size++;
            _head = new Node()
            {
                Next = _head,
                NodeContent = content
            };
        }

        /// <summary>
        ///  Throwing this in to help test the list
        /// </summary>
        public void ListNodes()
        {
            Node tempNode = _head;

            while (tempNode != null)
            {
                Console.WriteLine(tempNode.NodeContent);
                tempNode = tempNode.Next;
            }
        }

        /// <summary>
        /// Returns the Node in the specified position or null if inexistent
        /// </summary>
        /// <param name="position">One based position of the node to retrieve</param>
        /// <returns>The desired node or null if inexistent</returns>
        public Node Retrieve(int position)
        {
            Node tempNode = _head;
            Node retNode = null;
            int count = 0;

            while (tempNode != null)
            {
                if (count == position - 1)
                {
                    retNode = tempNode;
                    break;
                }
                count++;
                tempNode = tempNode.Next;
            }

            return retNode;
        }

        /// <summary>
        /// Delete a Node in the specified position
        /// </summary>
        /// <param name="position">Position of node to be deleted</param>
        /// <returns>Successful</returns>
        public bool Delete(int position)
        {
            if (_size == 0)
            {
                return false;
            }

            if (position == 1)
            {
                _head = _current = _head.Next;
                return true;
            }

            if (position > 1 && position <= _size)
            {
                Node tempNode = _head;

                Node lastNode = null;
                int count = 0;

                while (tempNode != null)
                {
                    if (count == position - 1)
                    {
                        lastNode.Next = tempNode.Next;
                        return true;
                    }
                    count++;

                    lastNode = tempNode;
                    tempNode = tempNode.Next;
                }
            }

            return false;
        }

        public void Reverse()
        {
            /*
                     _head
             Null  x   A  ->  B  ->  C  ->  D  ->  null
              n        p     
             ------------------------------------------
             Null  <-  A  x   B  ->  C  ->  D  ->  null
                       n      p  
             ------------------------------------------
             Null  <-  A  <-  B  x   C  ->  D  ->  null 
                              n      p              
             ------------------------------------------
             Null  <-  A  <-  B  <-  C  x   D  ->  null 
                                     n      p     
             ------------------------------------------
             Null  <-  A  <-  B  <-  C  <-  D  x   null 
                                            n       p    
                                          _head
             */
            if (_size > 1)
            {
                Node n = null, p = _head;
                while (p != null)
                {
                    Node tmp = p.Next;
                    p.Next = n;
                    n = p;
                    p = tmp;
                }

                _head = n;
            }
        }

        public void Reverse1()
        {
            Node pre;
            Node current;
            Node next;
            pre = _head;
            current = _head.Next;
            while (current != null)
            {
                next = current.Next;
                current.Next = pre;
                pre = current;
                current = next;
            }

            _head.Next = null;
            _head = pre;
        }

        /// <summary>
        /// 判断链表是否有环路 例如 a -> b ->  c  -> d ->  e ->  b
        /// </summary>
        /// <returns></returns>
        public static bool HasLoop(Node head)
        {
            var p1 = head;
            var p2 = head;
            if (head == null || head.Next == null)
            {
                return false;
            }

            do
            {
                p1 = p1.Next;
                p2 = p2.Next.Next;
            } while (p1 != null && p2 != null && p1!= p2);

            if (p1 == p2)
                return true;

            return false;
        }
    }
}