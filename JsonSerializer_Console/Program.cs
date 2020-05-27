using System;
using System.Text;
using JsonSerializer;
using JsonSerializer_Console.classes;

namespace JsonSerializer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonGenerator jsonGenerator = new JsonGenerator();

            #region person_array
            //Person_Array person_Array = new Person_Array(1, "Jack");
            //person_Array.AppendCapacity();
            //person_Array.Children[0] = null;
            //person_Array.AppendCapacity();
            //person_Array.Children[1] = new Person_Array(2, null);
            //jsonGenerator.GenerateJson(person_Array, person_Array.GetType().Name);
            #endregion

            #region sportsman
            //Sportsman sportsman = new Sportsman("Usain Bolt", 33);
            //sportsman.Medals[2] = "gold";
            //jsonGenerator.GenerateJson(sportsman, sportsman.GetType().Name);
            #endregion

            #region writer
            //Writer writer = new Writer("Haruki Murakami", 71);
            //writer.Book.Add("1Q84");
            //writer.Book.Add(null);
            //writer.Book.Add("Norwegian Wood");
            //jsonGenerator.GenerateJson(writer , writer.GetType().Name);
            #endregion

            #region person
            ////Person person1 = new Person(4, "Jonatan");
            ////person1.Children.Add(new Person(5, "Joseph"));
            //Person person = new Person(1, "Jack");
            ////person.Children.Add(person1);
            //person.Children.Add(new Person(2, "Jill"));
            ////person.Children.Add(new Person(3, "Josuke"));
            //jsonGenerator.GenerateJson(person, person.GetType().Name);
            #endregion

            Console.ReadKey();
        }
    }
}
