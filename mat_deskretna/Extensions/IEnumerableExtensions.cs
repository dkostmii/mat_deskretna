using System;
using System.Collections.Generic;
using System.Text;

namespace mat_deskretna.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> ApplyIf<T>(this IEnumerable<T> seq, bool condition, Func<IEnumerable<T>, IEnumerable<T>> fn)
        {
            if (condition)
                return fn(seq);

            return seq;
        }
    }
}
