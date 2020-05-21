using System;
using System.Collections.Generic;
using JsonSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjerctJson
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
        [JsonAttribute]
        public List<Person> Children { get; set; }
    }
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
    [JsonObject]
    class Sportsman
    {
        public Sportsman(string name, int age)
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
    [JsonObject]
    class Writer
    {
        public Writer(string name, int age)
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
    public class Student
    {
        public Student(string name,int age)
        {
            Name = name;
            Age = age;
        }
        [JsonAttribute]
        public string Name { get; set; }
        [JsonAttribute]
        public int Age { get; set; }
    }
    [TestClass]
    public class UnitTestJson
    {
        [TestMethod]
        public void TestObjectWithoutAttribute()
        {
            Student student = new Student("Jack", 20);
            Assert.AreEqual("Object not marked with attribute", JsonGenerator.GenerateJson(student).ToString());
        }
        [TestMethod]
        public void TestArrayValue()
        {
            Sportsman sportsman = new Sportsman("Usain Bolt", 33);
            sportsman.Medals[2] = "gold";
            string[] lines = JsonGenerator.GenerateJson(sportsman).ToString().Split('\n');

            Assert.AreEqual("{", lines[0].Trim());
            Assert.AreEqual("\"name\" : \"Usain Bolt\",", lines[1].Trim());
            Assert.AreEqual("\"age\" : 33,", lines[2].Trim());
            Assert.AreEqual("\"medals\" : [", lines[3].Trim());
            Assert.AreEqual("null,", lines[4].Trim());
            Assert.AreEqual("null,", lines[5].Trim());
            Assert.AreEqual("\"gold\"", lines[6].Trim());
            Assert.AreEqual("]", lines[7].Trim());
            Assert.AreEqual("}", lines[8].Trim());
        }
        [TestMethod]
        public void TestArrayObject()
        {
            Person_Array person_Array = new Person_Array(1, "Jack");
            person_Array.AppendCapacity();
            person_Array.Children[0] = new Person_Array(2, "Jill");
            string[] lines = JsonGenerator.GenerateJson(person_Array).ToString().Split('\n');

            Assert.AreEqual("{", lines[0].Trim());
            Assert.AreEqual("\"id\" : 1,", lines[1].Trim());
            Assert.AreEqual("\"name\" : \"Jack\",", lines[2].Trim());
            Assert.AreEqual("\"children\" : [", lines[3].Trim());
            Assert.AreEqual("{", lines[4].Trim());
            Assert.AreEqual("\"id\" : 2,", lines[5].Trim());
            Assert.AreEqual("\"name\" : \"Jill\"", lines[6].Trim());
            Assert.AreEqual("}", lines[7].Trim());
            Assert.AreEqual("]", lines[8].Trim());
            Assert.AreEqual("}", lines[9].Trim());
        }
        [TestMethod]
        public void TestListValue()
        {
            Writer writer = new Writer("Haruki Murakami", 71);
            writer.Book.Add("1Q84");
            writer.Book.Add(null);
            writer.Book.Add("Norwegian Wood");
            string[] lines = JsonGenerator.GenerateJson(writer).ToString().Split('\n');

            Assert.AreEqual("{", lines[0].Trim());
            Assert.AreEqual("\"name\" : \"Haruki Murakami\",", lines[1].Trim());
            Assert.AreEqual("\"age\" : 71,", lines[2].Trim());
            Assert.AreEqual("\"book\" : [", lines[3].Trim());
            Assert.AreEqual("\"1Q84\",", lines[4].Trim());
            Assert.AreEqual("null,", lines[5].Trim());
            Assert.AreEqual("\"Norwegian Wood\"", lines[6].Trim());
            Assert.AreEqual("]", lines[7].Trim());
            Assert.AreEqual("}", lines[8].Trim());
        }
        [TestMethod]
        public void TestListObjects()
        {
            Person person = new Person(1, "Jack");
            person.Children.Add(new Person(2, "Jill"));
            JsonGenerator.GenerateJson(person);
            string[] lines = JsonGenerator.GenerateJson(person).ToString().Split('\n');

            Assert.AreEqual("{", lines[0].Trim());
            Assert.AreEqual("\"id\" : 1,",lines[1].Trim());
            Assert.AreEqual("\"name\" : \"Jack\",", lines[2].Trim());
            Assert.AreEqual("\"children\" : [", lines[3].Trim());
            Assert.AreEqual("{", lines[4].Trim());
            Assert.AreEqual("\"id\" : 2,", lines[5].Trim());
            Assert.AreEqual("\"name\" : \"Jill\"", lines[6].Trim());
            Assert.AreEqual("}", lines[7].Trim());
            Assert.AreEqual("]", lines[8].Trim());
            Assert.AreEqual("}", lines[9].Trim());
        }
        [TestMethod]
        public void TestSerializeNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { JsonGenerator.GenerateJson(null); });
        }
    }
}
