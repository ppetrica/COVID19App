using System.Collections.Generic;


namespace core
{
    public interface DataProvider<T>
    {
        IReadOnlyList<T> GetCountryData();
    }
}
