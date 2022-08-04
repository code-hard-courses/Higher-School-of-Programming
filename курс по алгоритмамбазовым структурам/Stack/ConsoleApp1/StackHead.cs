using System.Collections.Generic;
using System.Linq;

namespace StackHead
{
    
    public class Node
    {
        public int value;
        public Node next;
        public Node prev;
        public Node(int _value)
        {
            value = _value;
            next = null;
            prev = null;
        }

        public int GetValue()
        {
            return value;
        }
    }
    

    public class Stack
    {
        private LinkedList<Node> stackObject;
        
        public Stack()
        {
            stackObject = new LinkedList<Node>();
        } 

        public int Size() 
        {
            // размер текущего стека		  
            return stackObject.Count;
        }

        public Node Pop()
        {
            Node firstNode = null;
            if (stackObject.Count != 0)
            {
                firstNode = stackObject.First();
                stackObject.RemoveFirst();
            }

            return firstNode;
        }
	  
        public void Push(Node val)
        {
            stackObject.AddFirst(val);
        }

        public Node Peek()
        {
            if (stackObject.Count != 0)
                return stackObject.First();

            return null;
        }

    }

}