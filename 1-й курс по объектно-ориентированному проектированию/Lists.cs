//АТД ParentList
/*
abstract class ParentList<T>
      // конструктор
// постусловие: создан новый пустой список
  public ParentList<T> ParentList();
      //команды
//предусловие: список не пуст
//постусловие: курсор установлен на первый узел в списке
public void head();
//предусловие: список не пуст
//постусловие: курсор установлен на последний узел в списке
public void tail();
//предусловие: список не пуст; курсор указывает на не последний элемент
//постусловие: курсор сдвинут на один узел вправо
public void right();
//предусловие: список не пуст;
//постусловие: в список добавлен элемент справа
public void put_right(T value);
//предусловие: список не пуст;
//постусловие: в список добавлен элемент слева
public void put_left(T value);
//предусловие: список не пуст
//постусловие: из списка удалён элемент; курсор смещён к правому соседу, если он есть, в противном случае курсор смещён к левому соседу, если он есть
public void remove();
//постусловие: из списка удаляются все значения
public void clear();
//постусловие: в список добавлен элемент (в конец)
public void add_tail(T value); //можно представить через операции tail() и put_right(T value)
//постусловие: удаление текущего значения и добавление нового на его место
public void replace (T value); //можно представить через операции put_right(T value) и remove() 
//предусловие: искомое значение есть в списке
//постусловие: изменение значения курсора
public void find(T value); //можно представить через операцию right() с проверкой соответветствия
//постусловие: удаление элементов из списка с заданным значением
public void remove_all(T value); 
     //запросы
//предусловие: список не пуст
public T get(); //возвращает значение текущего элемента
public int size(); //вовзращает количество узлов списка
    //запросы статусов
public bool is_head(); //возвращает статус курсора; true, если в начале списка
public bool is_tail(); //возвращает статус курсора; true, если в конце списка
public bool is_value(); //возвращает статус курсора; true, если список не пуст
public int get_status_head();  //возвращает статус HEAD_*
public int get_status_tail();  //возвращает статус TAIL_*
public int get_status_right(); //возвращает статус RIGHT_*
public int get_status_get();   //возвращает статус GET_*
public int get_status_find();  //возвращает статус FIND_*
public int get_status_remove() //возвращает статус REMOVE_*
public int get_status_replace() //возвращает статус RERPLACE_*
public int get_status_put_right() //возвращает статус PUT_RIGHT_*
public int get_status_put_left()  //возвращает статус PUT_LEFT_*
*/

//АТД LinkedList
/*
 abstract class LinkedList<T> : ParentList<T>
       //конструктор
 public LinkedList<T> LinkedList() : ParentList();
 */

//АТД TwoWayList
/*
 abstract class TwoWayList<T> : ParentList<T>
      //конструктор
 public TwoWayList<T> TwoWayList() : ParentList();
public const int LEFT_NIL = 0;  //left() не вызывался
public const int LEFT_OK = 1;   //последний вызов left() завершился успехом
public const int LEFT_ERR = 2;  //последний вызов left() завершился неудачей - список пуст
//предусловие: список не пуст; курсор указывает на не первый элемент
//постусловие: изменение значения курсора
public void left();
public int get_status_left();  //возвращает статус LEFT_*
 */

using System;
using System.Collections.Generic;

namespace lab3
{
    class ParentList<T>
    {
        //статусы 
        protected int status_head;
        protected int status_tail;
        protected int status_right;
        protected int status_remove;
        protected int status_replace;
        protected int status_get;
        protected int status_find;
        protected int status_put_right;
        protected int status_put_left;

        //хранимые значения
        protected int head_mark; //указатель на голову
        protected int tail_mark; //указатель на хвост
        protected int cursor; //указатель на текущее положение курсора
        protected T[] space;  //массив для хранения данных
        protected int[] next; //массив указателей на следующую ячейку
        protected int[] prev; //массив указателей на предыдущую ячейку
        protected int maxlength; //максимальная длина списка

