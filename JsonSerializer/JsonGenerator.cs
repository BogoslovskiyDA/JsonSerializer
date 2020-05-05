using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializer
{
    public class JsonGenerator
    {
        public static void GenerateJson(object value)
        {
            var objectType = value.GetType();
            var properties = objectType.GetProperties();
            var result = "";
            foreach (var prop in properties)
            {
                var attrs = prop.GetCustomAttributes(false);
                
                if (attrs.Any(a => a.GetType() == typeof(JsonAttributeAttribute)))
                {
                    Console.WriteLine(prop.PropertyType + " " + prop.Name + " " + prop.GetValue(value));
                }
                if (attrs.Any(a => a.GetType() == typeof(JsonListAttribute)))
                {

                    Console.WriteLine(prop.ToString());
                }
            }
        }
    }
}
