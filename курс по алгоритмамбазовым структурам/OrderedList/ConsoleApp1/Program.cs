using System;
using System.Linq;

namespace AlgorithmsDataStructures
{
    class MainProgramm
    {
        static void Main(string[] args)
        {
            OrderedList<int> ascList = new OrderedList<int>(true);

            ascList.Add(12);
            ascList.Add(0);
            ascList.Add(2);
            ascList.Add(12);
            ascList.Add(12);
            ascList.Add(12);
            ascList.Add(0);
            ascList.Add(0);
            ascList.Add(1);
            ascList.Add(1);
            ascList.Add(13);
            Console.WriteLine(ascList.Print());

            //ascList.Delete(3);
            ascList.Delete(13);
            
            //ascList.Delete(12);
            Console.WriteLine(ascList.Print());

            Console.ReadKey();
        }
    }
}