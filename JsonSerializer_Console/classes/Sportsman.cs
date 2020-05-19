using System;
using System.Collections.Generic;
using System.Text;
using JsonSerializer;

namespace JsonSerializer_Console.classes
{
    [JsonObject]
    class Sportsman
    {
        public Sportsman(string name,int age)
        {
            Name = name;
            Age = age;
            Medals = new string[3];
        }
        [JsonAttribute]
        public string Name { get; set; }
        [JsonAttribute]
        public int Age { get; set; }
        [JsonAttribute]
        public string[] Medals { get; set; }
    }
}
