using System;

namespace lesson13
{
    // 1. метод публичен в родительском классе А и публичен в его потомке B в С# возможно
    class Task_1
    {
        public string Name { get; set; }

        public void Display()
        {
            System.Console.WriteLine(Name);
        }
    }

    class NewTas_1k : Task_1
    {
        public string Company { get; set; }
    }
    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         var p = new Task_1 {Name = "Bill"};
    //         p.Display(); // Bill
    //         var emp = new NewTask_1 {Name = "Tom", Company = "Microsoft"};
    //         emp.Display(); // Tom
    //         System.Console.Read();
    //     }
    // }
    
    // 2. метод публичен в родительском классе А и скрыт в его потомке B. Возможно через переопределение new.
    class Task_2
    {
        public string Name { get; set; }

        public void Display()
        {
            System.Console.WriteLine(Name);
        }
    }

    class NewTask_2 : Task_2
    {
        public string Company { get; set; }
        
        protected new void Display()
        {
            System.Console.WriteLine(Name);
        }
    }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         var p = new Task_2 {Name = "Bill"};
    //         p.Display(); // Bill
    //         var emp = new NewTask_2 {Name = "Tom", Company = "Microsoft"};
    //         emp.Display(); // Tom
    //         System.Console.Read();
    //     }
    // }
    //
    // 3. метод скрыт в родительском классе А и публичен в его потомке B. Только через переобределение new
    class Task_3
    {
        public string Name { get; set; }

        protected void Display()
        {
            System.Console.WriteLine(Name);
        }
    }

    class NewTask_3 : Task_3
    {
        public string Company { get; set; }
        
        public void Display()
        {
            System.Console.WriteLine(Name);
        }
    }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         var p = new Task_3 {Name = "Bill"};
    //         p.Display(); // Bill
    //         var emp = new NewTask_3 {Name = "Tom", Company = "Microsoft"};
    //         emp.Display(); // Tom
    //         System.Console.Read();
    //     }
    // }
    
    // 4. метод скрыт в родительском классе А и скрыт в его потомке B. Возможно
    class Task_4
    {
        public string Name { get; set; }

        protected void Display()
        {
            System.Console.WriteLine(Name);
        }
    }

    class NewTask_4 : Task_4
    {
        public string Company { get; set; }
        
        public void Show()
        {
            Display();
        }
    }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         var emp = new NewTask_4 {Name = "Tom", Company = "Microsoft"};
    //         emp.Show(); // Tom
    //         System.Console.Read();
    //     }
    // }
}