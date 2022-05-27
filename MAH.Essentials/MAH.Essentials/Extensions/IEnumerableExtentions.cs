using System;
using System.Linq;
using System.Collections.Generic;

namespace MAH.Essentials
{
    public static class IEnumerableExtentions
    {
        public static void ForLoop<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source is null) { return; }

            for (int index = 0; index < source.Count(); index++)
            {
                action(source.ElementAt(index), index);
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source is null || source.Any() == false;
        }

        public static void RunBatches<T>(this IEnumerable<T> source, int batchSize, Action<IEnumerable<T>, int> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (source.IsNullOrEmpty()) { return; }

            var page = 0;

            var currentBatch = source.Skip(page * batchSize).Take(batchSize);

            while (currentBatch.Any())
            {
                action(currentBatch, page);
                page++;
                currentBatch = source.Skip(page * batchSize).Take(batchSize);
            }
        }
    }
}