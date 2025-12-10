using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class EnumerableExtensions
    {
        public static List<T> ToList<T>(this IEnumerable<T> self, List<T> list, bool append = false)
        {
            if (!append)
            {
                list.Clear();
            }

            list.AddRange(self);

            return list;
        }

        public static bool TryGetFirst<T>(this IEnumerable<T> self, Predicate<T> predicate, out T result)
        {
            foreach (var item in self)
            {
                if (predicate != null && !predicate.Invoke(item))
                {
                    continue;
                }

                result = item;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryGetFirst<T>(this IEnumerable<T> self, out T result)
        {
            return self.TryGetFirst(null, out result);
        }
    }
}