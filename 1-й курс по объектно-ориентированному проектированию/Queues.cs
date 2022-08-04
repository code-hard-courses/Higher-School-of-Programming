/**
 abstract class ParentQueue<T>

  // конструктор
  public ParentQueue<T> ParentQueue();

  // команды
// постусловие: в хвост очереди добавлен новый элемент
  public void add_tail(T value);

// предусловие: очередь не пуста;
// постусловие: из головы очереди удалён элемент
  public void remove_front();

  // запросы
// предусловие: список не пуст
  public T get_front(); // значение элемента в голове очереди; 

  public int size(); // текущий размер очереди

  // запросы статусов (возможные значения статусов)
  public int get_remove_front_status(); // успешно; очередь пуста
  public int get_get_front_status(); // успешно; очередь пуста

abstract class Queue<T> : ParentQueue<T>

  // конструктор
  public Queue<T> Queue();

abstract class Deque<T> : ParentQueue<T>

  // конструктор
// постусловие: создана пустая очередь
  public Deque<T> Deque();

  // команды
// постусловие: в голову очереди добавлен новый элемент
  public void add_front(T value); 

// предусловие: очередь не пуста;
// постусловие: из хвоста очереди удалён элемент
  public void remove_tail(); 

  // запросы
// предусловие: список не пуст
  public T get_tail(); // значение элемента в хвосте очереди; 

  public int size(); // текущий размер очереди

  // запросы статусов (возможные значения статусов)
  public int get_remove_tail_status(); // успешно; очередь пуста
  public int get_get_tail_status(); // успешно; очередь пуста
 */


using System;
using System.Collections.Generic;


namespace lab6
{
    class BoundedStack<T>
    {
        //скрытые поля
        private List<T> stack; //основное хранилище стека
        private int status_pop; //статус команды pop()
        private int status_peek; //статус команды peek()
        private int status_push; //статус команды push()
        private int max_size; //максимальный размер стека

        //интерфейс класса, реализующий АТД BoundedStack
        public const int POP_NIL = 0; //pop() не вызывался
        public const int POP_OK = 1; //последний вызов pop() завершился успехом
        public const int POP_ERR = 2; //последний вызов pop() завершился неудачей - стек пуст
        public const int PEEK_NIL = 0; //peek() не вызывался
        public const int PEEK_OK = 1; //последний вызов peek() завершился успехом
        public const int PEEK_ERR = 2; //последний вызов peek() завершился неудачей - стек пуст
        public const int PUSH_NIL = 0; //push() не вызывался
        public const int PUSH_OK = 1; //последний вызов push() завершился успехом
        public const int PUSH_ERR = 2; //последний вызов push() завершился неудачей - стек переполнен

        //конструкторы
        public BoundedStack() //создаёт стек длиной 32
        {
            max_size = 32;
            clear();
        }

        public BoundedStack(int size) //создаёт стек длиной size; если size < 0, то по создаёт по умолчанию
        {
            if (size > 0)
            {
                stack = new List<T>(size);
                max_size = size;
                status_pop = POP_NIL;
                status_peek = PEEK_NIL;
                status_push = PUSH_NIL;
            }
            else
            {
                max_size = 32;
                clear();
            }
        }

        //команды
        public void push(T value) //добавляет элемент в стек
        {
            if (size() < max_size)
            {
                stack.Add(value);
                status_push = PUSH_OK;
            }
            else
            {
                status_push = PUSH_ERR;
            }
        }

        public void pop() //удаляет верхний элемент стека
        {
            if (size() > 0)
            {
                stack.RemoveAt(0);
                status_pop = POP_OK;
            }
            else
            {
                status_pop = POP_ERR;
            }
        }

        public void clear() //очищает стек
        {
            stack = new List<T>(max_size);
            status_pop = POP_NIL;
            status_peek = PEEK_NIL;
            status_push = PUSH_NIL;
        }

        //запросы
        public int size() //возвращает текущую длину стека
        {
            return stack.Count;
        }

        public T peek() //возвращает верхний элемент стека
        {
            T result;
            if (size() > 0)
            {
                result = stack[0];
                status_peek = PEEK_OK;
            }
            else
            {
                result = default(T);
                status_peek = PEEK_ERR;
            }

            return result;
        }

