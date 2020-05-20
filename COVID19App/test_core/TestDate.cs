using Microsoft.VisualStudio.TestTools.UnitTesting;
using core;
using System;


namespace test_core
{
    [TestClass]
    public class TestDate
    {
        [TestMethod]
        public void TestDateRight()
        {
            var date = Date.Parse("1999-12-17");
            Assert.AreEqual(Tuple.Create(1999, 12, 17), Tuple.Create(date.Year, date.Month, date.Day));

            date = Date.Parse("2000-2-29");
            Assert.AreEqual(Tuple.Create(2000, 2, 29), Tuple.Create(date.Year, date.Month, date.Day));

            date = Date.Parse("1999-5-31");
            Assert.AreEqual(Tuple.Create(1999, 5, 31), Tuple.Create(date.Year, date.Month, date.Day));

            date = Date.Parse("2004-02-29");
            Assert.AreEqual(Tuple.Create(2004, 2, 29), Tuple.Create(date.Year, date.Month, date.Day));
        }

        [TestMethod]
        public void TestDateWrongMonth()
        {
            Assert.ThrowsException<FormatException>(() => Date.Parse("2002-0-17-4"));
            Assert.ThrowsException<FormatException>(() => Date.Parse("-2002-0-17"));
            Assert.ThrowsException<ArgumentException>(() => Date.Parse("2002-0-17"));
            Assert.ThrowsException<ArgumentException>(() => Date.Parse("2001-2-29"));
            Assert.ThrowsException<ArgumentException>(() => Date.Parse("2002-4-31"));
            Assert.ThrowsException<ArgumentException>(() => Date.Parse("2002-6-31"));
        }

        
    }
}
