using System.Collections.Generic;


namespace core
{
    public struct CountryInfoEx
    {
        public CountryInfoEx(CountryInfo info, string countryCode)
        {
            _info = info;
            CountryCode = countryCode;

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
    }
}
