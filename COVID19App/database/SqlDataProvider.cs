using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;

namespace database
{
    public class SqlDataProvider : DataProvider<CountryInfoEx>
    {
        private IDbManager _dbManager;

        public SqlDataProvider(string databasePath)
        {
            if (databasePath == null)
            {
                databasePath = @"..\..\..\resources\sql\covid.db";
            }
            _dbManager = new SqlDbManager();
            _dbManager.SetDatabaseConnection(databasePath);
        }

        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            List<CountryInfoEx> countryData = new List<CountryInfoEx>();

            List<int> countriesId = _dbManager.GetCountriesId();
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
    }
}
