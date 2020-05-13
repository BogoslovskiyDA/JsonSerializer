using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            probels = -2;
            var sb = new StringBuilder();
            Serialize(value, sb);
            using (var sw = new StreamWriter("json.txt"))
            {
                sw.Write(sb);
            }
            Console.WriteLine(sb);
        }

        public static void Serialize(object value,StringBuilder sb)
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
            sb.Append(' ', probels);
            sb.AppendLine("{");
            foreach (var prop in properties)
            {              
                var attrs = prop.GetCustomAttributes(false);
                if (attrs.Any(a => a.GetType() == typeof(JsonAttributeAttribute)))
                {
                    if (prop.PropertyType.IsGenericType)
                    {
                        var array = prop.GetValue(value) as IEnumerable;
                        if (((IList)array).Count == 0)
                            break;
                        sb.Append(' ', probels);
                        sb.AppendLine($" \"{prop.Name.ToLower()}\" : [");
                        foreach (var item in array)
                        {
                            Serialize(item,sb);
                        }
                        sb.Append(' ', probels + 1);
                        sb.AppendLine("]");
                    }
                    else
                    {
                        sb.Append(' ', probels);
                        sb.AppendLine($" \"{prop.Name.ToLower()}\" : " + prop.GetValue(value));
                    }
                }
            }
            sb.Append(' ', probels);
            sb.AppendLine("}");
            probels -= 2;
        }
    }
}
