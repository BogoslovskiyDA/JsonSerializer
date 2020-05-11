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
        private static int probels = -2;
        public static void GenerateJson(object value)
        {
            probels += 2;
            var objectType = value.GetType();
            var attr = objectType.GetCustomAttributes(false);
            if (!Attribute.IsDefined(value.GetType(), typeof(JsonObjectAttribute)))
            {
                Console.WriteLine("Аттрибут не найден");
                return;
            }
            var properties = objectType.GetProperties();
            for (int i = 0; i < probels ; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("{");
            foreach (var prop in properties)
            {
                
                var attrs = prop.GetCustomAttributes(false);

                if (attrs.Any(a => a.GetType() == typeof(JsonAttributeAttribute)))
                {
                    for (int i = 0; i < probels; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine($" \"{prop.Name.ToLower()}\" : " + prop.GetValue(value));
                }
                if (attrs.Any(a => a.GetType() == typeof(JsonListAttribute)))
                {
                    
                    var array = prop.GetValue(value) as IEnumerable;
                    if (((IList)array).Count == 0)
                        break;
                    for (int i = 0; i < probels; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine($" \"{prop.Name.ToLower()}\" : [");
                    foreach (var item in array)
                    {
                        GenerateJson(item);                     
                    }
                    for (int i = 0; i < probels; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(" ]");
                }
            }
            for (int i = 0; i < probels; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("}");
            probels -= 2;
        }
    }
}