        //интерфейс класса, реализующий АТД ParentList
        public const int HEAD_NIL = 0;   //head() не вызывался
        public const int HEAD_OK = 1;    //последний вызов head() завершился успехом
        public const int HEAD_ERR = 2;   //последний вызов head() завершился неудачей - список пуст
        public const int TAIL_NIL = 0;   //tail() не вызывался
        public const int TAIL_OK = 1;    //последний вызов tail() завершился успехом
        public const int TAIL_ERR = 2;   //последний вызов tail() завершился неудачей - список пуст
        public const int RIGHT_NIL = 0;  //right() не вызывался
        public const int RIGHT_OK = 1;   //последний вызов right() завершился успехом
        public const int RIGHT_ERR = 2;  //последний вызов right() завершился неудачей - список пуст
        public const int RIGHT_ERR_TAIL = 3; //последний вызов right() завершился неудачей - курсор указывает на элемент в конце
        public const int REMOVE_NIL = 0; //remove() не вызывался
        public const int REMOVE_OK = 1;  //последний вызов remove() завершился успехом
        public const int REMOVE_ERR = 2; //последний вызов remove() завершился неудачей - список пуст
        public const int GET_NIL = 0;    //get() не вызывался
        public const int GET_OK = 1;     //последний вызов get() завершился успехом
        public const int GET_ERR = 2;    //последний вызов get() завершился неудачей - список пуст
        public const int FIND_NIL = 0;    //find() не вызывался
        public const int FIND_OK = 1;     //последний вызов find() завершился успехом
        public const int FIND_ERR = 2;    //последний вызов find() завершился неудачей - искомого значения нет в списке
        public const int REPLACE_NIL = 0; //relpace() не вызывался
        public const int REPLACE_OK = 1;  //последний вызов replace() завершился успехом
        public const int REPLACE_ERR = 2; //последний вызов replace() завершился неудачей - список пуст
        public const int PUT_RIGHT_NIL = 0; //put_right() не вызывался
        public const int PUT_RIGHT_OK = 1;  //последний вызов put_right() завершился успехом
        public const int PUT_RIGHT_ERR = 2; //последний вызов put_right() завершился неудачей - список пуст
        public const int PUT_LEFT_NIL = 0;  //put_left() не вызывался
        public const int PUT_LEFT_OK = 1;   //последний вызов put_left() завершился успехом
        public const int PUT_LEFT_ERR = 2;  //последний вызов put_left() завершился неудачей - список пуст

        //конструкторы
        public ParentList()
        {
            clear();
        }
        
        public void head() //устанавливет курсор в начало списка
        {
            status_head = HEAD_ERR;
            if (head_mark != -1) { cursor = head_mark; status_head = HEAD_OK; }
        }

        public void tail() //устанавливет курсор в конец списка 
        {
            status_head = TAIL_ERR;
            if (head_mark != -1) { cursor = tail_mark; status_tail = TAIL_OK; }
        }

        public void right() //устанавливает курсор к следующему значению списка 
        {
            status_head = RIGHT_ERR;
            if (head_mark != -1)
            {
                if (cursor == tail_mark) { status_right = RIGHT_ERR_TAIL; }
                else { cursor = next[cursor]; status_right = RIGHT_OK; }
            }
        }

        protected int get_cell () //возвращает индекс свободного элемента (в случае превышения размеров списка - реаллокация) 
        {
            for (int i = 0; i < maxlength; i++)
            {
                if (next[i] == -1 && i != tail_mark) return i;
            }
            reallocation(); return maxlength / 2; 
        }

        protected void reallocation () //увеличивает размер буфера для хранения списка 
        {
            int oldlength = maxlength;
            maxlength *= 2;
            T[] temp_space = new T[oldlength];
            int[] temp_next = new int[oldlength];
            int[] temp_prev = new int[oldlength];
            Array.Copy(space, temp_space, oldlength);
            Array.Copy(next, temp_next, oldlength);
            Array.Copy(prev, temp_prev, oldlength);
            space = new T[maxlength];
            next = new int[maxlength];
            prev = new int[maxlength];
            Array.Copy(temp_space, space, oldlength);
            Array.Copy(temp_next, next, oldlength);
            Array.Copy(temp_prev, prev, oldlength);
        }

        public void put_right(T value) //добавляет элемент справа от курсора 
        {
            status_put_right = PUT_RIGHT_ERR;
            if (head_mark != -1)
            {
                int free_index = get_cell();
                space[free_index] = value;

                if (cursor == tail_mark)
                {
                    next[cursor] = free_index;
                    prev[free_index] = cursor;
                    next[free_index] = -1;
                    tail_mark = free_index;
                }
                else
                {
                    int next_index = next[cursor];
                    next[cursor] = free_index;
                    prev[free_index] = cursor;
                    next[free_index] = next_index;
                    prev[next_index] = free_index;
                }
                status_put_right = PUT_RIGHT_OK;
            }
        }

        public void put_left(T value) //добавляет элемент слева от курсора 
        {
            status_put_left = PUT_LEFT_ERR;
            if (head_mark != -1)
            {
                int free_index = get_cell();
                space[free_index] = value;

                if (cursor == head_mark)
                {
                    prev[cursor] = free_index;
                    prev[free_index] = -1;
                    next[free_index] = cursor;
                    head_mark = free_index;
                }
                else
                {
                    int prev_index = prev[cursor];
                    prev[cursor] = free_index;
                    next[free_index] = cursor;
                    next[prev_index] = free_index;
                    prev[free_index] = prev_index;
                }
                status_put_left = PUT_LEFT_OK;
            }
        }
  
        public void remove() //удаляет текущий элемент 
        {
            status_remove = REMOVE_ERR;
            if (head_mark != -1)
            {
                space[cursor] = default(T);
                if (cursor == head_mark)
                {
                    int next_index = next[cursor];
                    next[cursor] = -1;
                    prev[next_index] = -1;
                    head_mark = next_index;
                    cursor = next_index;
                }
                else if (cursor == tail_mark)
                {
                    int prev_index = prev[cursor];
                    prev[cursor] = -1;
                    next[prev_index] = -1;
                    tail_mark = prev_index;
                    cursor = prev_index;
                }
                else
                {
                    int next_index = next[cursor];
                    int prev_index = prev[cursor];
                    next[prev_index] = next_index;
                    prev[next_index] = prev_index;
                    next[cursor] = -1;
                    prev[cursor] = -1;
                    cursor = next_index;
                }
                status_remove = REMOVE_OK;
            }
        }

