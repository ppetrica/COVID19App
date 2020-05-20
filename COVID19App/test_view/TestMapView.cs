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
