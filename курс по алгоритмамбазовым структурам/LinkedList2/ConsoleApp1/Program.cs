using System;
using AlgorithmsDataStructures;

namespace ConsoleApp1
{
    class AlgorithmsDataStructures
    {
        static void Main(string[] args)
        {
            
/*
*1. Добавьте в класс LinkedList2 метод поиска первого узла по его значению.
2. Добавьте в класс LinkedList2 метод поиска всех узлов по конкретному значению (возвращается список/массив найденных узлов).
3. Добавьте в класс LinkedList2 метод удаления одного узла по его значению.
4. Добавьте в класс LinkedList2 метод удаления всех узлов по конкретному значению.
5. Добавьте в класс LinkedList2 метод вставки узла после заданного узла.
6. Добавьте в класс LinkedList2 метод вставки узла самым первым элементом.
7. Добавьте в класс LinkedList2 метод очистки всего содержимого (создание пустого списка).
8. Напишите проверочные тесты для каждого из предыдущих заданий.
*/
            
            
            
            LinkedList2 sList = new LinkedList2();
            sList.AddInTail(new Node(111));
            sList.AddInTail(new Node(128));
            sList.AddInTail(new Node(256));
            sList.AddInTail(new Node(322));
            sList.AddInTail(new Node(127));
            sList.AddInTail(new Node(128));
            sList.AddInTail(new Node(11));
            //sList.AddInTail(new Node(322));
            //sList.AddInTail(new Node(111));
            /*sList.AddInTail(new Node(22256));
            sList.AddInTail(new Node(4234));
            
            sList.AddInTail(new Node(256));*/

            //Выводим данные на экран
            sList.WriteToConsole();
            Console.WriteLine();


            /*Console.WriteLine("1. Добавьте в класс LinkedList метод удаления одного узла по его значению.");
            sList.Remove(111);
            sList.WriteToConsole();*/

            /*Console.WriteLine("2. Добавьте в класс LinkedList метод удаления всех узлов по конкретному значению.");
            sList.RemoveAll(128);
            sList.WriteToConsole();*/

            /*Console.WriteLine("5. Добавьте в класс LinkedList метод вычисления длины списка.");
            Console.WriteLine("Количество = " + sList.Count());
            sList.WriteToConsole();*/

            /*Console.WriteLine("4. Добавьте в класс LinkedList метод поиска всех узлов по конкретному значению (возвращается список/массив найденных узлов).");
            Console.WriteLine(sList.Find(128).value);*/


             /*Console.WriteLine("6. Добавьте в класс LinkedList метод вставки узла после заданного узла.");
             sList.InsertAfter(null, new Node(1));
             sList.WriteToConsole();*/

            sList.InsertAfter(new Node(127), new Node(42));
            sList.WriteToConsole();
            

            /*Console.WriteLine("3. Добавьте в класс LinkedList метод очистки всего содержимого (создание пустого списка).");
            sList.Clear();
            sList.WriteToConsole();*/
        }
    }
}