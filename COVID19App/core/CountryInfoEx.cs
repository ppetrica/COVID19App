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

        public string Name
        {
            get => _info.Name;
        }

        public IReadOnlyList<DayInfo> DaysInfo
        {
            get => _info.DaysInfo;
        }

        public int Confirmed
        {
            get => _mostRecent.Confirmed;
        }

        public int Deaths
        {
            get => _mostRecent.Deaths;
        }
        
        public int Recovered
        {
            get => _mostRecent.Recovered;
        }

        public readonly string CountryCode;
        private CountryInfo _info;
        private DayInfo _mostRecent;
    }
}