        //дополнительные запросы
        public int get_status_pop()
        {
            return status_pop;
        }

        public int get_status_peek()
        {
            return status_peek;
        }

        public int get_status_push()
        {
            return status_push;
        }
    }

    class ParentQueue<T>
    {
        //хранимые значения
        protected BoundedStack<T> put;
        protected BoundedStack<T> take;

        //статусы
        protected int status_peekFront;
        protected int status_removeFront;

        //интерфейс
        public const int PEEKFRONT_NIL = 0;
        public const int PEEKFRONT_OK = 1;
        public const int PEEKFRONT_ERR = 2;
        public const int REMOVEFRONT_NIL = 0;
        public const int REMOVEFRONT_OK = 1;
        public const int REMOVEFRONT_ERR = 2;

        //конструкторы
        public ParentQueue()
        {
            clear();
        }

        //добавляет элемент в конец очереди
        public void addTail(T item)
        {
            put.push(item);
        }

        //удаляет из головы  элемент
        public void removeFront()
        {
            status_removeFront = REMOVEFRONT_ERR;
            if (take.size() == 0)
            {
                reverseStack();
                if (take.size() > 0)
                {
                    take.pop();
                    status_removeFront = REMOVEFRONT_OK;
                }
            }
        }

        public void clear() //очищает очередь
        {
            put = new BoundedStack<T>();
            take = new BoundedStack<T>();
            status_peekFront = PEEKFRONT_OK;
            status_removeFront = REMOVEFRONT_ERR;
        }

        protected void reverseStack()
        {
            int n = put.size();
            for (int i = 0; i < n; i++)
            {
                take.push(put.peek());
                put.pop();
            }
        }

        //запросы

        public T peekFront(int i) //возвращает значение головы
        {
            status_peekFront = PEEKFRONT_ERR;
            if (put.size() > 0)
            {
                status_peekFront = PEEKFRONT_OK;
                return put.peek();
            }

            return default(T);
        }

        public int size() //возвращает текущую длину очереди
        {
            return take.size() + put.size();
        }

        //запросы статусов

        public int get_status_peekFront()
        {
            return status_peekFront;
        } //не вызывался; успех; неудача - очередь пуста

        public int get_status_removeFront()
        {
            return status_removeFront;
        } //не вызывался; успех; неудача - очередь пуста
    }

    class Queue<T> : ParentQueue<T>
    {
        //конструкторы
        Queue() : base()
        {
        }
    }

    class Deque<T> : ParentQueue<T>
    {
        //статусы
        protected int status_peekTail;
        protected int status_removeTail;

        //интерфейс
        public const int PEEKTAIL_NIL = 0;
        public const int PEEKTAIL_OK = 1;
        public const int PEEKTAIL_ERR = 2;
        public const int REMOVEFTAIL_NIL = 0;
        public const int REMOVEFTAIL_OK = 1;
        public const int REMOVEFTAIL_ERR = 2;

        //конструкторы
        Deque() : base()
        {
            status_peekTail = PEEKFRONT_NIL;
            status_removeTail = REMOVEFTAIL_NIL;
        }

        public void addFront(T item) //добавляет элемент в начало очереди
        {
            take.push(item);
        }


        public void removeTail() //удаляет элемент из хвоста
        {
            status_removeTail = REMOVETAIL_ERR;
            if (put.size() == 0)
            {
                reversebackStack();
                if (put.size() > 0)
                {
                    put.pop();
                    status_removeTail = REMOVEFTAIL_OK;
                }
            }
        }

        private void reversebackStack()
        {
            int n = take.size();
            for (int i = 0; i < n; i++)
            {
                put.push(take.peek());
                take.pop();
            }
        }

        //запросы

        //предусловие: очередь не пуста
        public T peekTail(int i) //возвращает значение хвоста
        {
            status_peekTail = PEEKTAIL_ERR;
            if (take.size() > 0)
            {
                status_peekTail = PEEKTAIL_OK;
                return take.peek();
            }

            return default(T);
        }

        //запросы статусов

        public int get_status_peekTail()
        {
            return status_peekTail;
        } //не вызывался; успех; неудача - очередь пуста

        public int get_status_removeTail()
        {
            return status_removeTail;
        } //не вызывался; успех; неудача - очередь пуста
    }
}