using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace AlgorithmsDataStructures
{

    public class DynArray<T>
    {
        public T [] array;
        public int count;
        public int capacity;

        public DynArray()
        {
            count = 0;
            MakeArray(16);
        }

        public void MakeArray(int new_capacity)
        {
            this.capacity = new_capacity;
            
            if (this.array == null)
            {
                this.array = new T[new_capacity];
            }
            else
            {
                T[] resArr = new T[new_capacity];

                for (int i = 0; i < array.Length; i++)
                {
                    resArr[i] = array[i];
                }
                this.array = resArr;
            }
        }
        

        public T GetItem(int index)
        {
            // ваш код
            if (index < this.count && index >= 0)
            {
                return this.array[index];
            }
            else
            {
                new IndexOutOfRangeException("Введён Недопустимый индекс массива!");
            }
            return default(T);
        }

        public void Append(T itm)
        {
            if (this.count == this.capacity)
            {
                this.MakeArray(capacity * 2);
            }
            array[this.count] = itm;
            this.count++;
        }

        public void Insert(T itm, int index)
        {
            if (index < 0 || index > this.count)
                throw new IndexOutOfRangeException("Введён недопустимый индекс массива!");
            else if (index == this.count)
                this.Append(itm);
            else
            {
                if (count == capacity)
                    MakeArray(capacity * 2);

                T[] tempArr = new T[capacity];

                for (int i = 0; i < count; i++)
                {
                    if (i < index)
                        tempArr[i] = array[i];
                    else if (i == index)
                    {
                        tempArr[i] = itm;
                        tempArr[i + 1] = array[i];
                    }
                    else
                        tempArr[i + 1] = array[i];
                }

                this.array = tempArr;
                this.count++;
            }
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException("Введён недопустимый индекс массива!");
            if (count <= this.capacity / 2 && this.capacity >= 16)
            {
                this.capacity = (int)(this.capacity / 1.5);
                if (this.capacity < 16)
                    this.capacity = 16;
            }

            T[] tempArr = new T[this.capacity];

            for (int i = 0; i < this.count - 1; i++)
            {
                if (i < index)
                    tempArr[i] = this.array[i];
                else
                    tempArr[i] = this.array[i + 1];
            }
            this.array = tempArr;
            this.count--;
        }
        
        // не проверяется
        public void Print()
        {
            for (int i = 0; i < this.count; i++)
            {
                Console.Write("{0} ", this.array[i]);
            }
            Console.WriteLine();
        }
        

    }
        

}