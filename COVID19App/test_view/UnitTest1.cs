using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Collections.Generic;
using core;
using view;


namespace test_view
{
    [TestClass]
    public class MapViewTest
    {
        [TestMethod]
        public void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<CountryInfoEx> mock = CreateMockData();

            MapView view = new MapView(mock);

            TestForm form = new TestForm();

            view.Subscribe(form);

            form.Controls.Add(view.GetControl());

            Application.Run(form);
        }

        private List<CountryInfoEx> CreateMockData()
        {
            List<CountryInfoEx> mock = new List<CountryInfoEx>();

            List<DayInfo> list;

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1980, 10, 2), 0, 0, 0));
            list.Add(new DayInfo(new Date(1980, 10, 3), 5, 1, 0));
            list.Add(new DayInfo(new Date(1980, 10, 4), 25, 3, 1));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "MX"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 1, 0, 1));
            list.Add(new DayInfo(new Date(1981, 11, 15), 7, 2, 1));
            list.Add(new DayInfo(new Date(1981, 11, 18), 18, 4, 0));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "CA"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 0, 0, 0));
            list.Add(new DayInfo(new Date(1981, 11, 15), 1, 0, 0));
            list.Add(new DayInfo(new Date(1981, 11, 18), 2, 0, 1));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "RU"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 0, 0, 0));
            list.Add(new DayInfo(new Date(1981, 11, 15), 20, 1, 2));
            list.Add(new DayInfo(new Date(1981, 11, 30), 80, 10, 5));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "IT"));

            return mock;
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
