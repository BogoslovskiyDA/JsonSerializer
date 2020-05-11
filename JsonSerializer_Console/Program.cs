﻿using System;
using System.Collections.Generic;
using JsonSerializer;

namespace JsonSerializer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person person1 = new Person(4, "Jonatan");
            //person1.Children.Add(new Person(5, "Joseph"));
            Person person = new Person(1, "Jack");
            //person.Children.Add(person1);
            person.Children.Add(new Person(2, "Jill"));
            //person.Children.Add(new Person(3, "Josuke"));
            
            
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
