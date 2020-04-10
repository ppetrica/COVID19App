using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;

namespace database
{
    /// <summary>
    /// This class implements DataProvider interface for getting the list of countryInfoEx from the database.
    /// It also implements insertion and deletion of dayInfo from the database.
    /// </summary>
    public class SQLiteDataProvider : DataProvider<CountryInfoEx>
    {
        private const string DatabaseDefaultPath = @"..\..\..\resources\sql\covid.db";
        private readonly IDbManager _dbManager;

        /// <param name="databasePath">Path to the local database</param>
        public SQLiteDataProvider(string databasePath = DatabaseDefaultPath)
        {
            _dbManager = new SQLiteDbManager();
            _dbManager.SetDatabaseConnection(databasePath);
        }

        /// <summary>
        /// Countries Id are extracted from the database and using them,
        /// a list of CountryInfoEx is constructed.
        /// </summary>
        /// <returns>A list of CountryInfoEx</returns>
        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            List<CountryInfoEx> countryData = new List<CountryInfoEx>();
            List<int> countriesId = _dbManager.GetCountriesId();
            if (countriesId == null) return countryData.AsReadOnly();
            foreach (var countryId in countriesId)
            {
                (string name, string alphaCode, int regionId) = _dbManager.GetCountryInfoById(countryId);
                List<DayInfo> daysInfo = new List<DayInfo>();
                var countryInfoList = _dbManager.GetCovidInfoByCountryId(countryId);
                if (countryInfoList != null)
                {
                    foreach ((string date, int confirmedCases, int deaths, int recoveredCases) in countryInfoList)
                    {
                        daysInfo.Add(new DayInfo(Date.Parse(date), confirmedCases, deaths, recoveredCases));
                    }
                }
                var countryInfo = new CountryInfo(name, daysInfo);
                countryData.Add(new CountryInfoEx(countryInfo, alphaCode));
            }
            return countryData.AsReadOnly();
        }

        /// <summary>
        /// Insert the list of countryInfo to the database, transferring raw data to IDbManager
        /// </summary>
        /// <param name="countryInfoList">List of Country Info to be inserted in the database</param>
        public void InsertCountryData(List<CountryInfo> countryInfoList)
        {
            foreach (var countryInfo in countryInfoList)
            {
                var daysInfo = countryInfo.DaysInfo;
                var countryCode = _dbManager.GetCountryIdByName(countryInfo.Name);
                foreach (var dayInfo in daysInfo)
                {
                    _dbManager.InsertDayInfo(dayInfo.Date.ToString(), dayInfo.Confirmed, dayInfo.Deaths,
                        dayInfo.Recovered, countryCode);
                }
            }
        }

        /// <summary>
        /// Clear dayinfo table from the database
        /// </summary>
        public void ClearDayInfoData()
        {
            _dbManager.ClearTable("dayinfo");
        }
    }
}
