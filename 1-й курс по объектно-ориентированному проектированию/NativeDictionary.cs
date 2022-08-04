/**
 *abstract class NativeDictionary<T>

  // конструктор
// постусловие: создан пустой ассоциативный массив
  public NativeDictionary<T> NativeDictionary();

  // команды
// постусловие: в массив добавлена новая пара ключ-значение, 
// если данный ключ отсутствовал;
// в противном случае обновлено значение
// для соответствующего ключа
  public void put(string key, T value);

// предусловие: ключ key присутствует в массиве
// постусловие: ключ удаляется вместе со своим значением
  public void remove(string key);

  // запросы
// предусловие: ключ key присутствует в массиве
  public T get(string key);

// данный запрос требуется отдельно, 
// чтобы не использовать вместо него
// второстепенный запрос проверки статуса запроса get
  public bool is_key(string key); // проверка наличия ключа в массиве

  public int size(); // текущий размер массива

  // запросы статусов (возможные значения статусов)
  public int get_put_status();
    // успешно добавлен новый ключ и значение; 
    // успешно обновлено значение существующего ключа

  public int get_remove_status(); // успешно; ключ отсутствует

  public int get_get_status(); // успешно; ключ отсутствует
 *
 * 
 */


using System;
using System.Collections.Generic;

namespace lab8
{
    class NativeDictionary<T>
    {
        //хранимые значения
        private string[] keys; //ключи
        private T[] values; //значения
        private int length; //изначальная длина таблицы
        private int length_new; //текущая длина таблицы

        private int status_get;
        public const int GET_NIL = 0;
        public const int GET_OK = 1;
        public const int GET_ERR = 2;

        //конструкторы
        public NativeDictionary(int size)
        {
            length = size;
            length_new = size;
            keys = new string[length];
            values = new T[length];

            status_get = GET_NIL;
        }

        //команды

        //добавляет элемент по указанному ключу
        public void put(string key, T value)
        {
            int index = hash(key);
            if (keys[index] == null || keys[index] == key)
            {
                keys[index] = key;
                values[index] = value;
            }
            else
            {
                reallocation();
                keys[length_new - length + index] = key;
                values[length_new - length + index] = value;
            }
        }

        //удаляет ключ и элемент 
        public void remove(string key)
        {
            int index = hash(key);
            for (int i = index; i < length_new; i += length)
            {
                if (keys[index] == key)
                {
                    keys[index] = null;
                    values[i] = default(T);
                    break;
                }
            }
        }

        //вычисляет индекс слота по ключу
        private int hash(string key)
        {
            int sum = 0;
            for (int i = 0; i < key.Length; i++)
            {
                sum += key[i];
            }

            return sum % length;
        }

        //увеличивает размер буфера
        private void reallocation()
        {
            length_new += length;
            string[] temp_keys = new string[length];
            T[] temp_values = new T[length];

            Array.Copy(keys, temp_keys, length);
            Array.Copy(values, temp_values, length);

            keys = new string[length_new];
            values = new T[length_new];
            Array.Copy(temp_keys, keys, length);
            Array.Copy(temp_values, values, length);
        }

        //запросы

        public bool is_key(string key) //true, если ключ содержится в таблице; false, если нет
        {
            int index = hash(key);
            for (int i = index; i < length_new; i += length)
            {
                if (keys[index] == key) return true;
            }

            return false;
        }

        public T get_value(string key) //возвращает элемент по указанному ключу
        {
            status_get = GET_ERR;
            if (is_key(key))
            {
                return default(T);
            }
            else
            {
                status_get = GET_OK;
                int index = hash(key);
                int x = 0;
                for (int i = index; i < length_new; i += length)
                {
                    if (keys[index] == key) x = i;
                }

                return values[x];
            }
        }

        //запросы статусов

        public int get_status_get()
        {
            return status_get;
        } //не вызывался; успех; неудача - ключа нет в таблице
    }
}