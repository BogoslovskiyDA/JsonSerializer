using System;
using JsonSerializer;

namespace JsonSerializer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person(4, "Jonatan");
            person1.Num.Add(4);
            person1.Num.Add(5);
            person1.Num.Add(6);
            //person1.Children.Add(new Person(5, "Joseph"));
            Person person = new Person(1, "Jack");
            person.Num.Add(1);
            person.Num.Add(2);
            person.Num.Add(3);
            person.Children.Add(person1);
            //person.Children.Add(new Person(2, "Jill"));
            //person.Children.Add(new Person(3, "Josuke"));                        
            JsonGenerator.GenerateJson(person);

            //int v = 1;
            //var objecttype = v.GetType();
            //var propinf = objecttype.GetProperties();

            Console.ReadKey();
        }
    }
}
