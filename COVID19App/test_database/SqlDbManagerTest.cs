using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using database;
using core;

namespace test_database
{
    [TestClass]
    public class SqlDbManager_Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDbManager b = new SqlDbManager();
            b.SetDatabaseConnection(@"..\..\resources\covid.db");

            //Test clear all tables
            Assert.AreEqual(true, b.ClearTable("dayinfo"));
            Assert.AreEqual(true, b.ClearTable("country"));
            Assert.AreEqual(true, b.ClearTable("region"));

            //Test Insert region
            Assert.AreEqual(true, b.InsertRegion(1, "America"));
            Assert.AreEqual(true, b.InsertRegion(2, "Asia"));
            Assert.AreEqual(true, b.InsertRegion(3, "Europe"));

            //Test Insert country
            Assert.AreEqual(true, b.InsertCountry("Italy", 1, "IT", 3));
            Assert.AreEqual(true, b.InsertCountry("Romania", 2, "RO", 3));
            Assert.AreEqual(true, b.InsertCountry("USA", 3, "US", 1));
            Assert.AreEqual(true, b.InsertCountry("China", 4, "CH", 2));

            //Test Insert dayinfo
            Date d = new Date(2020, 3, 7);
            Assert.AreEqual(true, b.InsertDayInfo(d.ToString(), 90000, 25000, 10000, 1));
            Assert.AreEqual(true, b.InsertDayInfo("2020-4-5", 3000, 30, 300, 2));
            Assert.AreEqual(true, b.InsertDayInfo("2020-4-5", 80000, 2000, 60000, 4));

            var usaInfo = new List<Tuple<string, int, int, int, int>>();
            usaInfo.Add(Tuple.Create("2020-4-5", 400000, 12000, 50000, 3));
            usaInfo.Add(Tuple.Create("2020-4-6", 410000, 13000, 51000, 3));
            usaInfo.Add(Tuple.Create("2020-4-7", 420000, 14000, 52000, 3));
            foreach (var (item1, item2, item3, item4, item5) in usaInfo)
            {
                Assert.AreEqual(true, b.InsertDayInfo(item1, item2, item3, item4, item5));
            }
            
            //Test get day info
            var usaInfoFromDb = b.GetCovidInfoByCountryId(3);
            for (int i = 0; i < usaInfoFromDb.Count; i++)
            {
                Assert.AreEqual(true, usaInfoFromDb[i].Equals(SubTuple5To4<string, int, int, int, int>(usaInfo[i])));
            }

            //Test get region name
            Assert.AreEqual("Asia", b.GetRegionNameById(2));
            Assert.AreEqual("Europe", b.GetRegionNameById(3));

            //Test get country name and id
            Assert.AreEqual(1, b.GetCountryIdByName("Italy"));
            Assert.AreEqual(Tuple.Create("Romania", "RO", 3), b.GetCountryInfoById(2));

        }

        public static Tuple<T1, T2, T3, T4> SubTuple5To4<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> tuple) => Tuple.Create(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

    }

}
