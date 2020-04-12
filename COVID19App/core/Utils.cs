using System;
using System.Collections.Generic;
using System.Linq;


namespace core
{
    public class Utils
    {
        /// <summary>
        /// Finds the maximum element in an enumerable / collection.
        /// </summary>
        /// <typeparam name="T">Type of element inside enumerable.</typeparam>
        /// <param name="coll">Collection to search in.</param>
        /// <param name="compareFunc">
        /// Function which returns if the first element is to
        /// be considered "greater" than the second one
        /// </param>
        /// <returns>The maximum element in the collection.</returns>
        public static T MaxElement<T>(IEnumerable<T> coll, Func<T, T, bool> compareFunc)
        {
            if (!coll.Any())
                throw new ArgumentException();

            IEnumerator<T> iter = coll.GetEnumerator();
            T max = iter.Current;

            while (iter.MoveNext())
            {
                T current = iter.Current;

                if (compareFunc(current, max))
                {
                    max = current;
                }
            }

            return max;
        }

        /// <summary>
        /// Function used to search a collection for an element.
        /// </summary>
        /// <typeparam name="T">Type of item inside collection.</typeparam>
        /// <param name="coll">Collection to be searched.</param>
        /// <param name="pred">
        /// A function which should return true if the element satisfies the search criteria.
        /// </param>
        /// <returns>The element searched or null if it was not found.</returns>
        public static T? Find<T>(IEnumerable<T> coll, Predicate<T> pred) where T : struct
        {
            if (!coll.Any()) return null;
            IEnumerator<T> iter = coll.GetEnumerator();

            while (iter.MoveNext())
            {
                if (pred(iter.Current))
                    return iter.Current;
            }

            return null;
        }
    }
}