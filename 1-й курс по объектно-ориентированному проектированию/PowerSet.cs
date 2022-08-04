/**
 *
 * abstract class PowerSet<T> : HashTable<T>

  // конструктор
// постусловие: создано пустое множество
// на максимальное количество элементов sz
  public PowerSet<T> PowerSet(int sz); 

  // запросы
// возвращает пересечение текущего множества
// с множеством set
  public PowerSet<T> Intersection(PowerSet<T> set);

// возвращает объединение текущего множества
// и множества set
  public PowerSet<T> Union(PowerSet<T> set);

// возвращает разницу между текущим множеством
// и множеством set
  public PowerSet<T> Difference(PowerSet<T> set);

// проверка, будет ли set подмножеством
// текущего множества
  public bool IsSubset(PowerSet<T> set);
 *
 * Обратите внимание, что операции Intersection, Union и Difference не требуют ни предусловий, ни постусловий. Они замкнуты: корректно и полностью определены на типе данных (структуре данных) "множество". Более того, пользователю не нужно заботиться об ограничениях на максимальное количество элементов, например, при операции Union(): корректная реализация скрыто учтёт и задаст правильный максимальный размер итогового множества.
 */


using System;
using System.Collections.Generic;

namespace lab9
{
    class HashTable<T>
    {
        //хранимые значения
        protected int length; //изначальная длина таблицы
        protected T[] space; //пространство для хранения 
        private int step; //шаг для разрешения коллизий

        private int status_add;
        public const int ADD_NIL = 0;
        public const int ADD_OK = 1;
        public const int ADD_ERR = 2;

        //конструкторы
        public HashTable(int size)
        {
            length = size;
            step = length / 10;
            space = new T[length];
            status_add = ADD_NIL;
        }

        //команды

        //добавляет элемент в таблицу
        public void add(T value)
        {
            status_add = ADD_ERR;

            int index = hash(value);
            if (!is_member(value))
            {
                for (int i = 0; i < length / step; i++)
                {
                    if (space[(index + step * i) % length] == null)
                    {
                        space[(index + step * i) % length] = value;
                        status_add = ADD_OK;
                        break;
                    }
                }
            }
        }

        //удаляет элемент из таблицы
        public void remove(T value)
        {
            int index = hash(value);

            for (int i = 0; i < length / step; i++)
            {
                if (space[(index + step * i) % length].ToString() == value.ToString())
                {
                    space[(index + step * i) % length] = default(T);
                }
            }
        }

        //возвращает индекс элемента
        protected int hash(T value)
        {
            string temp = value.ToString();
            int sum = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                sum += temp[i];
            }

            return sum % length;
        }

        //запросы

        public bool is_member(T value) //true, если элемент содержится в таблице; false, если нет
        {
            int index = hash(value);
            for (int i = 0; i < length / step; i++)
            {
                if (space[(index + step * i) % length].ToString() == value.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        public int get_status_add()
        {
            return status_add;
        } //возвращает статус ADD_*
    }

    class PowerSet<T> : HashTable<T>
    {
        public PowerSet() : base(1000)
        {
        }

        //запросы

        public PowerSet<T> intersection(PowerSet<T> set) //возвращает пересечение множеств
        {
            PowerSet<T> result = new PowerSet<T>();
            for (int i = 0; i < length; i++)
            {
                if (space[i] != null)
                {
                    if (set.is_member(space[i]))
                    {
                        result.add(space[i]);
                    }
                }
            }

            return result;
        }

        public PowerSet<T> union(PowerSet<T> set) //возвращает объединение множеств
        {
            PowerSet<T> result = new PowerSet<T>();

            for (int i = 0; i < length; i++)
            {
                result.add(space[i]);
                if (!is_member(set.space[i])) result.add(set.space[i]);
            }

            return result;
        }

        public PowerSet<T> difference(PowerSet<T> set) //возвращает разницу множеств
        {
            PowerSet<T> result = new PowerSet<T>();
            for (int i = 0; i < length; i++)
            {
                if (space[i] != null)
                {
                    if (!set.is_member(space[i]))
                    {
                        result.add(space[i]);
                    }
                }
            }

            return result;
        }

        public bool is_subset(PowerSet<T> set) //true, если set является подмножеством текущего
        {
            int length_full = 0;
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                if (space[i] != null)
                {
                    length_full++;
                    if (set.is_member(space[i])) count++;
                }
            }

            if (length_full == count) return true;
            return false;
        }
    }
}