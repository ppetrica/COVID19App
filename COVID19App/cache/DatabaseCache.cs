/*************************************************************************
 *                                                                        *
 *  File:        DatabaseCache.cs                                         *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: The cache manager responsable for keeping the data in    *
 *  the database up to date.                                              *
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
using System.Linq;
using System.Net;
using core;
using network;


/// <summary>
/// This module manages cache system. If there are not new information online, we will use only local database, reducing internet traffic. 
/// </summary>
namespace cache
{
    /// <summary>
    /// This class will take a look if there are any new data to insert in the database.
    /// </summary>
    public class DatabaseCache : AbstractDatabaseCache
    {
        private Date _mostRecent;

        public List<CountryInfo> CountryInfoList
        {
            get => _countryInfoList;
            set
            {
                try
                {
                    var mostRecent = getTheMostRecentDateFromProviders();
                    _countryInfoList?.Clear();
                    foreach (var countryInfo in value)
                    {
                        _countryInfoList.Add(CountryInfo.FilterCountryInfoDates(countryInfo, mostRecent));
                    }
                }
                catch (ObjectNotFoundException)
                {
                    _countryInfoList = value;
                }
                finally
                {
                    Notify();
                }
            }
        }

        /// <summary>
        /// Check if the most recent data in the database is added recently in the current day
        /// </summary>
        public void CheckUpdate()
        {
            try
            {
                _mostRecent = getTheMostRecentDateFromProviders();
                
                // Get the yesterday date.
                var yesterdayDay = DateTime.Today.AddDays(-1);
                if (new Date(yesterdayDay.Year, yesterdayDay.Month, yesterdayDay.Day) > _mostRecent)
                {
                    UpdateData();
                }
            }
            //if no data in the database
            catch (ObjectNotFoundException)
            {
                UpdateData();
            }
        }

        /// <summary>
        /// Get Data from the Internet and updating the database.
        /// If Internet Connection problem, nothing is inserted in the database.
        /// </summary>
        private void UpdateData()
        {
            try
            {
                var netProvider = new CovidDataProvider();
                CountryInfoList = netProvider.GetCountryData().ToList();
            }
            catch (WebException)
            {
                //Console.WriteLine("Internet Connection Problem");
            };
        }
    }
}
