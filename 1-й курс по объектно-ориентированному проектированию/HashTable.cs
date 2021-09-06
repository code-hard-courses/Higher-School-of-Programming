/**
 * abstract class HashTable<T>

  // конструктор
// постусловие: создана пустая хэш-таблица заданного размера
  public HashTable<T> HashTable(int sz); 

  // команды
// предусловие: в таблице имеется свободный слот для value;
// постусловие: в таблицу добавлено новое значение
  public void put(T value); 

// предусловие: в таблице имеется значение value;
// постусловие: из таблицы удалено значение value
  public void remove(T value); 

  // запросы
  public bool get(T value); // содержится ли значение value в таблице

  public int size(); // количество элементов в таблице

  // запросы статусов (возможные значения статусов)
  public int get_put_status(); // успешно; 
    // система коллизий не смогла найти свободный слот для значения

  public int get_remove_status(); // успешно; значения нету в таблице
 */


using System;
using System.Collections.Generic;

namespace lab7
{
    class HashTable<T>
    {
        //хранимые значения
        private int length; //изначальная длина таблицы
        private int length_new; //текущая длина таблицы
        private T[] space; //пространство для хранения 

        //конструкторы
        public HashTable(int size)
        {
            length = size;
            length_new = size;
            space = new T[length];
        }

        //команды

        //добавляет элемент в таблицу
        public void add(T value)
        {
            bool flag = false;
            int index = hash(value);
            for (int i = index; i < length_new; i += length)
            {
                if (space[index].ToString() == default(T).ToString())
                {
                    space[index] = value;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                reallocation();
                space[length_new - length + index] = value;
            }
        }

        //удаляет элемент из таблицы
        public void remove(T value)
        {
            int index = hash(value);
            for (int i = index; i < length_new; i += length)
            {
                if (space[i].ToString() == value.ToString())
                {
                    space[i] = default(T);
                    break;
                }
            }
        }

        //возвращает индекс элемента
        private int hash(T value)
        {
            string temp = value.ToString();
            int sum = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                sum += temp[i];
            }

            return sum % length;
        }

        //увеличивает размер буфера
        private void reallocation()
        {
            length_new += length;
            T[] temp = new T[length];
            Array.Copy(space, temp, length);

            space = new T[length_new];
            Array.Copy(temp, space, length);
        }

        //запросы

        //true, если элемент содержится в таблице; false, если нет
        public bool is_member(T value)
        {
            int index = hash(value);
            for (int i = index; i < length_new; i += length)
            {
                if (space[i].ToString() == value.ToString()) return true;
            }

            return false;
        }
    }
}