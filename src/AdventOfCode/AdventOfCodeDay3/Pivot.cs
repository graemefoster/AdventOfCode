
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeDay3
{
    static class EnumerableEx
    {
        public static IEnumerable<T[]> Pivot<T>(this IEnumerable<T[]> items)
        {
            var columns = items.First().Length;
            return Enumerable.Range(0, columns)
                .Select(idx => items.Select(item => item[idx]).ToArray());
        }

        public static T Dump<T>(this T t)
        {
            if (t is IEnumerable)
            {
                foreach (var x in (IEnumerable)t)
                {
                    Console.WriteLine(x);
                }
            }
            Console.WriteLine(t.ToString());
            return t;
        }
    }
}