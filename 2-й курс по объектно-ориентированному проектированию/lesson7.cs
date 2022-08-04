using System;
using System.Collections.Generic;

namespace Lesson7
{
    class Program
    {
        class Book
        {
            public virtual void show_type()
            {
                Console.WriteLine("book");
            }
        }

        class FictionBook : Book
        {
            public override void show_type()
            {
                Console.WriteLine("fiction");
            }
        }


        // static void Main(string[] args)
        // {
        //     Book x = new Book();
        //     x.show_type(); //вывод book
        //     FictionBook w = new FictionBook();
        //     w.show_type(); //вывод fiction
        //     Book h = new FictionBook();
        //     h.show_type(); //во время выполнения программы свяжется с методом класса FictionBook, вывод fiction
        //     Console.WriteLine();
        // }
    }
}