using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructuresQueueonArray
{

    public class QueueonArray<T>
    {
        public T[] items;
        private int count;
        int capacity;
        
        public QueueonArray()
        {
            count = 0;
            MakeArray(16);
        } 

        public void Enqueue(T item)
        {
            if (this.count == this.capacity)
            {
                this.MakeArray(capacity * 2);
            }
            this.items[this.count] = item;
            this.count++;
        }

        public T Dequeue()
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
                T item = this.items[0];
                this.count--;
                
                T[] resArr = new T[items.Length];
                for (int i = 1; i < items.Length; i++)
                {
                    resArr[i-1] = items[i];
                }
                
                this.items = resArr;
                
                return item;
            }
            
            // выдача из головы
            return default(T); // если очередь пустая
        }

        public int Size()
        {
            return this.count;
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
        
        
        public void Rotate(int offset)
        {
            while (offset > 0)
            {
                Enqueue(Dequeue());
                --offset;
            }
        }
        
        
    }
}