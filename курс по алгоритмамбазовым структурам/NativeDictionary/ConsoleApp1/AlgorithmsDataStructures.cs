using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    
    public class NativeDictionary<T>
    {
        public int size;
        public string [] slots;
        public T [] values;

        public NativeDictionary(int sz)
        {
            size = sz;
            slots = new string[size];
            values = new T[size];
        }

        public int HashFun(string key)
        {
            // всегда возвращает корректный индекс слота
            int hashIndex = 0;
            if (key == null) return hashIndex;
            foreach (var t in key)
            {
                hashIndex += t;
            }

            hashIndex %= this.size;
            return hashIndex;

        }

        public bool IsKey(string key)
        {
            return slots[HashFun(key)] == key;
        }

        public void Put(string key, T value)
        {
            // гарантированно записываем 
            // значение value по ключу key
            int index = HashFun(key);
            slots[HashFun(key)] = key;
            values[HashFun(key)] = value;
            // возвращается индекс слота или -1
            // если из-за коллизий элемент не удаётся разместить 
        }

        public T Get(string key)
        {
            // возвращает value для key, 
            // или null если ключ не найден
            return IsKey(key) ? values[HashFun(key)] : default(T);
        }
    } 
    
    
}