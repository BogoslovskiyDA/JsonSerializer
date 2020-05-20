using System;
using JsonSerializer;
using JsonSerializer_Console.classes;

namespace JsonSerializer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region person_array
            //Person_Array person_Array = new Person_Array(1, "Jack");
            //person_Array.AppendCapacity();
            //person_Array.Children[0] = new Person_Array(2, "Jill");
            //JsonGenerator.GenerateJson(person_Array);
            #endregion

            #region sportsman
            //Sportsman sportsman = new Sportsman("Усейн Болт", 33);
            //sportsman.Medals[2] = "gold";
            //JsonGenerator.GenerateJson(sportsman);
            #endregion

            #region writer
            //Writer writer = new Writer("Харуки Мураками", 71);
            //writer.Book.Add("1Q84");
            //writer.Book.Add(null);
            //writer.Book.Add("Норвежский лес");
            //JsonGenerator.GenerateJson(writer);
            #endregion

            #region person
            Person person1 = new Person(4, "Jonatan");
            person1.Children.Add(new Person(5, "Joseph"));
            Person person = new Person(1, "Jack");
            person.Children.Add(person1);
            person.Children.Add(new Person(2, "Jill"));
            person.Children.Add(new Person(3, "Josuke"));
            JsonGenerator.GenerateJson(person);
            #endregion

            Console.ReadKey();
        }
    }
}
