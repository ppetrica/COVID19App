using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using database;
using core;
using System.Collections.Generic;


namespace test_database
{
    [TestClass]
    public class TestSQLiteDataProvider
    {
        [TestMethod]
        public void DataProviderTest()
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

            
            //Test insertion of CountryInfo list
            var list = CreateMockData();
            provider.InsertCountryData(list);

            //Extract list of countryInfoEx
            var countryInfoExList = provider.GetCountryData();
            foreach (var countryInfo in countryInfoExList)
            {
                var tuple = (countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered, countryInfo.Continent,
                    countryInfo.Population);
                Console.WriteLine(tuple);
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

            //Test extraction the most recent date
            Assert.AreEqual(new Date(1983, 11, 30), provider.GetTheMostRecentDateOfData());
        }

        private List<CountryInfo> CreateMockData()
        {
            var mock = new List<CountryInfo>();

            var list = new List<DayInfo>
            {
                new DayInfo(new Date(1980, 10, 12), 0, 0, 0),
                new DayInfo(new Date(1980, 10, 13), 5, 1, 0),
                new DayInfo(new Date(1980, 10, 14), 25, 3, 1)
            };
            mock.Add(new CountryInfo("Romania", list));

            list = new List<DayInfo>
            {
                new DayInfo(new Date(1981, 11, 14), 1, 0, 1),
                new DayInfo(new Date(1981, 11, 15), 7, 2, 1),
                new DayInfo(new Date(1981, 11, 18), 18, 4, 0)
            };
            mock.Add(new CountryInfo("USA", list));

            list = new List<DayInfo>
            {
                new DayInfo(new Date(1982, 11, 14), 0, 0, 0),
                new DayInfo(new Date(1982, 11, 15), 1, 0, 0),
                new DayInfo(new Date(1982, 11, 18), 2, 0, 1)
            };
            mock.Add(new CountryInfo("Italy", list));

            list = new List<DayInfo>
            {
                new DayInfo(new Date(1983, 11, 14), 0, 0, 0),
                new DayInfo(new Date(1983, 11, 15), 20, 1, 2),
                new DayInfo(new Date(1983, 11, 30), 80, 10, 5)
            };
            mock.Add(new CountryInfo("China", list));
            return mock;
        }
    }
}
