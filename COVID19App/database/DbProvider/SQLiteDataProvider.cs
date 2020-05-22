/**************************************************************************
 *                                                                        *
 *  File:        SQLiteDataProvider.cs                                    *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: The data provider managing data from the Database.       *
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
using System.Data;
using Core;


namespace Database
{
    /// <summary>
    /// This class implements DataProvider interface for getting the list of countryInfoEx from the Database.
    /// It also implements insertion and deletion of dayInfo from the Database.
    /// If tables dayinfo, country and region not in the Database System.Data.SQLite.SQLiteException is thrown.
    /// </summary>
    public class SQLiteDataProvider : IDatabase, IDataProvider<CountryInfoEx>
    {
        private const string DatabaseDefaultPath = @"..\..\..\resources\sql\covid.db";
        private readonly IDbManager _dbManager;

        /// <param name="databasePath">Path to the local Database</param>
        public SQLiteDataProvider(string databasePath = DatabaseDefaultPath)
        {
            _dbManager = new SQLiteDbManager();
            _dbManager.SetDatabaseConnection(databasePath);
        }

        /// <summary>
        /// Countries Id are extracted from the Database and using them,
        /// a list of CountryInfoEx is constructed.
        /// </summary>
        /// <returns>A list of CountryInfoEx</returns>
        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            var countryData = new List<CountryInfoEx>();
            List<int> countriesId = _dbManager.GetCountryCodes();
            if (countriesId == null) return countryData.AsReadOnly();
            foreach (var countryId in countriesId)
            {
                if (countryId == -1)
                {
                    Console.WriteLine("Country : ");
                    continue;
                }
                (string name, string alphaCode, int regionId, long population) = _dbManager.GetCountryInfoById(countryId);
                var daysInfo = new List<DayInfo>();
                var countryInfoList = _dbManager.GetCovidInfoByCountryId(countryId);
                if (countryInfoList != null)
                {
                    foreach ((string date, int confirmedCases, int deaths, int recoveredCases) in countryInfoList)
                    {
                        daysInfo.Add(new DayInfo(Date.Parse(date), confirmedCases, deaths, recoveredCases));
                    }
                }

                var continent = _dbManager.GetRegionNameByCountryId(countryId);
                var countryInfo = new CountryInfo(name, daysInfo);
                countryData.Add(new CountryInfoEx(countryInfo, alphaCode, continent, population));
            }
            return countryData.AsReadOnly();
        }

        /// <summary>
        /// Insert the list of countryInfo to the Database, transferring raw data to IDbManager
        /// </summary>
        /// <param name="countryInfoList">List of Country Info to be inserted in the Database</param>
        public void InsertCountryData(IReadOnlyList<CountryInfo> countryInfoList)
        {
            var counter = 0;
            
            var rawDaysInfoList = new List<Tuple<string, int, int, int, int>>();
            
            foreach (var countryInfo in countryInfoList)
            {
                var daysInfo = countryInfo.DaysInfo;
                int countryCode;
                try
                {
                    countryCode = _dbManager.GetCountryIdByName(countryInfo.Name);
                }
                catch (ObjectNotFoundException e)
                {
                    Console.WriteLine(e.Message + " -> " + countryInfo.Name);
                    continue;
                }

                foreach (var dayInfo in daysInfo)
                {
                    rawDaysInfoList.Add(Tuple.Create(dayInfo.Date.ToString(), dayInfo.Confirmed, dayInfo.Deaths, dayInfo.Recovered, countryCode));
                }

                counter += rawDaysInfoList.Count;

                Console.WriteLine("Inserted into country " + countryInfo.Name + ", Days : "+ rawDaysInfoList.Count+  " Total: " + counter);
            }

            if (rawDaysInfoList.Count > 0)
            {
                _dbManager.InsertDayInfos(rawDaysInfoList);
            }
        }

        /// <summary>
        /// Clear dayinfo table from the Database
        /// </summary>
        public void ClearDayInfoData()
        {
            _dbManager.ClearTable("dayinfo");
        }

        /// <returns>The most recent Date of the data from the Database</returns>
        public Date GetTheMostRecentDateOfData()
        {
            return Date.Parse(_dbManager.GetTheMostRecentDate());
        }
    }
}
