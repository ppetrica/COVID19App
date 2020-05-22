/*************************************************************************
 *                                                                        *
 *  File:        TestDatabaseCache.cs                                     *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: This class is used to test the cache module              *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System.Linq;
using cache;
using core;
using database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using test_core;

namespace test_cache
{
    [TestClass]
    public class TestDatabaseCache
    {
        [TestMethod]
        public void DatabaseCacheTest()
        {
            var provider = new MockDatabaseProvider();

            var cacheSystem = new DatabaseCache();
            cacheSystem.Attach(provider);

            IDataProvider<CountryInfo> mockDataProvider = new MockDataProvider();
            cacheSystem.CountryInfoList = mockDataProvider.GetCountryData().ToList();

            //Extract list of countryInfoEx
            var countryInfoExList = provider.GetCountryData();
            foreach (var countryInfo in countryInfoExList)
            {
                var tuple = (countryInfo.Confirmed, countryInfo.Deaths, countryInfo.Recovered);
                switch (countryInfo.Name)
                {
                    case "Italy":
                        Assert.AreEqual(tuple, (2, 0, 1));
                        break;
                    case "USA":
                        Assert.AreEqual(tuple, (18, 4, 0));
                        break;
                    case "Romania":
                        Assert.AreEqual(tuple, (25, 3, 1));
                        break;
                    case "China":
                        Assert.AreEqual(tuple, (80, 10, 5));
                        break;
                }
            }
        }

        [TestMethod]
        public void DatabaseCacheTest_2()
        {
            DatabaseCacheTest();

            var provider = new SQLiteDataProvider(@"..\..\..\test_database\resources\covid.db");
            var cacheSystem = new DatabaseCache();
            cacheSystem.Attach(provider);
            cacheSystem.CheckUpdate();    //check if the data is fresh (yesterday is present in the db) and update the db if not
        }
    }
}
