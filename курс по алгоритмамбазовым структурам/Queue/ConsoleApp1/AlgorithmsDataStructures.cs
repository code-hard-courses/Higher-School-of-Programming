using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Queue<T>
    {
        LinkedList<T> items;
       
        public Queue()
        {
            items = new LinkedList<T>();
        }

        public void Enqueue(T item)
        {
            items.AddFirst(item);
        }

        public T Dequeue()
        {
            T lastItem = default(T);
            if (Size() > 0)
            {
                lastItem = items.Last.Value;
                items.RemoveLast();

                return lastItem;
            }

            return default(T); 
        }

        public int Size()
        {
            return items.Count;
        }
    }
}