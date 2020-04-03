using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using database;
using core;

namespace test_database
{
    [TestClass]
    public class SqlDataProviderTest
    {
        [TestMethod]
        public void TestMethod1()
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
    }
}
