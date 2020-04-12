using core;
using System.Collections.Generic;

namespace test_core
{
    public class MockDataProvider : DataProvider<CountryInfoEx>
    {
        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            return new List<CountryInfoEx> {
                new CountryInfoEx(
                    new CountryInfo("Mexico",
                        new List<DayInfo> {
                            new DayInfo(new Date(1980, 10, 2), 0, 0, 0),
                            new DayInfo(new Date(1980, 10, 3), 5, 1, 0),
                            new DayInfo(new Date(1980, 10, 4), 25, 3, 1)
                        }
                    ),"MX"),
                new CountryInfoEx(
                    new CountryInfo("China",
                        new List<DayInfo>() {
                            new DayInfo(new Date(1981, 11, 14), 1, 0, 1),
                            new DayInfo(new Date(1981, 11, 15), 7, 2, 1),
                            new DayInfo(new Date(1981, 11, 18), 18, 4, 0)
                        }),
                    "CA"),
                new CountryInfoEx(
                    new CountryInfo("Russia", new List<DayInfo> {
                        new DayInfo(new Date(1981, 11, 14), 0, 0, 0),
                        new DayInfo(new Date(1981, 11, 15), 1, 0, 0),
                        new DayInfo(new Date(1981, 11, 18), 2, 0, 1)
                        }),
                    "RU"),
                new CountryInfoEx(
                    new CountryInfo("Italy", new List<DayInfo> {
                        new DayInfo(new Date(1981, 11, 14), 0, 0, 0),
                        new DayInfo(new Date(1981, 11, 15), 20, 1, 3),
                        new DayInfo(new Date(1981, 11, 18), 40, 10, 5)
                        }),
                    "IT")
            };
        }
    }
}
