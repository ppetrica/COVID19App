using System;
using System.Collections.Generic;
using System.Windows.Forms;
using view;
using database;
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

            provider.InsertCountryData(netProvider.GetCountryData());

            var data = provider.GetCountryData();

            IView mapView = new MapView(data);
            IView globalView = new GlobalView(data);

            Application.Run(new MainForm(new List<IView> { mapView, globalView }));
        }
    }
}
