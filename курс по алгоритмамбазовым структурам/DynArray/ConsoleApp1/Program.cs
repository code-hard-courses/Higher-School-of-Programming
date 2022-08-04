using System;
using AlgorithmsDataStructures;

namespace ConsoleApp1
{
    class AlgorithmsDataStructures
    {
        static void Main(string[] args)
        {
            
            DynArray<int> testDynArr = new DynArray<int>();
            int item = 0;

            for (item = 1; item < 18; item++)
            {
                testDynArr.Append(item);
            }
            
            Console.WriteLine("Заполненный массив:");
            testDynArr.Print();

            for (int i = 0; i < 2; i++)
            {
                testDynArr.Remove(3);
                testDynArr.Print();
                Console.WriteLine("===============================================");
            }

            Console.ReadKey();
            
        }
    }
}