using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class HashTable
    {
        public int size;
        public int step;
        public string [] slots; 

        public HashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            slots = new string[size];
            for(int i=0; i<size; i++) slots[i] = null;
        }

        public int HashFun(string value)
        {    
            // всегда возвращает корректный индекс слота
            int hashIndex = 0;
            if (value != null)
            {
                foreach (var t in value)
                {
                    hashIndex += t;
                }
                hashIndex %= this.size;
            }
            return hashIndex;
        }

        public int SeekSlot(string value)
        {
            // находит индекс пустого слота для значения, или -1
            int hashIndex = HashFun(value);
            
            if (slots[hashIndex] == null)
                return hashIndex;
            else
            {
                int offset = hashIndex;
                bool loopEnds = false;

                while (slots[offset] != null)
                {
                    offset += this.step;
                    if (offset >= slots.Length)
                    {
                        loopEnds = true;
                        offset -= slots.Length;
                    }
                    if (loopEnds && offset >= hashIndex)
                        break;
                    if (slots[offset] == null)
                        return offset;
                }
            }

            return -1;
        }

        public int Put(string value)
        {
            // записываем значение по хэш-функции
            int index = SeekSlot(value);

            if (index != -1)
                slots[index] = value;
            
            // возвращается индекс слота или -1
            // если из-за коллизий элемент не удаётся разместить 
            return index;
        }

        public int Find(string value)
        {
            
            // находит индекс пустого слота для значения, или -1
            int hashIndex = HashFun(value);
            
            if (slots[hashIndex] == value)
                return hashIndex;
            else
            {
                int offset = hashIndex;
                bool loopEnds = false;

                while (slots[offset] != value)
                {
                    offset += this.step;
                    if (offset >= slots.Length)
                    {
                        loopEnds = true;
                        offset -= slots.Length;
                    }
                    if (loopEnds && offset >= hashIndex)
                        break;
                    if (slots[offset] == value)
                        return offset;
                }
            }
            
            // находит индекс слота со значением, или -1
            return -1;
        }
    }
 
    
    // наследуйте этот класс от HashTable
    // или расширьте его методами из HashTable
    public class PowerSet<T>
    {
        private readonly List<T> elements;
        
        
        public PowerSet()
        {
            elements= new List<T>();
        }

        public int Size()
        {
            return elements.Count;
        }

        public void Put(T value)
        {
            if (!Get(value))
            {
                elements.Add(value);
            }
        }

        public bool Get(T value)
        {
            // возвращает true если value имеется в множестве,
            // иначе false
            return elements.Contains(value);
        }

        public bool Remove(T value)
        {
            // возвращает true если value удалено
            // иначе false
            return elements.Remove(value);
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            // пересечение текущего множества и set2
            PowerSet<T> newSet = new PowerSet<T>();

            foreach (T element in elements)
            {
                if (set2.Get(element))
                {
                    newSet.Put(element);
                }
            }

            return newSet;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            PowerSet<T> newSet = new PowerSet<T>();

            foreach (T element in elements)
            {
                newSet.Put(element);
            }

            foreach (T element in set2.elements)
            {
                newSet.Put(element);
            }
            
            // объединение текущего множества и set2
            return newSet;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            PowerSet<T> newSet = new PowerSet<T>();
            
            foreach (T element in elements)
            {
                newSet.Put(element);
            }
            
            foreach (T item in set2.elements)
            {
                // удаляем все элементы второго множества, которые присутствуют в первом
                newSet.Remove(item);
            }
            
            // разница текущего множества и set2
            return newSet;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            
            foreach (T element in set2.elements)
            {
                if (!Get(element)) return false;
            }

            // возвращает true, если set2 есть
            // подмножество текущего множества,
            // иначе false
            return true;
        }
    }
}