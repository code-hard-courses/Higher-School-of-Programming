using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    class Deque<T>
    {
        
        LinkedList<T> items;
        
        public Deque()
        {
            items = new LinkedList<T>();
        }

        public void AddFront(T item)
        {
            items.AddFirst(item);
        }

        public void AddTail(T item)
        {
            items.AddLast(item);
        }

        public T RemoveFront()
        {
            T firstItem = default(T);
            if (Size() > 0)
            {
                firstItem = items.First.Value;
                items.RemoveFirst();
            }

            return firstItem; 
        }

        public T RemoveTail()
        {
            T lastItem = default(T);
            if (Size() > 0)
            {
                lastItem = items.Last.Value;
                items.RemoveLast();
            }

            return lastItem; 
        }
        
        public int Size()
        {
            return items.Count;
        }
    }

}