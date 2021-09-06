using System;
using TestSpace;
using System.Collections.Generic;

namespace TestSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            BoundedStack<int> stack = new BoundedStack<int>();
            
            
            stack.push(10);
            stack.push(20);
            stack.push(30);
            stack.push(40);
            stack.push(50);
            
            Console.WriteLine(stack);
        }
    }
}