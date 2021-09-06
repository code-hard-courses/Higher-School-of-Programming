using System;
using SortSpace;
using System.Collections.Generic;

namespace SortSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int [4] {1,2,3,4};
            
            bool result= SortLevel.GallopingSearch(array,14);
            Console.WriteLine(result);
        }
    }
}