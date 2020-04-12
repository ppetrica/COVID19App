using System.Collections.Generic;


namespace core
{
    public interface IDataProvider<out T>
    {
        IReadOnlyList<T> GetCountryData();
    }
}
