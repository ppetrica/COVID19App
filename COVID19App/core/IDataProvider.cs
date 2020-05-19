using System.Collections.Generic;


namespace core
{
    /// <summary>
    /// Interface implemented by any data provider in program
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<out T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Statistics about COVID-19 organized by country as a read only list of core.CountryInfo.</returns>
        IReadOnlyList<T> GetCountryData();
    }
}
