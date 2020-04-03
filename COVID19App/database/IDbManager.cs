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
        /// Insert a country on the local database
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <param name="code">Unique Code Id assigned to this country</param>
        /// <param name="alpha">2 letter alphanumeric code of the country</param>
        /// <param name="regionId">Id of the continent</param>
        /// <returns></returns>
        bool InsertCountry(string name, ushort code, string alpha, byte regionId);

        /// <summary>
        /// Insert a region on the local database
        /// </summary>
        /// <param name="regionId">Id of the continent</param>
        /// <param name="regionName">Name of the continent</param>
        /// <returns></returns>
        bool InsertRegion(byte regionId, string regionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateDate"></param>
        /// <param name="confirmed">Number of confirmed cases</param>
        /// <param name="deaths">Number of deaths</param>
        /// <param name="recovered">Number of recovered cases</param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool InsertDayInfo(string updateDate, int confirmed, int deaths, int recovered, int code);
    }
}