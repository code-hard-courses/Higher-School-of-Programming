using System;
using System.Collections.Generic;

class Base
{
    public static void PrintBases(IEnumerable<Base> bases)
    {
        foreach(Base b in bases)
        {
            Console.WriteLine(b);
        }
    }

    public virtual void Method()
    {
        Console.WriteLine("Base method");
    }
}

class Derived : Base
{
    public override void Method()
    {
        Console.WriteLine("Derived method");
    }

    public static void Main()
    {
        Derived derivedObj = new Derived();
        Base baseObj = derivedObj; // Полиморфизм
        baseObj.Method(); // Derived method

        List<Derived> dlist = new List<Derived>();

        Derived.PrintBases(dlist); // Ковариантный тип параментра метода
        IEnumerable<Base> bIEnum = dlist; // Ковариантность
    }
}

// Полиморфизм - вы можете присвоить объект типа Derived переменной типа Base
Derived derived = new Derived();
Base base = derived;

// Ковариантность - вы можете присвоить объект типа IEnumerable<Derived> переменной типа IEnumerable<Base>.
IEnumerable<Derived> d = new List<Derived>();
IEnumerable<Base> b = d;