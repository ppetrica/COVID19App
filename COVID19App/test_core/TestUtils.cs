using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using core;
using System;


namespace test_core
{
    [TestClass]
    public class TestUtils
    {
        [TestMethod]
        public void TestFind()
        {
            var l = new List<int>();

            Assert.AreEqual(null, Utils.Find(l, (int x) => true));

            l.Add(1);
            l.Add(2);
            l.Add(3);
            l.Add(4);
            l.Add(5);
            l.Add(6);

            Assert.AreEqual(3, Utils.Find(l, (int x) => x == 3));
            Assert.AreEqual(2, Utils.Find(l, (int x) => x % 2 == 0));
            Assert.AreEqual(null, Utils.Find(l, (int x) => x == 7));
            Assert.AreEqual(null, Utils.Find(l, (int x) => x > 8));
        }

        [TestMethod]
        public void TestMax()
        {
            var l = new List<int>();

            Assert.ThrowsException<ArgumentException>(() => Utils.MaxElement(l, (int a, int b) => true));

            l.Add(1);
            l.Add(1);
            l.Add(1);
            Assert.AreEqual(1, Utils.MaxElement(l, (int a, int b) => a > b));
            
            l.Add(5);
            Assert.AreEqual(5, Utils.MaxElement(l, (int a, int b) => a > b));
            
            l.Add(4);
            Assert.AreEqual(5, Utils.MaxElement(l, (int a, int b) => a > b));
            
            l.Add(-2);
            Assert.AreEqual(-2, Utils.MaxElement(l, (int a, int b) => a < b));
        }
    }
}
