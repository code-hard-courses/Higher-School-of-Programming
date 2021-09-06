using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {
        public T value;
        public Node<T> next, prev;

        public Node(T _value)
        {
            value = _value;
            next = null;
            prev = null;
        }
    }

    public class OrderedList<T>
    {
        public Node<T> head, tail;
        private bool _ascending;

        public OrderedList(bool asc)
        {
            head = null;
            tail = null;
            _ascending = asc;
        }

        public int Compare(T v1, T v2)
        {
            int result = 0;
            if (typeof(T) == typeof(String))
            {
                string firstStr = Convert.ToString(v1).Trim();
                string secondStr = Convert.ToString(v2).Trim();

                if (firstStr.Length > secondStr.Length)
                {
                    result = 1;
                }
                else if (firstStr.Length < secondStr.Length)
                {
                    result = -1;
                }
                else
                {
                    for (int i = 0; i < secondStr.Length; i++)
                    {
                        if (firstStr[i] > secondStr[i])
                        {
                            result = 1;
                            break;
                        }
                        else if (firstStr[i] < secondStr[i])
                        {
                            result = -1;
                            break;
                        }
                    }
                }
            }
            else
            {
                int firstDigit = Convert.ToInt32(v1);
                int secondDigit = Convert.ToInt32(v2);


                if (firstDigit > secondDigit)
                {
                    result = 1;
                }
                else if (firstDigit < secondDigit)
                {
                    result = -1;
                }

                // универсальное сравнение
            }

            return result;
        }

        public void Add(T value)
        {
            Node<T> node = head;
            Node<T> nodeToInsert = new Node<T>(value);

            if (node != null)
            {
                
                //в конец списка
                if (((Compare(tail.value, value) == -1 || Compare(tail.value, value) == 0) && _ascending) ||
                    ((Compare(tail.value, value) == 1 || Compare(tail.value, value) == 0) && !_ascending))
                {
                    tail.next = nodeToInsert;
                    nodeToInsert.prev = tail;
                    tail = nodeToInsert;
                    tail.next = null;

                    return;
                }
                
                //в начало списка.
                if ((Compare(head.value, value) == 1 && _ascending) ||
                    (Compare(head.value, value) == -1 && !_ascending))
                {
                    nodeToInsert.next = head;
                    head.prev = nodeToInsert;
                    nodeToInsert.prev = null;
                    head = nodeToInsert;
                    head.prev = null;

                    return;
                }
                
                while (node != null)
                {
                    int result = Compare(node.value, value);

                    if ((result == 1 && _ascending) ||
                        ((result == -1 || result == 0) && !_ascending))
                    {
                        nodeToInsert.next = node;
                        nodeToInsert.prev = node.prev;
                        node.prev.next = nodeToInsert;
                        node.prev = nodeToInsert;

                        return;
                    }

                    node = node.next;
                }
            }
            else
            {
                head = nodeToInsert;
                head.next = null;
                head.prev = null;
                tail = nodeToInsert;
                tail.next = null;
                tail.prev = null;
            }
        }

        public Node<T> Find(T val)
        {
            Node<T> node = head;

            while (node != null)
            {
                int result = Compare(node.value, val);


                if (result == 0)
                {
                    return node;
                }
                else if ((result == 1 && _ascending) || (result == -1 && !_ascending))
                {
                    break;
                }

                node = node.next;
            }

            return null;
        }

        public void Delete(T val)
        {
            Node<T> node = this.head;
            Node<T> prevNode = this.head;
            
            while (node != null)
            {
                if (Compare(node.value , val)==0)
                {
                    if (this.head == this.tail)
                    {
                        Clear(_ascending);
                    }
                    else if (this.head == node)
                    {
                        this.head = node.next;
                        this.head.prev = null;
                    }
                    else if (this.tail == node)
                    {
                        prevNode.next = null;
                        this.tail = prevNode;
                    }
                    else
                    {
                        prevNode.next = node.next;
                        node.next.prev = prevNode;
                    }

                    return;
                }
                else
                {
                    prevNode = node;
                    node = node.next;
                }
            }
        }

        public void Clear(bool asc)
        {
            _ascending = asc;
            head = null;
            tail = null;
            // здесь будет ваш код
        }

        public int Count()
        {
            int count = 0;
            Node<T> node = head;
            while (node != null)
            {
                count++;
                node = node.next;
            }

            return count;
        }

        public string Print()
        {
            Node<T> node = head;
            string res = "";
            while (node != null)
            {
                res += node.value + " ";
                node = node.next;
            }

            if (res.Length == 0) return "[]";
            return res;
        }


        List<Node<T>> GetAll()
        {
            List<Node<T>> r = new List<Node<T>>();
            Node<T> node = head;
            while (node != null)
            {
                r.Add(node);
                node = node.next;
            }

            return r;
        }
    }
}