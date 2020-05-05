using System;
using System.Collections.Generic;
using JsonSerializer;

namespace JsonSerializer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person(1, "Jack");
            person.Children.Add(new Person(2, "Jill"));
            JsonGenerator.GenerateJson(person);

            //var list = new List<int> { 1, 2, 3 };
            //Console.WriteLine(list);
            //var arr = list.ToArray();
            //foreach (var item in arr)
            //{
            //    Console.WriteLine(item);
            //}

            Console.ReadKey();
        }
    }
}
