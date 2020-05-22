/*************************************************************************
 *                                                                        *
 *  File:        TestSQLiteDataProvider.cs                                *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: This class is used to test the sqlite provider module    *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using database;
using core;
using test_core;

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
            IDataProvider<CountryInfo> mockDataProvider = new MockDataProvider();
            var list = mockDataProvider.GetCountryData();
            provider.InsertCountryData(list);

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
            //Test extraction the most recent date
            Assert.AreEqual(new Date(1983, 11, 30), provider.GetTheMostRecentDateOfData());
        }
        
    }
}
