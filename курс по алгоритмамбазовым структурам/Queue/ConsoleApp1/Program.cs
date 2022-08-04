using System;
using System.Linq;
 
 namespace AlgorithmsDataStructures
 {
  
     public class QueueonStack<T>
     {
         private int count;
         Stack<T> headStack;
         Stack<T> tailStack;

         public QueueonStack()
         {
             headStack = new Stack<T>();
             tailStack = new Stack<T>();
             count = 0;
         }

         public void Enqueue(T item)
         {
             tailStack.Push(item);
             ++count;
         }

         public T Dequeue()
         {
             T lastItem = default(T);
             int innerCount = Size();

             while (innerCount > 0)
             {
                 headStack.Push(tailStack.Pop());
                 lastItem = headStack.Pop();
                 --innerCount;
             }

             if (count > 0) --count;
             else count = 0;

             return lastItem;
         }

         public int Size()
         {
             return count;
         }
     }

     public class Stack<T>
     {
         public T[] items;
         private int count;
         int capacity;
        
         public Stack()
         {
             count = 0;
             MakeArray(16);
             // инициализация внутреннего хранилища стека
         } 

         public int Size() 
         {
             // размер текущего стека		  
             return this.count;
         }

         public T Pop()
         {
             // ваш код
             if (this.count <= this.capacity / 2 && this.capacity >= 16)
             {
                 this.capacity = (int)(this.capacity / 1.5);
                 if (this.capacity < 16)
                     this.capacity = 16;
             }
            
             if (this.count > 0)
             {
                 T item = this.items[--count];
                 this.items[count] = default(T);

                 return item;
             }
            
             return default(T); // null, если стек пустой
         }
	  
         public void Push(T val)
         {
             if (this.count == this.capacity)
             {
                 this.MakeArray(capacity * 2);
             }
             this.items[this.count] = val;
             this.count++;
             // ваш код
         }

         public T Peek()
         {
             // ваш код
             if (this.count > 0)
                 return this.items[count - 1];

             return default(T); // null, если стек пустой
         }
        
         public void MakeArray(int new_capacity)
         {
             this.capacity = new_capacity;
            
             if (this.items == null)
             {
                 this.items = new T[new_capacity];
             }
             else
             {
                 T[] resArr = new T[new_capacity];

                 for (int i = 0; i < items.Length; i++)
                 {
                     resArr[i] = items[i];
                 }
                 this.items = resArr;
             }
         }
     }
     
     
     class MainProgramm
     {
         static void Main(string[] args)
         {
             Queue<int> qu = new Queue<int>();

             int i = 0;
             while (qu.Size() < 100000)
             {
                 qu.Enqueue(11);
                 i++;
             }

            // qu.Rotate(5);
             
             
             /*while (qu.Size() > 0)
                 qu.Dequeue();*/
 
             
            // Console.WriteLine("Заполненный массив:");
             Console.ReadKey();
         }
 
 
     }
     
 }