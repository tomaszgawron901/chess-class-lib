using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Add element and return new IEumerable
        /// </summary>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T element)
        {
            var enumerableCount = enumerable.Count();
            var newArray = new T[enumerableCount + 1];
            using (var enumerator = enumerable.GetEnumerator())
            {

                for (int i = 0; i < enumerableCount; i++)
                {
                    enumerator.MoveNext();
                    newArray[i] = enumerator.Current;
                }
            }

            newArray[enumerableCount] = element;
            return newArray;
        }

        /// <summary>
        /// Swap first element which equals 'find' with 'swap' and return new IEnumerable 
        /// </summary>
        public static IEnumerable<T> SwapFirst<T>(this IEnumerable<T> enumerable, T find, T swap) where T : class
        {
            
            var newArray = enumerable.ToArray();
            for(int i = 0; i < newArray.Length; i++)
            {
                if(newArray[i].Equals(find))
                {
                    newArray[i] = swap;
                    return newArray;
                }
            }
            return newArray;
        }
    }
}
