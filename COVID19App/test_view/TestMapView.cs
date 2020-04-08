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
        // This test can be used to look at the generated map.
        [TestMethod]
        public void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<CountryInfoEx> mock = CreateMockData();

            MapView view = new MapView(mock);

            TestForm form = new TestForm();

            view.Subscribe(form);

            TabControl tabControl = new TabControl();
            tabControl.Controls.Add(view.GetPage());

            form.Controls.Add(tabControl);
            tabControl.Dock = DockStyle.Fill;

            Application.Run(form);
        }

        public static List<CountryInfoEx> CreateMockData()
        {
            return new List<CountryInfoEx> {
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo> {
                            new DayInfo(new Date(1980, 10, 2), 0, 0, 0),
                            new DayInfo(new Date(1980, 10, 3), 5, 1, 0),
                            new DayInfo(new Date(1980, 10, 4), 25, 3, 1)
                        }
                    ),"MX"),
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo>() {
                            new DayInfo(new Date(1981, 11, 14), 1, 0, 1),
                            new DayInfo(new Date(1981, 11, 15), 7, 2, 1),
                            new DayInfo(new Date(1981, 11, 18), 18, 4, 0)
                        }),
                    "CA"),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(1981, 11, 14), 0, 0, 0),
                        new DayInfo(new Date(1981, 11, 15), 1, 0, 0),
                        new DayInfo(new Date(1981, 11, 18), 2, 0, 1)
                        }),
                    "RU"),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(1981, 11, 14), 0, 0, 0),
                        new DayInfo(new Date(1981, 11, 15), 20, 1, 3),
                        new DayInfo(new Date(1981, 11, 18), 40, 10, 5)
                        }),
                    "IT")
            };
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
