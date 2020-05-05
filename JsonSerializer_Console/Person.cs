using System;
using System.Collections.Generic;
using System.Text;
using JsonSerializer;

namespace JsonSerializer_Console
{
    [JsonObject]
    class Person
    {       
        public Person(int id, string name)
        {
            Id = id;
            Name = name;
            Children = new List<Person>();
        }
        [JsonAttribute]
        public int Id { get; set; }
        [JsonAttribute]
        public string Name { get; set; }
        [JsonList]
        public List<Person> Children { get; set; }
    }
}
