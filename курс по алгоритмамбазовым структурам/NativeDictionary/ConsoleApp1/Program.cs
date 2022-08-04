using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{
    class MainProgramm
    {
        static void Main(string[] args)
        {

            NativeDictionary<string> table = new NativeDictionary<string>(23);
            
            int colCounter1 = 0;
            
            for (int i = 0; i < table.size * 2; i++)
            {
                string value = "" + (char)(i + 33);
                Console.WriteLine("VALUE = "+value);
                Console.WriteLine("HashFun = "+table.HashFun(value));
                table.Put(value,value);
            }
            System.Console.WriteLine("Число коллизий\n хэш-функции1:{0}\n", colCounter1);
            Console.ReadKey();
        }
    }
}