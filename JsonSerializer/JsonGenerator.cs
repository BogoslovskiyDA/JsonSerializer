using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsonSerializer
{
    public class InvalidJsonObject : Exception
    {
        public InvalidJsonObject() : base("Object not marked with attribute")
        { }
    }
    public class JsonGenerator
    {
        private const int Probels = 2;
        private int P = -Probels;
        public StringBuilder GenerateJson(object value , string filename)
        {
            if (value == null)
                throw new ArgumentNullException();
            var sb = new StringBuilder();
            if (!Attribute.IsDefined(value.GetType(), typeof(JsonObjectAttribute)))
            {
                throw new InvalidJsonObject();
            }
            Serialize(value, sb);
            using (var sw = new StreamWriter(filename + ".json"))
            {
                sw.Write(sb);
            }
            Console.WriteLine(sb);
            return sb;
        }
        private void Serialize(object value, StringBuilder sb)
        {
            P += Probels;
            var objectType = value.GetType();
            var properties = objectType.GetProperties();
            sb.Append(' ', P);
            sb.AppendLine("{");
            for (int i = 0; i < properties.Length; i++)
            {
                var attrs = properties[i].GetCustomAttributes(false);
                if (attrs.Any(a => a.GetType() == typeof(JsonAttributeAttribute)))
                {
                    if (properties[i].PropertyType.IsArray || properties[i].PropertyType.Name.Equals(typeof(List<>).Name))
                    {
                        Serialize_List_and_Array(properties[i], value,  sb);
                    }
                    else
                    {
                        Serialize_Property(value, properties[i],  sb);
                    }
                    if (i + 1 == properties.Length)
                        sb.Append("\n");
                    else
                        sb.Append(",\n");
                }
            }           
            sb.Append(' ', P);
            sb.Append("}");
            P -= Probels;
        }
        private void Serialize_List_and_Array(PropertyInfo prop, object value,  StringBuilder sb)
        {
            var array = prop.GetValue(value) as IList;

            if (array.Count == 0)
            {
                sb.Append(' ', P);
                sb.Append($" \"{prop.Name}\" : []");
                return;
            }
            sb.Append(' ', P);
            sb.AppendLine($" \"{prop.Name}\" : [");
            P += Probels;
            int i = array.Count;
            foreach (var item in array)
            {
                i--;
                Serialize_Item_From_List_or_Array(item,  sb);
                if (i != 0)
                    sb.Append(",\n");
                else
                    sb.Append("\n");
            }
            sb.Append(' ', P);
            sb.Append("]");
            P -= Probels;
        }
        private void Serialize_Item_From_List_or_Array(object item,  StringBuilder sb)
        {
            if (item != null)
            {
                if (item.GetType().Equals(typeof(string)))
                {
                    sb.Append(' ', P + Probels);
                    sb.Append($"\"{item}\"");
                    return;
                }
                if (item.GetType().IsValueType)
                {
                    sb.Append(' ', P + Probels);
                    sb.Append($"\"{item}\"");
                    return;
                }
                else
                {
                    P -= Probels; 
                    Serialize(item,  sb);
                    P += Probels;
                }
            }
            else
            {
                sb.Append(' ', P + Probels);
                sb.Append("null");
            }
        }
        private void Serialize_Property(object value, PropertyInfo property,  StringBuilder sb)
        {
            if (property.GetValue(value) == null)
            {
                sb.Append(' ', P);
                sb.Append($" \"{property.Name}\" : null");
                return;
            }
            if (property.PropertyType.Name.Equals(typeof(string).Name))
            {
                sb.Append(' ', P);
                sb.Append($" \"{property.Name}\" : \"{property.GetValue(value)}\"");
                return;
            }
            else
            {
                sb.Append(' ', P);
                sb.Append($" \"{property.Name}\" : {property.GetValue(value)}");
                return;
            }
        }
    }
}
