
using System;
using System.Collections.Generic;

namespace core
{
    public class Utils
    {
        public static T MaxElement<T>(IEnumerable<T> coll, Func<T, T, bool> compareFunc)
        {
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

        public static Nullable<T> Find<T>(IEnumerable<T> coll, Predicate<T> pred) where T : struct
        {
            IEnumerator<T> iter = coll.GetEnumerator();
            do
            {
                if (pred(iter.Current))
                    return iter.Current;
            } while (iter.MoveNext());

            return null;
        }
    }
}