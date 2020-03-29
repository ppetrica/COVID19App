using System.Collections.Generic;

namespace core
{
    /// <summary>
    /// This structure will hold information about the status of
    /// COVID-19 in a particular period of time.
    /// </summary>
    public struct CountryInfo
    {
        public CountryInfo(string name, List<DayInfo> daysInfo)
        {
            Name = name;
            DaysInfo = daysInfo;
        }

        public readonly string Name;
        public readonly IReadOnlyList<DayInfo> DaysInfo;
    }
}
