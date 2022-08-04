  
// АТД Queue
/*
 abstract class Queue<T>
  //конструкторы
   //постусловие: создана пустая очередь
   public Queue<T> ();
   
   //команды
   //постусловие: элемент добавлен в конец очереди
   public void enqueue (T item);
   
   //предусловие: очередь не пуста
   //постусловие: из головы удалён элемент
   public void dequeue ();
     
     // запросы
// предусловие: очередь не пуста
  public T get(); // получить элемент из головы очереди; 

  public int size(); // текущий размер очереди

  // запросы статусов (возможные значения статусов)
  public int get_dequeue_status(); // успешно; очередь пуста
  public int get_get_status(); // успешно; очередь пуста
 */


using System;
using System.Collections.Generic;

namespace lab5
{
    class Queue<T>
    {
        //хранимые значения
        private T[] space;
        private int head;
        private int tail;
        private int length;
        private int capacity;

        //статусы
        private int status_peek;
        private int status_dequeue;

        //интерфейс
        public const int PEEK_NIL = 0;
        public const int PEEK_OK = 1;
        public const int PEEK_ERR = 2;
        public const int DEQUEUE_NIL = 0;
        public const int DEQUEUE_OK = 1;
        public const int DEQUEUE_ERR = 2;

        //конструкторы
        public Queue() { clear();  }

        //добавляет элемент в хвост
        public void enqueue(T item)
        {
            if (istail()) moveheadtail();
            if (isfull()) reallocation();
            tail++;
            space[tail] = item;
            length++;
        }

        //удаляет элемент головы
        public void dequeue()
        {
            status_dequeue = DEQUEUE_ERR;
            if (length > 0)
            {
                space[head] = default(T);
                head++;
                length--;
                if (ishead()) moveheadtail();
                status_dequeue = DEQUEUE_OK;
            }
        }

        private void moveheadtail() //смещает голову к началу массива
        {
            for (int i = 0; i < length; i++)
            {
                space[i] = space[head + i];
            }
        }

        private void reallocation() //увеличивает размер массива
        {
            int new_capacity = capacity * 2;

            T[] temp = new T[length];
            Array.Copy(space, temp, length);
            space = new T[new_capacity];
            Array.Copy(temp, space, length);
        }

        public void clear() //очищает очередь
        {
            head = 0;
            tail = 0;
            capacity = 10;
            space = new T[capacity];
            length = 0;
            status_dequeue = DEQUEUE_NIL;
            status_peek = PEEK_NIL;
        }
       
        //запросы

        public T peek(int i) //возвращает значение головы
        {
            status_peek = PEEK_ERR;
            if (length > 0)
            {
                status_peek = PEEK_OK;
                return space[head];
            }
            return default(T);
        } 

        public int size() //возвращает текущую длину очереди
        {
            return length;
        } 

        private bool ishead() //возвращает true, если голова смещена на четверть длины
        {
            if (head >= capacity / 4) return true;
            return false;
        } 

        private bool istail() //возвращает true, если хвост достиг конца массива
        {
            if (tail == capacity - 1) return true;
            return false;
        } 

        private bool isfull() //возвращает true, если весь массив заполнен
        {
            if (length == capacity) return true;
            return false;
        }

        //запросы статусов

        public int get_status_dequeue() { return status_dequeue; } //не вызывался; успех; неудача - очередь пуста
        public int get_status_peek() { return status_peek; }    //не вызывался; успех; неудача - очередь пуста
    }
}