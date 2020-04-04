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
        public void GetDayInfoData()
        {
            DataProvider<CountryInfoEx> provider = new SqlDataProvider(@"..\..\resources\covid.db");
            var countryInfoExList = provider.GetCountryData();
            
            foreach (var countryInfo in countryInfoExList)
            {
                Console.WriteLine("----------------");
                Console.WriteLine(countryInfo.CountryCode);
                Console.WriteLine(countryInfo.Deaths);
                Console.WriteLine(countryInfo.Recovered);
                Console.WriteLine(countryInfo.Confirmed);
            }
        }

        [TestMethod]
        public void InsertData()
        {
            SqlDataProvider provider = new SqlDataProvider(@"..\..\resources\covid.db");

            provider.ClearDayInfoData();
            var list = CreateMockData();

            provider.InsertCountryData(list);

            var countryInfoExList = provider.GetCountryData();
            foreach (var countryInfo in countryInfoExList)
            {
                Console.WriteLine("----------------");
                Console.WriteLine(countryInfo.CountryCode);
                Console.WriteLine(countryInfo.Deaths);
                Console.WriteLine(countryInfo.Recovered);
                Console.WriteLine(countryInfo.Confirmed);
            }
            
        }

        [TestMethod]
        private List<CountryInfo> CreateMockData()
        {
            List<CountryInfo> mock = new List<CountryInfo>();
            List<DayInfo> list;

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1980, 10, 12), 0, 0, 0));
            list.Add(new DayInfo(new Date(1980, 10, 13), 5, 1, 0));
            list.Add(new DayInfo(new Date(1980, 10, 14), 25, 3, 1));
            mock.Add(new CountryInfo("Tunisia", list));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 1, 0, 1));
            list.Add(new DayInfo(new Date(1981, 11, 15), 7, 2, 1));
            list.Add(new DayInfo(new Date(1981, 11, 18), 18, 4, 0));
            mock.Add(new CountryInfo("Turkey", list));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1982, 11, 14), 0, 0, 0));
            list.Add(new DayInfo(new Date(1982, 11, 15), 1, 0, 0));
            list.Add(new DayInfo(new Date(1982, 11, 18), 2, 0, 1));
            mock.Add(new CountryInfo("Tonga", list));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1983, 11, 14), 0, 0, 0));
            list.Add(new DayInfo(new Date(1983, 11, 15), 20, 1, 2));
            list.Add(new DayInfo(new Date(1983, 11, 30), 80, 10, 5));
            mock.Add(new CountryInfo("Thailand", list));

            return mock;
        }
    }
}
