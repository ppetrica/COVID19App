using System.Collections.Generic;
using System.Data;
using core;

namespace database.DbCache
{
    public class DatabaseCache : AbstractDatabaseCache
    {
        public List<CountryInfo> CountryInfoList
        {
            get => _countryInfoList;
            set
            {
                try
                {
                    _countryInfoList?.Clear();
                    var mostRecent = getTheMostRecentDateFromProviders();
                    foreach (var countryInfo in value)
                    {
                        _countryInfoList.Add(CountryInfo.FilterCountryInfoDates(countryInfo, mostRecent));
                    }
                }
                catch (ObjectNotFoundException)
                {
                    _countryInfoList = value;
                }
                finally
                {
                    Notify();
                }
            }
        }
    }
}
