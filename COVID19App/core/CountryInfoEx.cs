using System;
using System.Collections.Generic;


namespace core
{
    /// <summary>
    /// This structure will hold information about the status of
    /// COVID-19 in a particular period of time.
    /// It extends CountryInfo.
    /// </summary>
    public struct CountryInfoEx
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">The CountryInfo structure that will be extended</param>
        /// <param name="countryCode"></param>
        /// <param name="continent"></param>
        /// <param name="population"></param>
        public CountryInfoEx(CountryInfo info, string countryCode, string continent, long population)
        {
            _info = info;
            CountryCode = countryCode;
            Continent = continent;
            Population = population;

            _mostRecent = Utils.MaxElement(info.DaysInfo, (DayInfo d1, DayInfo d2) => d1.Date > d2.Date);
        }

        public string Name => _info.Name;

        public IReadOnlyList<DayInfo> DaysInfo => _info.DaysInfo;

        public int Confirmed => _mostRecent.Confirmed;

        public int Deaths => _mostRecent.Deaths;

        public int Recovered => _mostRecent.Recovered;
       
        public readonly string CountryCode;
        private readonly CountryInfo _info;
        private readonly DayInfo _mostRecent;
        public readonly long Population;
        public readonly string Continent;
    }
}
