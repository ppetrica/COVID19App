/*************************************************************************
 *                                                                        *
 *  File:        TestSQLiteDbManager.cs                                   *
 *  Copyright:   (c) 2020, Enachi Vasile                                  *
 *  E-mail:      vasile.enachi@student.tuiasi.ro                          *
 *  Description: This class is used to test the sqlite Database manager.  *
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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;
using Core;


namespace TestDatabase
{
    [TestClass]
    public class TestSQLiteDbManager
    {
        private const string DatabaseTestPath = @"..\..\resources\covid.db";

        [TestMethod]
        public void TestSqLiteDbManager()
        {
            IDbManager sqLiteDbManager = new SQLiteDbManager();
            sqLiteDbManager.SetDatabaseConnection(@"..\..\resources\covid.db");

            //Test clear all tables
            sqLiteDbManager.ClearTable("dayinfo");
            sqLiteDbManager.ClearTable("country");
            sqLiteDbManager.ClearTable("region");

            //Test Insert region
            sqLiteDbManager.InsertRegion(1, "America");
            sqLiteDbManager.InsertRegion(2, "Asia");
            sqLiteDbManager.InsertRegion(3, "Europe");

            //Test Insert country
            sqLiteDbManager.InsertCountry("Italy", 1, "IT", 3, 50_000_000);
            sqLiteDbManager.InsertCountry("Romania", 2, "RO", 3, 19_000_000);
            sqLiteDbManager.InsertCountry("USA", 3, "US", 1, 300_000_000);
            sqLiteDbManager.InsertCountry("China", 4, "CH", 2, 1_000_000_000);

            //Test Insert dayinfo
            var d = new Date(2020, 3, 7);
            sqLiteDbManager.InsertDayInfo(d.ToString(), 90000, 25000, 10000, 1);
            sqLiteDbManager.InsertDayInfo("2020-4-5", 3000, 30, 300, 2);
            sqLiteDbManager.InsertDayInfo("2020-4-5", 80000, 2000, 60000, 4);

            var usaInfo = new List<Tuple<string, int, int, int, int>>
            {
                Tuple.Create("2020-4-5", 400000, 12000, 50000, 3),
                Tuple.Create("2020-4-6", 410000, 13000, 51000, 3),
                Tuple.Create("2020-4-7", 420000, 14000, 52000, 3)
            };
            foreach (var (item1, item2, item3, item4, item5) in usaInfo)
            {
                sqLiteDbManager.InsertDayInfo(item1, item2, item3, item4, item5);
            }
            
            //Test get day info
            var usaInfoFromDb = sqLiteDbManager.GetCovidInfoByCountryId(3);
            for (var i = 0; i < usaInfoFromDb.Count; i++)
            {
                Assert.AreEqual(true, usaInfoFromDb[i].Equals(SubTuple5To4<string, int, int, int, int>(usaInfo[i])));
            }

            //Test get region name
            Assert.AreEqual("Asia", sqLiteDbManager.GetRegionNameById(2));
            Assert.AreEqual("Europe", sqLiteDbManager.GetRegionNameById(3));

            //Test get country name and id
            Assert.AreEqual(1, sqLiteDbManager.GetCountryIdByName("Italy"));
            Assert.AreEqual(Tuple.Create("Romania", "RO", 3, (long)19_000_000), sqLiteDbManager.GetCountryInfoById(2));

            //Test get region name by country id
            Assert.AreEqual("Europe", sqLiteDbManager.GetRegionNameByCountryId(1));
            Assert.AreEqual("Asia", sqLiteDbManager.GetRegionNameByCountryId(4));

            //Test the most recent data
            Assert.AreEqual("2020-4-7", sqLiteDbManager.GetTheMostRecentDate());
        }

        /// <summary>
        /// Tuple Conversion from 5-Tuple to 3-Tuple
        /// </summary>
        /// <param name="tuple">Input 5-tuple</param>
        /// <returns>A 3-tuple consisting of the first 3 tuples of the input</returns>
        public static Tuple<T1, T2, T3, T4> SubTuple5To4<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> tuple) => Tuple.Create(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

    }

}
