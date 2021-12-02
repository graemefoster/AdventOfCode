using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    static class EnumerableEx
    {
        public static TR Scan<T, TR>(this IEnumerable<T> items, TR initial, Func<TR, T, TR> next)
        {
            var current = initial;
            foreach (var item in items)
            {
                current = next(current, item);
            }

            return current;
        }
    }
}