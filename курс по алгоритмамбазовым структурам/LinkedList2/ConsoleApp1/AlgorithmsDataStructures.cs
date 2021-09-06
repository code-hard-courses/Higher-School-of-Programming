using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Node
    {
        public int value;
        public Node next, prev;

        public Node(int _value) { 
            value = _value; 
            next = null;
            prev = null;
        }
    }

    public class LinkedList2
    {
        public Node head;
        public Node tail;

        public LinkedList2()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(Node _item)
        {
            if (head == null) {
                head = _item;
                head.next = null;
                head.prev = null;
            } else {
                tail.next = _item;
                _item.prev = tail;
            }
            tail = _item;
        }

        public Node Find(int _value)
        {
            Node node = head;
            while (node != null)
            {
                if (node.value == _value) return node;
                node = node.next;
            }

            return null;
        }

        public List<Node> FindAll(int _value)
        {
            List<Node> findNodes = new List<Node>();

            Node node = this.head;
            while (node != null)
            {
                if (node.value == _value)
                {
                    findNodes.Add(node);
                }

                node = node.next;
            }

            return findNodes;
        }

        public bool Remove(int _value)
        {
           
            Node node = this.head;
            Node prevNode = this.head;
            
            while (node != null)
            {
                if (node.value == _value)
                {
                    if (this.head == this.tail)
                    {
                        this.Clear();
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

                    return true;
                }
                else
                {
                    prevNode = node;
                    node = node.next;
                }
            }

            return false;
            
            
        }

        public void RemoveAll(int _value)
        {
            Node node = this.head;
            Node prevNode = this.head;

            while (node != null)
            {
                if (node.value == _value)
                {
                    if (this.head == this.tail)
                    {
                        this.Clear();
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
                }
                else
                {
                    prevNode = node;
                }

                node = node.next;
            }
        }

        public void Clear()
        {
            head = null;
            tail = null;
        }

        public int Count()
        {
            int count = 0;
            Node node = head;
            while (node != null)
            {
                count++;
                node = node.next;
            }

            return count; 
        }

        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            if (_nodeAfter != null)
            {
                Node node = head;
                while (node != null)
                {
                    if (node.value == _nodeAfter.value)
                    {
                        if (this.tail == node)
                        {
                            this.tail.next = _nodeToInsert;
                            _nodeToInsert.prev = this.tail;
                            this.tail = _nodeToInsert;
                        }
                        else
                        {
                            _nodeToInsert.next = node.next;
                            _nodeToInsert.prev = node;
                            node.next = _nodeToInsert;
                        }
                    }

                    node = node.next;
                }
            }
            else if (_nodeAfter == null && head == null)
            {
                this.head = _nodeToInsert;
                this.tail = _nodeToInsert;
            }
            else if (_nodeAfter == null && head != null)
            {
                Node currentHead = this.head;
                this.head = _nodeToInsert;
                this.head.next = currentHead;
                this.head.prev = null;
            }

        }
        
        public void WriteToConsole()
        {
            Node node = head;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.next;
            }
        }
        

    }
}