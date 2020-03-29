using System.Collections.Generic;


namespace core
{
    interface DataProvider<T>
    {
        IReadOnlyList<T> GetCountryData();
    }
}
