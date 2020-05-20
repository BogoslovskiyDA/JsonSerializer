using System;
using System.Collections.Generic;
using System.Text;
using JsonSerializer;

namespace JsonSerializer_Console.classes
{
    [JsonObject]
    class Person_Array
    {
        public Person_Array(int id, string name)
        {
            Id = id;
            Name = name;
            Children = new Person_Array[0];
        }
        [JsonAttribute]
        public int Id { get; set; }
        [JsonAttribute]
        public string Name { get; set; }
        [JsonAttribute]
        public Person_Array[] Children { get; set; }
        public void AppendCapacity()
        {
            var tempitems = Children;
            Children = new Person_Array[Children.Length + 1];
            Array.Copy(tempitems, Children, tempitems.Length);
        }
    }
}
