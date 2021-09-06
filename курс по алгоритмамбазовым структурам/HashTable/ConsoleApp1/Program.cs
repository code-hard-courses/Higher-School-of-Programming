using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{
    class MainProgramm
    {
        static void Main(string[] args)
        {

            HashTable table = new HashTable(23, 5);
            
            int colCounter1 = 0;
            
            for (int i = 0; i < table.size * 2; i++)
            {
                string value = "" + (char)(i + 33);
                Console.WriteLine("VALUE = "+value);
                Console.WriteLine("HashFun = "+table.HashFun(value));
                int putSlot = table.Put(value);
                Console.WriteLine("putSlot = "+putSlot);
                if (putSlot == -1) ++colCounter1;
            }
            System.Console.WriteLine("Число коллизий\n хэш-функции1:{0}\n", colCounter1);
            Console.ReadKey();
        }
    }
}