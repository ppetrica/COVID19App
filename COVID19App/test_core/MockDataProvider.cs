using System.Collections.Generic;
using core;

namespace test_core
{
    public class MockDataProvider : IDataProvider<CountryInfo>
    {
        public IReadOnlyList<CountryInfo> GetCountryData()
        {
            var mock = new List<CountryInfo>();

            var list = new List<DayInfo>
            {
                new DayInfo(new Date(1980, 10, 12), 0, 0, 0),
                new DayInfo(new Date(1980, 10, 13), 5, 1, 0),
                new DayInfo(new Date(1980, 10, 14), 25, 3, 1)
            };
            mock.Add(new CountryInfo("Romania", list));

            list = new List<DayInfo>
            {
                new DayInfo(new Date(1981, 11, 14), 1, 0, 1),
                new DayInfo(new Date(1981, 11, 15), 7, 2, 1),
                new DayInfo(new Date(1981, 11, 18), 18, 4, 0)
            };
            mock.Add(new CountryInfo("USA", list));

            list = new List<DayInfo>
            {
                new DayInfo(new Date(1982, 11, 14), 0, 0, 0),
                new DayInfo(new Date(1982, 11, 15), 1, 0, 0),
                new DayInfo(new Date(1982, 11, 18), 2, 0, 1)
            };
            mock.Add(new CountryInfo("Italy", list));

            list = new List<DayInfo>
            {
                new DayInfo(new Date(1983, 11, 14), 0, 0, 0),
                new DayInfo(new Date(1983, 11, 15), 20, 1, 2),
                new DayInfo(new Date(1983, 11, 30), 80, 10, 5)
            };
            mock.Add(new CountryInfo("China", list));
            return mock;
        }
    }
}
