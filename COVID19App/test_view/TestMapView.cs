using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Collections.Generic;
using core;
using view;
using test_core;


namespace test_view
{
    [TestClass]
    public class MapViewTest
    {
        // This test can be used to look at the generated map.
        [TestMethod]
        public void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataProvider<CountryInfoEx> dataProvider = new MockDataProvider();
            IReadOnlyList<CountryInfoEx> mock = dataProvider.GetCountryData();

            MapView view = new MapView(mock);

            TestForm form = new TestForm();

            view.Subscribe(form);

            TabControl tabControl = new TabControl();
            tabControl.Controls.Add(view.GetPage());

            form.Controls.Add(tabControl);
            tabControl.Dock = DockStyle.Fill;

            Application.Run(form);
        }

        class TestForm : Form, MapObserver
        {
            public TestForm()
            {
                Width = 640;
                Height = 480;
            }

            public void OnClick(CountryInfoEx country)
            {
                System.Console.WriteLine(country.CountryCode);
            }
        }
    }
}
