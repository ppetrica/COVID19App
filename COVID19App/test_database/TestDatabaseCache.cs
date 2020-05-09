using System;
using System.Linq;
using database;
using database.DbCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test_database
{
    [TestClass]
    public class TestDatabaseCache
    {
        [TestMethod]
        public void DatabaseCacheTest()
        {
            var provider = new SQLiteDataProvider(@"..\..\resources\covid.db");
            try
            {
                provider.ClearDayInfoData();
            }
            catch (Exception)
            {
                // ignored
            }
            provider.ClearDayInfoData();

            var cacheSystem = new DatabaseCache();
            cacheSystem.Attach(provider);

            var mockDataProvider = new MockDataProvider();
            cacheSystem.CountryInfoList = mockDataProvider.GetCountryData().ToList();

            //Extract list of countryInfoEx
            var countryInfoExList = provider.GetCountryData();
            foreach (var countryInfo in countryInfoExList)
            {
                var tuple = (countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered, countryInfo.Continent,
                    countryInfo.Population);
                switch (countryInfo.Name)
                {
                    case "Italy":
                        Assert.AreEqual(tuple, (2, 0, 1, "Europe", 50_000_000));
                        break;
                    case "USA":
                        Assert.AreEqual(tuple, (18, 4, 0, "America", 300_000_000));
                        break;
                    case "Romania":
                        Assert.AreEqual(tuple, (25, 3, 1, "Europe", 19_000_000));
                        break;
                    case "China":
                        Assert.AreEqual(tuple, (80, 10, 5, "Asia", 1_000_000_000));
                        break;
                }
            }

            //the most recent day will be 30-11-1983, only the dayinfo with date > this date will change
            cacheSystem.CountryInfoList = mockDataProvider.GetCountryData2().ToList();
            //Extract list of countryInfoEx AGAIN
            countryInfoExList = provider.GetCountryData();
            foreach (var countryInfo in countryInfoExList)
            {
                var tuple = (countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered, countryInfo.Continent,
                    countryInfo.Population);
                switch (countryInfo.Name)
                {
                    case "Italy":   //will change
                        Assert.AreEqual(tuple, (150, 0, 1, "Europe", 50_000_000));
                        break;
                    case "USA": //will not change
                        Assert.AreEqual(tuple, (18, 4, 0, "America", 300_000_000));
                        break;
                    case "Romania": //will not change
                        Assert.AreEqual(tuple, (25, 3, 1, "Europe", 19_000_000));
                        break;
                    case "China":  //will change
                        Assert.AreEqual(tuple, (100, 10, 5, "Asia", 1_000_000_000));
                        break;
                }
            }
        }
    }
}
