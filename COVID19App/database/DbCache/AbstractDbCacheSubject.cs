using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core;
using database.DbProvider;

namespace database
{
    /// <summary>
    /// The DatabaseCache class will take a look if there are any new data to insert in the database
    /// </summary>
    public abstract class AbstractDatabaseCache
    {
        private List<AbstractDbObserver> _providers = new List<AbstractDbObserver>();
        protected List<CountryInfo> _countryInfoList = new List<CountryInfo>();

        public void Attach(AbstractDbObserver provider)
        {
            _providers.Add(provider);
        }

        public void Detach(AbstractDbObserver provider)
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
