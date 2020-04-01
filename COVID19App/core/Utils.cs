using System;
using System.Collections.Generic;
using System.Linq;


namespace core
{
    public class Utils
    {
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

        public static Nullable<T> Find<T>(IEnumerable<T> coll, Predicate<T> pred) where T : struct
        {
            if (coll.Any())
            {
                IEnumerator<T> iter = coll.GetEnumerator();

                while (iter.MoveNext())
                {
                    if (pred(iter.Current))
                        return iter.Current;
                }
            }

            return null;
        }
    }
}