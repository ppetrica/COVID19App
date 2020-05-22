/**************************************************************************
 *                                                                        *
 *  File:        IDbManager.cs                                            *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: An interface for implementing Database functionality.    *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;


namespace Database
{
    /// <summary>
    /// Interface with sql methods specific for our Database.
    /// </summary>
    public interface IDbManager
    {
        /// <summary>
        /// Set the connection with the specified Database
        /// </summary>
        /// <param name="databasePath">Path to the local .db Database</param>
        void SetDatabaseConnection(string databasePath);

        /// <summary>
        /// Clear all data from specified table
        /// </summary>
        /// <param name="tableName">Table name to be cleared</param>
        void ClearTable(string tableName);

        /// <summary>
        /// Insert a country on the local Database
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <param name="code">Unique Code Id assigned to this country</param>
        /// <param name="alpha">2 letter alphanumeric code of the country</param>
        /// <param name="regionId">Id of the continent</param>
        /// <param name="population">Population of the country</param>
        /// <returns>Return true in case of success insertion or false otherwise</returns>
        void InsertCountry(string name, ushort code, string alpha, byte regionId, long population);

        /// <summary>
        /// Insert a region on the local Database
        /// </summary>
        /// <param name="regionId">Id of the continent</param>
        /// <param name="regionName">Name of the continent</param>
        /// <returns>Return true in case of success insertion or false otherwise</returns>
        void InsertRegion(byte regionId, string regionName);

        /// <summary>
        /// Inserting a country item in the country Table.
        /// </summary>
        /// <param name="updateDate"></param>
        /// <param name="confirmed">Number of confirmed cases</param>
        /// <param name="deaths">Number of deaths</param>
        /// <param name="recovered">Number of recovered cases</param>
        /// <param name="code"></param>
        /// <returns>Return true in case of success insertion or false otherwise</returns>
        void InsertDayInfo(string updateDate, int confirmed, int deaths, int recovered, int code);

        void InsertDayInfos(List<Tuple<string, int, int, int, int>> dayInfoList);

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
        /// <returns>Return a tuple containing country name, country alphabetic code, region id and population</returns>
        Tuple<string, string, int, long> GetCountryInfoById(int countryId);

        /// <summary>
        /// Getting the information of COVID-19
        /// </summary>
        /// <param name="countryId">Id of the Country</param>
        /// <returns>Return a list of tuples containing the date, numbers of confirmed cases, number of deaths and number of recovered cases</returns>
        List<Tuple<string, int, int, int>> GetCovidInfoByCountryId(int countryId);

        /// <returns>A list of countries id which holds data in the dayinfo table</returns>
        List<int> GetCountryCodes();

        /// <summary>
        /// Getting the name of the continent the country is located
        /// </summary>
        /// <returns>The name of region/continent</returns>
        string GetRegionNameByCountryId(int countryId);

        /// <summary>
        /// Getting the most current date of the data from the Database
        /// </summary>
        /// <returns>The day in format string "YYYY-MM-DD"</returns>
        string GetTheMostRecentDate();
    }
}
