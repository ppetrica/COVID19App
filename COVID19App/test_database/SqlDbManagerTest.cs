using System;
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
            string startupPath = Environment.CurrentDirectory;
            Console.WriteLine(startupPath);
            IDbManager b = new SqlDbManager();
            b.SetDatabaseConnection(@"..\..\resources\covid.db");
            //Assert.AreEqual(true, b.InsertCountry("TestABc", 995, "te", 4));
            //Assert.AreEqual(true, b.InsertRegion(8, "TestABc"));
            Date d = new Date(2020, 3, 7);
            Assert.AreEqual(true, b.InsertDayInfo(d.ToString(), 100, 100, 100, 996));
            Assert.AreEqual(true, b.InsertDayInfo("2020-3-5", 100, 100, 100, 996));

            //Console.WriteLine(b.GetRegionNameById(6));
            //var temp = b.GetCountryInfoById(10);
            //Console.WriteLine(temp.Item1 + " " + temp.Item2 + " " + temp.Item3);

            var list = b.GetCovidInfoByCountryId(996);
            foreach (var item in list)
            {
                Console.WriteLine(item.Item1 + " " + item.Item2 + " " + item.Item3 + " " + item.Item3);
            }
        }
    }
}
