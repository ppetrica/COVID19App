using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using database;
using core;
using System.Collections.Generic;

namespace test_database
{
    [TestClass]
    public class SqlDataProviderTest
    {
        [TestMethod]
        public void DataProviderTest()
        {
            var provider = new SqlDataProvider(@"..\..\resources\covid.db");

            provider.ClearDayInfoData();
            var list = CreateMockData();

            provider.InsertCountryData(list);

            var countryInfoExList = provider.GetCountryData();
            foreach (var countryInfo in countryInfoExList)
            {
                switch (countryInfo.Name)
                {
                    case "Italy":
                        Assert.AreEqual((countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered), (2, 0, 1));
                        break;
                    case "USA":
                        Assert.AreEqual((countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered), (18, 4, 0));
                        break;
                    case "Romania":
                        Assert.AreEqual((countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered), (25, 3, 1));
                        break;
                    case "China":
                        Assert.AreEqual((countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered), (80, 10, 5));
                        break;
                }
            }
        }

        [TestMethod]
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
