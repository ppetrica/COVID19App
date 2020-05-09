using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using view;
using database;
using database.DbCache;
using network;


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

            var netProvider = new CovidDataProvider();
            var provider = new SQLiteDataProvider();
            var cacheSystem = new DatabaseCache();
            cacheSystem.Attach(provider);

            try
            {
                cacheSystem.CountryInfoList = netProvider.GetCountryData().ToList();
            }
            catch (ArgumentException)
            {
                //Console.WriteLine("Internet Connection Problem");
            }

            var data = provider.GetCountryData();

            IView mapView = new MapView(data);
            IView globalView = new GlobalView(data);

            Application.Run(new MainForm(new List<IView> { mapView, globalView }));
        }
    }
}
