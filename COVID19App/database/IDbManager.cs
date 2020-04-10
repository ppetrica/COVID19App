using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public interface IDbManager
    {
        /// <summary>
        /// Set the connection with the specified database
        /// </summary>
        /// <param name="databasePath">Path to the local .db database</param>
        void SetDatabaseConnection(string databasePath);

        /// <summary>
        /// Clear all data from specified table
        /// </summary>
        /// <param name="tableName">Table name to be cleared</param>
        bool ClearTable(string tableName);

        /// <summary>
        /// Insert a country on the local database
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <param name="code">Unique Code Id assigned to this country</param>
        /// <param name="alpha">2 letter alphanumeric code of the country</param>
        /// <param name="regionId">Id of the continent</param>
        /// <returns>Return true in case of success insertion or false otherwise</returns>
        bool InsertCountry(string name, ushort code, string alpha, byte regionId);

        /// <summary>
        /// Insert a region on the local database
        /// </summary>
        /// <param name="regionId">Id of the continent</param>
        /// <param name="regionName">Name of the continent</param>
        /// <returns>Return true in case of success insertion or false otherwise</returns>
        bool InsertRegion(byte regionId, string regionName);

        /// <summary>
        /// Inserting a country item in the country Table.
        /// </summary>
        /// <param name="updateDate"></param>
        /// <param name="confirmed">Number of confirmed cases</param>
        /// <param name="deaths">Number of deaths</param>
        /// <param name="recovered">Number of recovered cases</param>
        /// <param name="code"></param>
        /// <returns>Return true in case of success insertion or false otherwise</returns>
        bool InsertDayInfo(string updateDate, int confirmed, int deaths, int recovered, int code);

        /// <summary>
        /// Get Region Name by his id.
        /// </summary>
        /// <param name="regionId">The id of the region</param>
        /// <returns>Return Null if not found or region's name</returns>
        string GetRegionNameById(int regionId);

        /// <summary>
        /// Get the country Name by id
        /// </summary>
        /// <param name="countryName">The name of the country</param>
        /// <returns>The id of the country which has the specified name</returns>
        int GetCountryIdByName(string countryName);

        /// <summary>
        /// Get country info by country's code
        /// </summary>
        /// <param name="countryId">Id of the Country</param>
        /// <returns>Return a tuple containing country name, country alphabetic code and region id </returns>
        Tuple<string, string, int> GetCountryInfoById(int countryId);

        /// <summary>
        /// Getting the information of COVID-19
        /// </summary>
        /// <param name="countryId">Id of the Country</param>
        /// <returns>Return a list of tuples containing the date, numbers of confirmed cases, number of deaths and number of recovered cases</returns>
        List<Tuple<string, int, int, int>> GetCovidInfoByCountryId(int countryId);

        /// <returns>A list of countries id which holds data in the dayinfo table</returns>
        List<int> GetCountriesId();

    }
}