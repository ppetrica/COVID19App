/*************************************************************************
 *                                                                        *
 *  File:        TestConnection.cs                                        *
 *  Copyright:   (c) 2020, Moisii Marin                                   *
 *  E-mail:      marin.moisii@student.tuiasi.ro                           *
 *  Description: This module is responsible for testing the               *
 *  InternetConnection class.                                             *
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
using network;


namespace test_network
{
    [TestClass]
    public class TestConnection
    {
        [TestMethod]
        public void TestOnline()
        {
            Assert.AreEqual(true, InternetConnection.isConnectionAvailable());
            Assert.AreEqual(true, InternetConnection.isConnectionAvailable(InternetConnection.DefaultTimeout,
                "https://www.newtonsoft.com/json"));
        }

        [TestMethod]
        [Ignore]
        public void TestOffline()
        {
            Assert.AreEqual(false, InternetConnection.isConnectionAvailable());
        }
    }
}
