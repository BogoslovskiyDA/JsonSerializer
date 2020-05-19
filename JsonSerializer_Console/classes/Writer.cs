using System;
using System.Collections.Generic;
using System.Text;
using JsonSerializer;

namespace JsonSerializer_Console.classes
{
    [JsonObject]
    class Writer
    {
        public Writer(string name,int age)
        {
            Name = name;
            Age = age;
            Book = new List<string>();
        }
        [JsonAttribute]
        public string Name { get; set; }
        [JsonAttribute]
        public int Age { get; set; }
        [JsonAttribute]
        public List<string> Book { get; set; }
    }
}
