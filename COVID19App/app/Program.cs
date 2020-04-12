using System;
using System.Collections.Generic;
using System.Windows.Forms;
using view;
using test_core;
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

            var net_provider = new CovidDataProvider();
            var provider = new SQLiteDataProvider();

            provider.InsertCountryData(net_provider.GetCountryData());

            IView view = new MapView(provider.GetCountryData());

            Application.Run(new MainForm(new List<IView> { view }));
        }
    }
}
