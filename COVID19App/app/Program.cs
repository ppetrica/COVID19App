using System;
using System.Collections.Generic;
using System.Windows.Forms;
using view;
using cache;
using database;

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
