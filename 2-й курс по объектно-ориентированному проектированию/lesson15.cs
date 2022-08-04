using System;
using System.Collections.Generic;

public class Human
{
    public string Name { get;set; }
    public int Age { get;set; }
    public virtual string RepresentYourself()
    {
        return $"My name is {this.Name}. I'am {this.Age} years old";
    }
}

public class Man: Human
{
    public const string Sex = "man";

    public override string RepresentYourself()
    {
        return $"{base.RepresentYourself()} and I'm a {Sex}";
    }
}

public class Woman: Human
{
    public const string Sex = "woman";

    public override string RepresentYourself()
    {
        return $"{base.RepresentYourself()} and I'm a {Sex}";
    }
}

class MainClassTask15
{
    public static void Main (string[] args) 
    {
        var humansList = new List<Human>();
        humansList.Add(new Man {Name="Oleg", Age=20});
        humansList.Add(new Woman {Name="Nastya", Age=23});

        foreach (var human in humansList)
        {
            Console.WriteLine(human.RepresentYourself());
        }

        System.Console.Read();
    }
}
