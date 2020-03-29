using System.Collections.Generic;


namespace core
{
    public struct ContryInfoEx
    {
        public ContryInfoEx(CountryInfo info, string countryCode)
        {
            _info = info;
            CountryCode = countryCode;
        }

        public string Name
        {
            get
            {
                return _info.Name;
            }
        }

        public IReadOnlyList<DayInfo> DaysInfo
        {
            get
            {
                return _info.DaysInfo;
            }
        }

        public readonly string CountryCode;
        
        private CountryInfo _info;
    }
}
