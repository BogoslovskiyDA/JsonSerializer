using System;
using JsonSerializer;
using JsonSerializer_Console.classes;

namespace JsonSerializer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sportsman sportsman = new Sportsman("Усейн Болт", 33);
            //sportsman.Medals[2] = "gold";
            //JsonGenerator.GenerateJson(sportsman);

            //Writer writer = new Writer("Харуки Мураками", 71);
            //writer.Book.Add("1Q84");
            //writer.Book.Add(null);
            //writer.Book.Add("Норвежский лес");
            //JsonGenerator.GenerateJson(writer);

            Person person1 = new Person(4, "Jonatan");
            person1.Children.Add(new Person(5, "Joseph"));
            Person person = new Person(1, "Jack");
            person.Children.Add(person1);
            person.Children.Add(new Person(2, "Jill"));
            person.Children.Add(new Person(3, "Josuke"));
            JsonGenerator.GenerateJson(person);

            Console.ReadKey();
        }
    }
}
