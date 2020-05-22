/*************************************************************************
 *                                                                        *
 *  File:        TestUtils.cs                                             *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class is used to test the utility functions         *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

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

        [TestMethod]
        public void TestDateValidate()
        {
            Assert.AreEqual(true, Utils.IsValid(2000, 2, 29));
            Assert.AreEqual(true, Utils.IsValid(2004, 2, 29));
            Assert.AreEqual(true, Utils.IsValid(2004, 5, 31));
            Assert.AreEqual(true, Utils.IsValid(20016, 2, 29));
            Assert.AreEqual(true, Utils.IsValid(2004, 5, 31));
            Assert.AreEqual(true, Utils.IsValid(2004, 7, 31));
            Assert.AreEqual(true, Utils.IsValid(2004, 8, 31));


            Assert.AreEqual(false, Utils.IsValid(0, 1, 29));
            Assert.AreEqual(false, Utils.IsValid(2100, 2, 29));
            Assert.AreEqual(false, Utils.IsValid(2000, 0, 29));
            Assert.AreEqual(false, Utils.IsValid(0, 1, 29));
            Assert.AreEqual(false, Utils.IsValid(0, 1, 29));
            Assert.AreEqual(false, Utils.IsValid(2004, 4, 31));
            Assert.AreEqual(false, Utils.IsValid(2004, 6, 31));
        }
    }
}
