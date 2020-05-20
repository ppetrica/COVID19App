
using core;
using database;
using System.Collections.Generic;
using System.Data;

namespace test_cache
{
    class MockDatabaseProvider : IDatabase, IDataProvider<CountryInfoEx>
    {
        List<CountryInfo> rawDaysInfoList = new List<CountryInfo>();

        public Date GetTheMostRecentDateOfData()
        {
            if (rawDaysInfoList.Count == 0)
            {
                throw new ObjectNotFoundException();
            }

            var date = new Date(1, 1, 1);
            foreach (var countryInfo in rawDaysInfoList)
            {
                foreach (var dayInfo in countryInfo.DaysInfo)
                {
                    if (date < dayInfo.Date)
                    {
                        date = dayInfo.Date;
                    }
                }
            }
            return date;
        }

        public void InsertCountryData(IReadOnlyList<CountryInfo> countryInfoList)
        {
            rawDaysInfoList.AddRange(countryInfoList);
        }

        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            var countryData = new List<CountryInfoEx>();
            foreach (var countryInfo in rawDaysInfoList)
            {
                countryData.Add(new CountryInfoEx(countryInfo, "whatever", "nevermind", 0));
            }
            return countryData;
        }
    }
}
