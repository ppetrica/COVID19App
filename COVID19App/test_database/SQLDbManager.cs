using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using database;

namespace test_database
{
    [TestClass]
    public class SQLDbManager_Test
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
            //Assert.AreEqual(true, b.InsertDayInfo("2020-03-07", 100, 100, 100, 996));
            //Assert.AreEqual(true, b.InsertDayInfo("2020-3-5", 100, 100, 100, 996));
        }
    }
}
