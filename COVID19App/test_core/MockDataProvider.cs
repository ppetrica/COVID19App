using core;
using System.Collections.Generic;


namespace test_core
{
    /// <summary>
    /// This class is used only for test. 
    /// It provides some simple data to test the views: MapView, CountryView, GlobalView.
    /// </summary>
    public class MockDataProvider : IDataProvider<CountryInfoEx>
    {
        /// <returns>Fake statistics about COVID-19 organized by country as a read only list of core.CountryInfo.</returns>
        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            return new List<CountryInfoEx> {
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo> {
                            new DayInfo(new Date(2020, 1, 22), 0, 0, 0),
                            new DayInfo(new Date(2020, 1, 23), 5, 1, 0),
                            new DayInfo(new Date(2020, 1, 24), 25, 3, 1)
                        }
                    ),"MX", "America",120_000_000),
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo>() {
                            new DayInfo(new Date(2020, 1, 22), 1, 0, 1),
                            new DayInfo(new Date(2020, 1, 23), 7, 2, 1),
                            new DayInfo(new Date(2020, 1, 24), 18, 4, 0)
                        }),
                    "CA", "America", 250_000_000),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(2020, 1, 22), 0, 0, 0),
                        new DayInfo(new Date(2020, 1, 23), 1, 0, 0),
                        new DayInfo(new Date(2020, 1, 24), 2, 0, 1)
                        }),
                    "RU", "Europe",360_000_000),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(2020, 1, 22), 0, 0, 0),
                        new DayInfo(new Date(2020, 1, 23), 20, 1, 3),
                        new DayInfo(new Date(2020, 1, 24), 40, 10, 5)
                        }),
                    "IT", "Europe",60_000_000)
            };
        }
    }
}
