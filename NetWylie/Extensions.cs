using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace org.rigpa.wylie
{
    public class HashMap<K,V> : Dictionary<K,V>  {  }

    public class RuntimeException : Exception { 
        public RuntimeException(string message) : base(message) { }
    }

    public class ArrayList<T> : List<T> { }

    public static class Extensions
    {
        public static void put<K,V>(this HashMap<K,V> map, K key, V value)
        {
            map.Add(key, value);
        }

        public static void add<T>(this HashSet<T> set, T value)
        {
            set.Add(value);
        }

        public static V get<K,V>(this HashMap<K,V> map, K key)
        {
            if (map.ContainsKey(key))
                return map[key];
            else
                return default(V);
        }

        public static bool contains<T>(this HashSet<T> set, T value)
        {
            return set.Contains(value);
        }

        public static bool containsKey<K,V>(this HashMap<K,V> map, K key)
        {
            return map.ContainsKey(key);
        }

        public static void add<T>(this List<T> list, T value)
        {
            list.Add(value);
        }

        public static string replaceFirst(this string value, string pattern, string replace)
        {
            return Regex.Replace(value, pattern, replace);
        }

        public static StringBuilder append(this StringBuilder sb, object value)
        {
            return sb.Append(value.ToString());
        }

        public static string toString(this object o)
        {
            return o.ToString();
        }

        public static bool equals(this string orig, string compare)
        {
            return orig.Equals(compare);
        }

        public static bool startsWith(this string value, string search)
        {
            return value.StartsWith(search);
        }

        public static string substring(this string value, int startIndex, int endindex = -1)
        {
            return (endindex == -1) ? value.Substring(startIndex) : value.Substring(startIndex, (endindex - startIndex));
        }

        public static bool isEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static int length(this string value)
        {
            return value.Length;
        }

        public static char charAt(this string value, int index)
        {
            return value[index];
        }

        public static int size<T>(this IEnumerable<T> coll)
        {
            return coll.Count();
        }

        public static string replace(this string value, string search, string replace)
        {
            return value.Replace(search, replace);
        }

        public static string replace(this string value, char search, char replace)
        {
            return value.Replace(search, replace);
        }

        public static int length(this StringBuilder sb)
        {
            return sb.Length;
        }

        public static void setLength(this StringBuilder sb, int length)
        {
            sb.Length = length;
        }

        public static void addAll<T>(this List<T> list, IEnumerable<T> adds)
        {
            list.AddRange(adds);
        }

        public static T get<T>(this ArrayList<T> list, int index)
        {
            return list[index];
        }

        public static bool isEmpty<T>(this ArrayList<T> list)
        {
            return list.Count == 0;
        }

        public static void addFirst<T>(this LinkedList<T> list, T value)
        {
            list.AddFirst(value);
        }

        public static void removeFirst<T>(this LinkedList<T> list)
        {
            list.RemoveFirst();
        }

        public static void add<T>(this LinkedList<T> list, T value)
        {
            list.AddLast(value);
        }

        public static bool isEmpty<T>(this LinkedList<T> list)
        {
            return list.Count == 0;
        }

        public static T get<T>(this LinkedList<T> list, int index)
        {
            return list.ToArray()[index];
        }
    }
}
