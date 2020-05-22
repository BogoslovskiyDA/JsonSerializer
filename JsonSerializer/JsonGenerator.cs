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
            Serialize(value,ref sb);
            using (var sw = new StreamWriter(filename + ".json"))
            {
                sw.Write(sb);
            }
            Console.WriteLine(sb);
            return sb;
        }
        private void Serialize(object value,ref StringBuilder sb)
        {
            P += Probels;
            var objectType = value.GetType();
            var attr = objectType.GetCustomAttributes(false);
            var properties = objectType.GetProperties();
            sb.Append(' ', P);
            sb.AppendLine("{");
            int i = properties.Length;
            foreach (var prop in properties)
            {
                i--; 
                var attrs = prop.GetCustomAttributes(false);
                if (attrs.Any(a => a.GetType() == typeof(JsonAttributeAttribute)))
                {           
                    if (prop.PropertyType.IsArray)
                    {
                        Serialize_Array(prop, value, ref sb);
                        AddComma(ref sb, i);
                        continue;
                    }
                    if (prop.PropertyType.Name.Equals(typeof(List<>).Name))
                    {
                        Serialize_List(prop, value,ref sb);
                        AddComma(ref sb, i);
                        continue;
                    }
                    else
                    {
                        if (prop.GetValue(value) == null)
                        {
                            sb.Append(' ', P);
                            sb.Append($" \"{prop.Name.ToLower()}\" : null");
                            AddComma(ref sb, i);
                            continue;
                        }
                        if (prop.PropertyType.Name.Equals(typeof(string).Name))
                        {
                            sb.Append(' ', P);
                            sb.Append($" \"{prop.Name.ToLower()}\" : \"{prop.GetValue(value)}\"");
                        }
                        else
                        {
                            sb.Append(' ', P);
                            sb.Append($" \"{prop.Name.ToLower()}\" : {prop.GetValue(value)}");
                        }
                        AddComma(ref sb, i);
                    }                  
                }
            }
            sb.Append(' ', P);
            sb.Append("}");
            P -= Probels;
        }
        private void Serialize_List(PropertyInfo prop, object value, ref StringBuilder sb)
        {
            var array = prop.GetValue(value) as IEnumerable;
            if (((IList)array).Count == 0)
            {
                sb.Append(' ', P);
                sb.Append($" \"{prop.Name.ToLower()}\" : []");
                return;
            }
            sb.Append(' ', P);
            sb.AppendLine($" \"{prop.Name.ToLower()}\" : [");
            P += Probels;
            int i = ((IList)array).Count;
            foreach (var item in array)
            {
                i--;
                Serialize_Item(item, ref sb);
                AddComma(ref sb, i);
            }
            sb.Append(' ', P);
            sb.Append("]");
            P -= Probels;
        }
        private void Serialize_Array(PropertyInfo prop, object value, ref StringBuilder sb)
        {
            var array = prop.GetValue(value) as Array;
            if (array.Length == 0)
            {
                sb.Append(' ', P);
                sb.Append($" \"{prop.Name.ToLower()}\" : []");
                return;
            }
            sb.Append(' ', P);
            sb.AppendLine($" \"{prop.Name.ToLower()}\" : [");
            P += Probels;
            int i = array.Length;
            foreach (var item in array)
            {
                i--;
                Serialize_Item(item, ref sb);
                AddComma(ref sb, i);
            }
            sb.Append(' ', P);
            sb.Append("]");
            P -= Probels;
        }
        private void Serialize_Item(object item, ref StringBuilder sb)
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
                    Serialize(item, ref sb);
                    P += Probels;
                }
            }
            else
            {
                sb.Append(' ', P + Probels);
                sb.Append("null");
            }
        }
        private void AddComma(ref StringBuilder sb, int i)
        {
            if (i != 0)
                sb.Append(",\n");
            else
                sb.Append("\n");
        }
    }
}
