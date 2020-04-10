using System;
using System.Collections.Generic;
using System.Windows.Forms;
using view;
using test_core;

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

            MockDataProvider dataProvider = new MockDataProvider();

            IView view = new MapView(dataProvider.GetCountryData());

            Application.Run(new MainForm(new List<IView> { view }));
        }
    }
}
