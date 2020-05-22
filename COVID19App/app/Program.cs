/*************************************************************************
 *                                                                        *
 *  File:        Program.cs                                               *
 *  Copyright:   (c) 2020, Petrica Petru                                  *
 *  E-mail:      petru.petrica@student.tuiasi.ro                          *
 *  Description: This class represents the entry point in the application,*
 *  it ties together all of the components.                               * 
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using View;
using Cache;
using Database;

/// <summary>
/// This module provides C# GUI.
/// </summary>
namespace COVID19App
{
    static class Program
    {
        /// <summary> 
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var provider = new SQLiteDataProvider();

            var cacheSystem = new DatabaseCache();
            cacheSystem.Attach(provider);
            cacheSystem.CheckUpdate();    //check if the data is fresh (yesterday is present in the db)

            var data = provider.GetCountryData();

            MapView mapView = new MapView(data);
            IView globalView = new GlobalView(data);
            CountryView countryView = new CountryView(data);

            mapView.Subscribe(countryView);

            Application.Run(new MainForm(new List<IView> { mapView, globalView , countryView }));
        }
    }
}
