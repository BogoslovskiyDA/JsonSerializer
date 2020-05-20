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
        private static void Serialize(object value,StringBuilder sb)
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
                    if (prop.PropertyType.IsArray)
                    {
                        Serialize_Array(prop, value, ref sb);
                        continue;
                    }
                    if (prop.PropertyType.IsGenericType)
                    {
                        Serialize_list(prop, value,ref sb);
                        continue;
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
        private static StringBuilder Serialize_list(PropertyInfo prop, object value, ref StringBuilder sb)
        {
            var array = prop.GetValue(value) as IEnumerable;
            if (((IList)array).Count == 0)
                return sb;
            sb.Append(' ', probels);
            sb.AppendLine($" \"{prop.Name.ToLower()}\" : [");
            foreach (var item in array)
            {
                if (item != null)
                {
                    if (item.GetType().Name == "String" || item.GetType().IsValueType)
                    {
                        sb.Append(' ', probels + 4);
                        sb.AppendLine($"{item},");
                    }
                    else
                        Serialize(item, sb);
                }
                else
                {
                    sb.Append(' ', probels + 4);
                    sb.AppendLine($"null,");
                }
            }
            sb.Append(' ', probels + 2);
            sb.AppendLine("]");
            return sb;
        }
        private static StringBuilder Serialize_Array(PropertyInfo prop, object value, ref StringBuilder sb)
        {
            var array = prop.GetValue(value) as Array;
            if (array.Length == 0)
                return sb;
            sb.Append(' ', probels);
            sb.AppendLine($" \"{prop.Name.ToLower()}\" : [");
            foreach (var item in array)
            {
                if (item != null)
                {
                    if (item.GetType().Name == "String" || item.GetType().IsValueType)
                    {
                        sb.Append(' ', probels + 4);
                        sb.AppendLine($"{item},");
                    }
                    else
                        Serialize(item, sb);
                }
                else
                {
                    sb.Append(' ', probels + 4);
                    sb.AppendLine("null,");
                }
            }
            sb.Append(' ', probels + 2);
            sb.AppendLine("]");
            return sb;
        }
    }
}
