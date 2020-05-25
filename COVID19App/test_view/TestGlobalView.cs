/*************************************************************************
 *                                                                        *
 *  File:        TestGlobalView.cs                                        *
 *  Copyright:   (c) 2020, Moisii Marin                                   *
 *  E-mail:      marin.moisii@student.tuiasi.ro                           *
 *  Description: This class can be used to test the global View           *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Core;
using View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms;
using TestCore;


namespace TestView
{
    [TestClass]
    public class TestGlobalView
    {
        // This test is used to look at page with statistics at global level.
        [Ignore]
        [TestMethod]
        public void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
 
            IDataProvider<CountryInfoEx> provider = new MockDataProviderEx();

            IReadOnlyList<CountryInfoEx> data = provider.GetCountryData();

            Form form = new Form();
            form.Width = 800;
            form.Height = 600;

            IView view = new GlobalView(data);

            TabControl tabControl = new TabControl();
            tabControl.Controls.Add(view.GetPage());

            form.Controls.Add(tabControl);
            tabControl.Dock = DockStyle.Fill;

            Application.Run(form);
        }
    }
}
