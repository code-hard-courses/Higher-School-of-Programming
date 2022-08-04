using System;
using System.Linq;
 
namespace AlgorithmsDataStructures
{
  
 
    class MainProgramm
    {
        static void Main(string[] args)
        {
            Deque<string> qu = new Deque<string>();

            qu.AddFront("f1");
            qu.AddTail("t1");
            qu.AddFront("f2");
            qu.AddTail("t2");
            while (qu.Size() > 0)
            {           
                qu.RemoveFront();
                qu.RemoveTail();
            }

          
            
            
            /*int i = 0;
            while (qu.Size() < 100000)
            {
                qu.Enqueue(11);
                i++;
            }*/

            // qu.Rotate(5);
             
             
            /*while (qu.Size() > 0)
                qu.Dequeue();*/
 
             
            // Console.WriteLine("Заполненный массив:");
            Console.ReadKey();
        }

        static bool isPalindrome(string str)
        {
            Deque<char> dequ = new Deque<char>();

            for (int i = 0; i < str.Length; i++)
            {
                dequ.AddFront(str[i]);
            }

            while (dequ.Size() > 0)
            {
                if (dequ.RemoveFront() != dequ.RemoveTail())
                {
                    return false;
                }
            }

            return true;
        }
    }
     
}