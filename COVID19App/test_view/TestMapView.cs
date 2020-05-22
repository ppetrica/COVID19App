/*************************************************************************
 *                                                                        *
 *  File:        TestMapView.cs                                           *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class can be used to test the map view.             * 
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
using System.Windows.Forms;
using System.Collections.Generic;
using core;
using view;
using test_core;


namespace test_view
{
    [TestClass]
    public class TestMapView
    {
        // This test can be used to look at the generated map.
        [Ignore]
        [TestMethod]
        public void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IDataProvider<CountryInfoEx> dataProvider = new MockDataProviderEx();
            IReadOnlyList<CountryInfoEx> mock = dataProvider.GetCountryData();

            var view = new MapView(mock);

            var form = new TestForm();

            view.Subscribe(form);

            var tabControl = new TabControl();
            tabControl.Controls.Add(view.GetPage());

            form.Controls.Add(tabControl);
            tabControl.Dock = DockStyle.Fill;

            Application.Run(form);
        }

        class TestForm : Form, IMapObserver
        {
            public TestForm()
            {
                Width = 1024;
                Height = 720;
            }

            public void OnClick(CountryInfoEx country)
            {
                System.Console.WriteLine(country.CountryCode);
            }
        }
    }
}
