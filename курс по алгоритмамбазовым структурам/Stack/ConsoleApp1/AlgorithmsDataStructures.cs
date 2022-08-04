using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

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

}