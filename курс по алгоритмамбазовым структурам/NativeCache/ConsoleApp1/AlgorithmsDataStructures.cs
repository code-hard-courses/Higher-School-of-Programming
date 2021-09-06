using System;
using System.Linq;

namespace AlgorithmsDataStructures
{
    
    public class NativeCache<T>
    {
        public int size;
        public string [] slots;
        public T [] values;
        public int [] hits;
        

        public NativeCache(int sz)
        {
            size = sz;
            slots = new string[size];
            values = new T[size];
            hits = new int[size];
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
            if (slots[HashFun(key)] == key)
            {
                hits[HashFun(key)]++;
                return true;
            }

            return false;
        }

        public void Put(string key, T value)
        {
            
            if (slots[HashFun(key)] == null)
            {
                // записываем значение ключа по хэш-функции
                slots[HashFun(key)] = key;
                // гарантированно записываем 
                // значение value по ключу key
                values[HashFun(key)] = value;
            }
            else
            {
                // записываем ключ key и значение value в слот с минимальным числом обращений
                slots[Array.IndexOf(hits, hits.Min())] = key;
                values[Array.IndexOf(hits, hits.Min())] = value;
            }
        }

        public T Get(string key)
        {
            // возвращает value для key, 
            // или null если ключ не найден
            return IsKey(key) ? values[HashFun(key)] : default(T);
        }
        
    } 
    
    
}