        public void clear() //очищение списка
        {
            maxlength = 10;
            head_mark = -1;
            tail_mark = -1;
            cursor = -1;
            next = new int[maxlength];
            prev = new int[maxlength];
            space = new T[maxlength];
            status_head = HEAD_NIL;
            status_tail = TAIL_NIL;
            status_right = RIGHT_NIL;
            status_remove = REMOVE_NIL;
            status_get = GET_NIL;
            status_find = FIND_NIL;
            status_put_right = PUT_RIGHT_NIL;
            status_put_left = PUT_LEFT_NIL;

            for (int i = 0; i < maxlength; i++)
            {
                next[i] = -1;
                prev[i] = -1;
            }
        }

        public void add_tail(T value) //добавляет элемент в конец списка 
        {
            if (head_mark == -1)
            {
                head_mark = 0;
                tail_mark = 0;
                cursor = 0;
                space[cursor] = value;
            }
            else
            {
                tail(); put_right(value);
            }
        } 

        public void replace(T value) //удаляет текущий элемент и добавляет новый на его место 
        {
            status_remove = REMOVE_ERR;
            if (head_mark != -1)
            {
                put_right(value);
                remove();
                status_remove = REMOVE_OK;
            }
        } 

        //находит первое вхождение элемента из списка
        public void find(T value)
        {
            status_find = FIND_ERR;
            if (head_mark != -1)
            {
                status_find = FIND_ERR;
                cursor = head_mark;
                status_right = RIGHT_NIL;
                while (status_right != RIGHT_ERR_TAIL)
                {
                    if (value.ToString() == space[cursor].ToString())
                    {
                        status_find = FIND_OK;
                        break;
                    }
                }
            }
        } 

        //удаляет все заданные элементы из списка
        public void remove_all(T value)
        {
            if (head_mark != -1)
            {
                cursor = head_mark;
                status_right = RIGHT_NIL;
                while (status_right != RIGHT_ERR_TAIL)
                {
                    find(value);
                    remove();
                }
            }
        }

        //запросы

        public T get() //значение текущего элемента
        {
            if (head_mark != -1)
            {
                status_get = GET_OK;
                return space[cursor];
            }
            else
            {
                status_get = GET_ERR;
                return default(T);
            }
        } 

        public int size() //количество узлов списка
        {
            int count = 0;
            if (head_mark != -1)
            {
                int current = head_mark;
                while (next[current] != -1)
                {
                    count++;
                    current = next[current];
                }
            }
            return count;
        } 

        //запросы статусов

        public bool is_head() //статус курсора; true, если в начале списка
        {
            if (head_mark != -1 && head_mark == cursor) return true;
            return false;
        } 
        public bool is_tail() //статус курсора; true, если в конце списка
        {
            if (head_mark != -1 && tail_mark == cursor) return true;
            return false;
        } 
        public bool is_value() //статус курсора; true, если список не пуст
        {
            if (head_mark != -1) return true;
            return false;
        } 

        public int get_status_head() { return status_head; }  //возвращает статус HEAD_*
        public int get_status_tail() { return status_tail; }  //возвращает статус TAIL_*
        public int get_status_right() { return status_right; } //возвращает статус RIGHT_*
        public int get_status_get() { return status_get; }   //возвращает статус GET_*
        public int get_status_find() { return status_find; }  //возвращает статус FIND_*
        public int get_status_remove() { return status_remove; }  //возвращает статус REMOVE_*
        public int get_status_replace() { return status_replace; }  //возвращает статус RERPLACE_*
        public int get_status_put_right() { return status_put_right; } //возвращает статус PUT_RIGHT_*
        public int get_status_put_left() { return status_put_left; } //возвращает статус PUT_LEFT_*
    }


    class LinkedList<T> : ParentList<T>
    {
        public LinkedList() : base() { } 

    }

    class TwoWayList<T> : ParentList<T>
    {
        public int status_left;

        public const int LEFT_NIL = 0;  //left() не вызывался 
        public const int LEFT_OK = 1;   //последний вызов left() завершился успехом
        public const int LEFT_ERR = 2;  //последний вызов left() завершился неудачей - список пуст
        public const int LEFT_ERR_HEAD = 3;  //последний вызов left() завершился неудачей - курсор указывает на голову

        public TwoWayList() : base() { }

        //предусловие: список не пуст; курсор указывает на не первый элемент
        //постусловие: изменение значения курсора
        public void left()
        {
            status_left = LEFT_ERR;
            if (head_mark != -1)
            {
                if (cursor == head_mark) { status_left = LEFT_ERR_HEAD; }
                else { cursor = prev[cursor]; }
            }
        }

        public int get_status_left() { return status_left; }  //возвращает статус LEFT_*

    }
}