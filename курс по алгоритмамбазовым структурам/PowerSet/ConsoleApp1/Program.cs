using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{
    class MainProgramm
    {
        static void Main(string[] args)
        {

            PowerSet<int> table = new PowerSet<int>();
            
            
            for (int i = 0; i < 10; i++)
            {
                table.Put(i);
                table.Put(i);
            }
            Console.ReadKey();
        }
    }
}