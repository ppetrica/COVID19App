/*************************************************************************
 *                                                                        *
 *  File:        DatabaseCache.cs                                         *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: The Cache manager responsable for keeping the data in    *
 *  the Database up to date.                                              *
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
using Core;
using Network;


/// <summary>
/// This module manages Cache system. If there are not new information online, we will use only local Database, reducing internet traffic. 
/// </summary>
namespace Cache
{
    /// <summary>
    /// This class will take a look if there are any new data to insert in the Database.
    /// </summary>
    public class DatabaseCache : AbstractDatabaseCache
    {
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
        /// Check if the most recent data in the Database is added recently in the current day
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
            //if no data in the Database
            catch (ObjectNotFoundException)
            {
                UpdateData();
            }
        }

        /// <summary>
        /// Get Data from the Internet and updating the Database.
        /// If Internet Connection problem, nothing is inserted in the Database.
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

        private Date _mostRecent;
    }
}
