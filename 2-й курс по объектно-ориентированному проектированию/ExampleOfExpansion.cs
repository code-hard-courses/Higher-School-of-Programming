using System;

namespace TestSpace
{
    /**
    * Класс животное
    */
    public class Animal 
    {
        public int CountLeg;
        public int CountEyes;
    }
    
    /**
    * Класс животное кошка
    */
    public class Cat : Animal
    {
        public int CountLeg=4;
        public int CountEyes=2;
    }

    public class Person 
    {
        public string Name;
    }
    
    /**
    * Класс животное кошка
    */
    public class Employee  : Person
    {
        public string Company;
    }
    
    
}