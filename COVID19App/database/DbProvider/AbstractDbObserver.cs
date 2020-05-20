using System.Collections.Generic;
using core;


/// <summary>
/// This module is a middleware (proxy) beetween database and highlevel module view.
/// </summary>
namespace database.DbProvider
{
    /// <summary>
    /// Database observer.
    /// This class is used for observer design pattern.
    /// </summary>
    public abstract class AbstractDbObserver
    {
        /// <summary>
        /// Insert the list of countryInfo to the database, transferring raw data to IDbManager
        /// </summary>
        /// <param name="countryInfoList">List of Country Info to be inserted in the database</param>
        public abstract void InsertCountryData(IReadOnlyList<CountryInfo> countryInfoList);

        /// <returns>The most recent Date of the data from the database</returns>
        public abstract Date GetTheMostRecentDateOfData();
    }
}
