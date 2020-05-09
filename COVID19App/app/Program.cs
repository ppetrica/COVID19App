using System;
using System.Collections.Generic;
using System.Windows.Forms;
using view;
using database;
using database.DbCache;


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
            cacheSystem.checkData();    //check if the data is fresh (yesterday is present in the db)

            var data = provider.GetCountryData();

            IView mapView = new MapView(data);
            IView globalView = new GlobalView(data);

            Application.Run(new MainForm(new List<IView> { mapView, globalView }));
        }
    }
}
