using core;
using System.Collections.Generic;


namespace test_core
{
    public class MockDataProvider : IDataProvider<CountryInfoEx>
    {
        public IReadOnlyList<CountryInfoEx> GetCountryData()
        {
            return new List<CountryInfoEx> {
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo> {
                            new DayInfo(new Date(1980, 10, 2), 0, 0, 0),
                            new DayInfo(new Date(1980, 10, 3), 5, 1, 0),
                            new DayInfo(new Date(1980, 10, 4), 25, 3, 1)
                        }
                    ),"MX", "America",120_000_000),
                new CountryInfoEx(
                    new CountryInfo("",
                        new List<DayInfo>() {
                            new DayInfo(new Date(1981, 11, 14), 1, 0, 1),
                            new DayInfo(new Date(1981, 11, 15), 7, 2, 1),
                            new DayInfo(new Date(1981, 11, 18), 18, 4, 0)
                        }),
                    "CA", "America", 250_000_000),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(1981, 11, 14), 0, 0, 0),
                        new DayInfo(new Date(1981, 11, 15), 1, 0, 0),
                        new DayInfo(new Date(1981, 11, 18), 2, 0, 1)
                        }),
                    "RU", "Europe",360_000_000),
                new CountryInfoEx(
                    new CountryInfo("", new List<DayInfo> {
                        new DayInfo(new Date(1981, 11, 14), 0, 0, 0),
                        new DayInfo(new Date(1981, 11, 15), 20, 1, 3),
                        new DayInfo(new Date(1981, 11, 18), 40, 10, 5)
                        }),
                    "IT", "Europe",60_000_000)
            };
        }
    }
}
