using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializer
{
    public class JsonGenerator
    {
        private static int k = -1;
        public static void GenerateJson(object value)
        {
            k++;
            var objectType = value.GetType();
            var properties = objectType.GetProperties();
            for (int i = 0; i < k ; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("{");
            foreach (var prop in properties)
            {
                
                var attrs = prop.GetCustomAttributes(false);

                if (attrs.Any(a => a.GetType() == typeof(JsonAttributeAttribute)))
                {
                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine($" \"{prop.Name.ToLower()}\" : " + prop.GetValue(value));
                }
                if (attrs.Any(a => a.GetType() == typeof(JsonListAttribute)))
                {
                    
                    var array = prop.GetValue(value) as IEnumerable;
                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine($" \"{prop.Name.ToLower()}\" : [");
                    foreach (var item in array)
                    {
                        GenerateJson(item);                     
                    }
                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(" ]");
                }
            }
            for (int i = 0; i < k; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("}");
            k--;
        }
    }
}





//var pi = item.GetType().GetProperties();
                        //foreach (var it in pi)
                        //{
                        //    Console.WriteLine(it.PropertyType + " " + it.Name + " " + it.GetValue(item));
                        //}