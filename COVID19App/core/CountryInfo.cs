using System.Collections.Generic;
using System.Linq;


namespace core
{
    /// <summary>
    /// This structure will hold information about the status of
    /// COVID-19 in a particular period of time.
    /// </summary>
    public struct CountryInfo
    {
        public CountryInfo(string name, IReadOnlyList<DayInfo> daysInfo)
        {
            Name = name;
            DaysInfo = daysInfo;
        }

        public readonly string Name;
        public readonly IReadOnlyList<DayInfo> DaysInfo;

        /// <param name="country">CountryInfo object</param>
        /// <param name="mostRecentDay">The date from which we need to include data</param>
        /// <returns>CountryInfo object which contains dayInfo starting from the day parameter(exclusive)</returns>
        public static CountryInfo FilterCountryInfoDates(CountryInfo country, Date mostRecentDay)
        {
            var daysInfoTemp = country.DaysInfo.Where(dayInfo => dayInfo.Date > mostRecentDay).ToList();
            return new CountryInfo(country.Name, daysInfoTemp);
        }
    }
}
