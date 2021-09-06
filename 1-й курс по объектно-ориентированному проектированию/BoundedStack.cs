using System;
using System.Collections.Generic;

namespace TestSpace
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

        public BoundedStack() 
        {
            max_size = 32;
            clear();
        }

        public BoundedStack(int size) 
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
}