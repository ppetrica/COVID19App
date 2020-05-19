using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using core;
using network;

namespace database.DbCache
{
    /// <summary>
    /// The DatabaseCache class will take a look if there are any new data to insert in the database
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
