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
            Date date = Date.Parse("1999-12-17");

            Assert.AreEqual(date.Year, 1999);
            Assert.AreEqual(date.Month, 12);
            Assert.AreEqual(date.Day, 17);
        }

        [TestMethod]
        public void TestDateWrongMonth()
        {
            Assert.ThrowsException<FormatException>(() => Date.Parse("2002-0-17"));
        }
    }
}
