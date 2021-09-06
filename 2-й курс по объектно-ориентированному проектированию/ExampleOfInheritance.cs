using System;

namespace TestSpace
{
    /**
    * Класс статей содержит ссылку на класс Автора
    */
    public class Article
    {
        public Author Author;
        public string Text;
        
        public virtual void getMaxLength()
        {
            Console.WriteLine("HAS NOT");
        }
    }

    public class Author
    {
        public string Name;
        public string Surname;
    }

    /**
    * Класс новостей наследуется от класса Статьи
    */
    public class News : Article
    {
        public string DateCreation;
        public string MaxLength;
        
        public override  void getMaxLength()
        {
            Console.WriteLine("1000 symbols");
        }
        
    }
    
    /**
    * Класс новостей наследуется от класса Статьи
    */
    public class ShortMessage : Article
    {
        public string MaxLength;
        
        public override  void getMaxLength()
        {
            Console.WriteLine("50 symbols");
        }
        
    }
    

    
}