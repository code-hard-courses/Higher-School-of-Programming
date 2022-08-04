/**
 *
 * abstract class BloomFilter<T>

  // конструктор
// постусловие: создан пустой фильтр Блюма заданного размера
  public BloomFilter<T> BloomFilter(int sz);

  // команды
// постусловие: в фильтр добавлено новое значение
  public void put(T value); 

  // запросы
public bool is_value(T value);
  // содержится ли значение в фильтре
  // (допускаются ложноположительные срабатывания)
 * 
 */


using System;
using System.Collections;

namespace lab10
{
    class BloomFilter<T>
    {
        //хранимые значения
        private int filter_length; //длина фильтра
        private BitArray bits; //битовый массив

        //конструктор
        public BloomFilter(int size)
        {
            filter_length = size;
            bits = new BitArray(size, false);
        }

        //команды

        public void add(T value) //добавляет новое значение
        {
            int[] h1 = new int[1] {hash1(value)};
            int[] h2 = new int[1] {hash2(value)};
            BitArray b1 = new BitArray(h1);
            BitArray b2 = new BitArray(h2);
            bits.Or(b1);
            bits.Or(b2);
        }

        // хэш-функции
        private int hash1(T value)
        {
            // 17
            string str = value.ToString();
            int res = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int code = (int) str[i];
                res = (res * 17 + code) % 128;
            }

            return res;
        }

        private int hash2(T value)
        {
            // 223
            string str = value.ToString();
            int res = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int code = (int) str[i];
                res = (res * 223 + code) % 128;
            }

            return res;
        }

        //запросы
        public bool is_value(T value) //true, если элемент содержится в таблице; false, если нет
        {
            int h1 = hash1(value);
            int h2 = hash2(value);
            BitArray temp = new BitArray(filter_length);
            BitArray b1 = new BitArray(BitConverter.GetBytes(h1));
            BitArray b2 = new BitArray(BitConverter.GetBytes(h2));
            temp.Or(b1);
            temp.Or(b2);

            BitArray res = new BitArray(filter_length);
            BitArray temp1 = new BitArray(temp);
            res = temp1.And(bits);

            for (int i = 0; i < filter_length; i++)
            {
                if (temp[i] != res[i]) return false;
            }

            return true;
        }
    }
}