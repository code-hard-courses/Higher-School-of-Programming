using System;
using System.Collections.Generic;
using System.Collections;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {
        public int filter_len;
        public BitArray bloomFilter;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
            bloomFilter = new BitArray(filter_len);
            // создаём битовый массив длиной f_len ...
        }

        // хэш-функции
        public int Hash1(string str1)
        {
            int multy = 17;
            int code = 0;
            
            // 17
            for(int i=0; i<str1.Length; i++)
            {
                code = (code * multy + (int)str1[i]) % filter_len;
            }
            // реализация ...
            return code;
        }
        public int Hash2(string str1)
        {
            // 223
            int multy = 233;
            int code = 0;
            
            // 17
            for(int i=0; i<str1.Length; i++)
            {
                code = (code * multy + (int)str1[i]) % filter_len;
            }
            // реализация ...
            return 0;
        }

        public void Add(string str1)
        {
            // добавляем строку str1 в фильтр
            bloomFilter.Set(Hash1(str1), true);
            bloomFilter.Set(Hash2(str1), true);
        }

        public bool IsValue(string str1)
        {
            // проверка, имеется ли строка str1 в фильтре

            return bloomFilter.Get(Hash1(str1)) && bloomFilter.Get(Hash2(str1));
        }

    }
}