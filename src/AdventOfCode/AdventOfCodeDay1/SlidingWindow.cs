using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    static class EnumerableEx
    {
        public static IEnumerable<TR> SlidingWindow<T, TR>(this IEnumerable<T> items, int windowSize, Func<T[], TR> func)
        {
            var group = new T[windowSize];
            foreach (var item in items)
            {
                Array.Copy(group, 1, group, 0, windowSize - 1);
                group[windowSize - 1] = item;
                yield return func(group);
            }
        }
    }
}