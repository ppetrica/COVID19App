using System.Collections.Generic;
using System.Data;
using core;
using database;

namespace cache
{
    /// <summary>
    /// The DatabaseCache class will take a look if there are any new data to insert in the database
    /// </summary>
    public abstract class AbstractDatabaseCache
    {
        private List<IDatabase> _providers = new List<IDatabase>();
        protected List<CountryInfo> _countryInfoList = new List<CountryInfo>();

        public void Attach(IDatabase provider)
        {
            _providers.Add(provider);
        }

        public void Detach(IDatabase provider)
        {
            _providers.Remove(provider);
        }

        public void Notify()
        {
            foreach (var provider in _providers)
            {
                provider.InsertCountryData(_countryInfoList);
            }
        }

        /// <returns>Return the most recent date of data of the existing providers
        /// If not recentDate was found throw the ObjectNotFoundException exception</returns>
        protected Date getTheMostRecentDateFromProviders()
        {
            var dateList = new List<Date>();
            foreach(var provider in _providers)
            {
                try
                {
                    dateList.Add(provider.GetTheMostRecentDateOfData());
                }
                catch (ObjectNotFoundException)
                {

                }
            }

            if (dateList.Count == 0)
            {
                throw new ObjectNotFoundException();
            }
            return Utils.MaxElement(dateList, (date1, date2) => date1 > date2);
        }
    }
}
