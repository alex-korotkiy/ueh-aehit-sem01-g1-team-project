using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure.Utils
{
    public static class EnumerableExtensions
    {
        public static List<List<T>> SplitOnStripes<T>(this IEnumerable<T> enumerable, int nparts)
        {
            var result = new List<List<T>>();
            for (var i = 0; i<nparts; i++)
            {
                result.Add(new List<T>());
            }

            int counter = 0;
            foreach (var item in enumerable)
            {
                result[counter % nparts].Add(item);
                counter++;
            }

            return result;
        }
    }
}
