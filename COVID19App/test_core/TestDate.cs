/*************************************************************************
 *                                                                        *
 *  File:        TestDate.cs                                              *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class is used to test the Date class.               *
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
using Core;
using System;


namespace TestCore
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
