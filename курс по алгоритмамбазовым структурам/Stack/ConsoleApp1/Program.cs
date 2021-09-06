using System;
using System.Collections.Generic;
using System.Linq;

namespace Stack
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
            Node lastNode = null;
            if (stackObject.Count != 0)
            {
                lastNode = stackObject.Last();
                stackObject.RemoveLast();
            }

            return lastNode;
        }
	  
        public void Push(Node val)
        {
            stackObject.AddLast(val);
        }

        public Node Peek()
        {
            if (stackObject.Count != 0)
                return stackObject.Last();

            return null;
        }

    }  

   
    
    class MainProgramm
    {
        static void Main(string[] args)
        {
            

            
            Console.WriteLine(MainProgramm.CheckBalance("(()((())())))"));
           // Console.WriteLine("Заполненный массив:");
            Console.ReadKey();
            
        }
        
        
        static bool CheckBalance(string brackets)
        {
            Stack stackBrackets = new Stack();

            int index = 0;
            while (index < brackets.Length)
            {
                if (brackets[index] == ')' && stackBrackets.Size() == 0)
                    return false;
                else if (brackets[index] == '(')
                    stackBrackets.Push(new Node(1));
                else
                    stackBrackets.Pop();
                index++;
            }

            return stackBrackets.Size() == 0;
        }

    }
    
}